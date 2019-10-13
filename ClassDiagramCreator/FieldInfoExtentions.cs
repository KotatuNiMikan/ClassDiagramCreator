// -----------------------------------------------------------------------
// <copyright file="FieldInfoExtentions.cs" company="TODO">
// Copyright (c) TODO. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Reflection;

namespace ClassDiagramCreator
{
    /// <summary>
    /// フィールド情報の拡張クラスです。
    /// </summary>
    internal static class FieldInfoExtentions
    {
        /// <summary>
        /// クラス図用の文字列を作成します。
        /// </summary>
        /// <param name="fieldInfo">フィールド情報です。</param>
        /// <returns>文字列です。</returns>
        public static string CreateUmlText(this FieldInfo fieldInfo)
        {
            return $"{(fieldInfo.IsPublic ? "+" : fieldInfo.IsFamily ? "#" : fieldInfo.IsPrivate ? "-" : string.Empty)}{fieldInfo.Name}:{fieldInfo.FieldType.GetNameForUml()}";
        }
    }
}
