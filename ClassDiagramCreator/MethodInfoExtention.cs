using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClassDiagramCreator
{

    public static class MethodInfoExtention
    {
        public static string CreateUmlText(this MethodInfo methodInfo)
        {
            return $"{GetAccesingTypeText(methodInfo)}{(methodInfo.IsStatic ? "{static}" : string.Empty)}{(methodInfo.IsAbstract ? "{abstract}" : string.Empty)}{methodInfo.Name}():{methodInfo.ReflectedType.GetNameForUml()}";
        }

        private static string GetAccesingTypeText(MethodInfo methodInfo)
        {
            return (methodInfo.IsPublic ? "+" : methodInfo.IsFamily ? "#" : methodInfo.IsPrivate ? "-" : string.Empty);
        }
    }
}
