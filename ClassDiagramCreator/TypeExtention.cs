// -----------------------------------------------------------------------
// <copyright file="TypeExtention.cs" company="TODO">
// Copyright (c) TODO. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassDiagramCreator
{
    /// <summary>
    /// 型の拡張クラスです。
    /// </summary>
    internal static class TypeExtention
    {
        /// <summary>
        /// クラス図用の文字列を作成します。
        /// </summary>
        /// <param name="self">型です。</param>
        /// <returns>文字列です。</returns>
        public static string ToUmlText(this Type self)
        {
            var result = new StringBuilder();
            result.AppendLine($"{(self.IsEnum ? "enum" : self.IsInterface ? "interface" : self.IsAbstract ? "abstract class" : "class")} {self.GetNameForUml()}");
            result.AppendLine("{");
            result = CreateFieldInfos(self, result);
            result = CreateMethodInfos(self, result);
            result.AppendLine("}");
            return result.ToString();
        }

        /// <summary>
        /// クラス図用の名前を取得します。
        /// </summary>
        /// <param name="self">型です。</param>
        /// <returns>クラス図用の名前です。</returns>
        public static string GetNameForUml(this Type self)
        {
            var genericArguments = self.GetGenericArguments().ToList();
            if (!genericArguments.Any())
            {
                return self.Name;
            }

            var typeName = self.Name.Split('`')[0];
            var genericArgumentsText = string.Join(", ", genericArguments.Select(GetNameForUml));
            return $"{typeName}<{genericArgumentsText}>";
        }

        /// <summary>
        /// 関数情報を作成します。
        /// </summary>
        /// <param name="targetType">対象の型です。</param>
        /// <param name="builder">つなげるビルダーです。</param>
        /// <returns>ビルダーです。</returns>
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

        /// <summary>
        /// フィールドを作成します。
        /// </summary>
        /// <param name="targetType">対象の型です。</param>
        /// <param name="builder">つなげるビルダーです。</param>
        /// <returns>ビルダーです。</returns>
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
