using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace ClassDiagramCreator
{
    public static class TypeExtention
    {
        public static string ToUmlText(this Type self)
        {
            var result = new StringBuilder();
            result.AppendLine($"{(self.IsEnum ? "enum" : self.IsInterface ? "interface" : self.IsAbstract ? "abstract class" : "class")} {self.Name}");
            result.AppendLine("{");
            result = CreateFieldInfos(self, result);
            result = CreateMethodInfos(self, result);
            result.AppendLine("}");
            return result.ToString();
        }

        public static string GetNameForUml(this Type self)
        {
            var genericArguments = self.GetGenericArguments().ToList();
            if (!genericArguments.Any())
                return self.Name;
            var typeName = self.Name.Split('`')[0];
            var genericArgumentsText = string.Join(", ", genericArguments.Select(GetNameForUml));
            return $"{typeName}<{genericArgumentsText}>";
        }

        private static StringBuilder CreateMethodInfos(Type targetType, StringBuilder builder)
        {
            var result = new StringBuilder(builder.ToString());
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            targetType.GetMethods(bindingFlags)
                .Where(methodInfo => methodInfo.IsPublic || methodInfo.IsFamily)
                .OrderByDescending(fieldInfo => fieldInfo.IsStatic)
                .ThenByDescending(fieldInfo => fieldInfo.IsPublic)
                .ThenBy(fieldInfo => fieldInfo.IsPrivate)
                .ToList()
                .ForEach(methodInfo => result.AppendLine(methodInfo.CreateUmlText()));
            return result;
        }

        private static StringBuilder CreateFieldInfos(Type targetType, StringBuilder builder)
        {
            var result = new StringBuilder(builder.ToString());
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            targetType.GetFields(bindingFlags)
                ////.Where(fieldInfo => fieldInfo.IsPublic || fieldInfo.IsFamily || (fieldInfo.IsPrivate))
                .Where(fieldInfo => fieldInfo.IsPublic || fieldInfo.IsFamily || (fieldInfo.IsPrivate && fieldInfo.FieldType.IsPrimitive))
                .OrderByDescending(fieldInfo => fieldInfo.IsStatic)
                .ThenByDescending(fieldInfo => fieldInfo.IsPublic)
                .ThenBy(fieldInfo => fieldInfo.IsPrivate)
                .ToList()
                .ForEach(fieldInfo => result.AppendLine(fieldInfo.CreateUmlText()));
            return result;
        }
    }
}
