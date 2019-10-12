using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClassDiagramCreator
{
    public static class FieldInfoExtentions
    {
        public static string CreateUmlText(this FieldInfo fieldInfo)
        {
            return $"{(fieldInfo.IsPublic ? "+" : fieldInfo.IsFamily ? "#" : fieldInfo.IsPrivate ? "-" : string.Empty)}{fieldInfo.Name}:{fieldInfo.FieldType.GetNameForUml()}";
        }
    }
}
