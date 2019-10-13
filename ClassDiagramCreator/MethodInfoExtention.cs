// -----------------------------------------------------------------------
// <copyright file="MethodInfoExtention.cs" company="TODO">
// Copyright (c) TODO. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ClassDiagramCreator
{
    /// <summary>
    /// メソッドに関する拡張クラスです。
    /// </summary>
    internal static class MethodInfoExtention
    {
        /// <summary>
        /// クラス図用の文字列を作成します。
        /// </summary>
        /// <param name="methodInfo">メソッド情報です。</param>
        /// <returns>文字列です。</returns>
        public static string CreateUmlText(this MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters()
                .Select(para => string.Format(CultureInfo.CurrentCulture, "{0}:{1}", para.Name, para.ParameterType.GetNameForUml()));
            return $"{GetAccesingTypeText(methodInfo)}{GetAttribute(methodInfo)}{methodInfo.Name}({string.Join(", ", parameters)}):{methodInfo.ReflectedType.GetNameForUml()}";
        }

        /// <summary>
        /// メソッドの属性を取得します。
        /// </summary>
        /// <param name="methodInfo">メソッド情報です。</param>
        /// <returns>文字列です。</returns>
        private static string GetAttribute(MethodInfo methodInfo)
        {
            return $"{(methodInfo.IsStatic ? "{static}" : string.Empty)}{(methodInfo.IsAbstract ? "{abstract}" : string.Empty)}";
        }

        /// <summary>
        /// メソッドの可視性を取得します。
        /// </summary>
        /// <param name="methodInfo">メソッド情報です。</param>
        /// <returns>文字列です。</returns>
        private static string GetAccesingTypeText(MethodInfo methodInfo)
        {
            return methodInfo.IsPublic ? "+" : methodInfo.IsFamily ? "#" : methodInfo.IsPrivate ? "-" : string.Empty;
        }
    }
}
