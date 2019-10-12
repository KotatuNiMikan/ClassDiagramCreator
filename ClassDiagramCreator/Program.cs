﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="TODO">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Linq;

namespace ClassDiagramCreator
{
    /// <summary>
    /// プログラムのエントリーポイント用クラスです。
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// プログラムのエントリーポイントです。
        /// </summary>
        /// <param name="args">引数文字列です。</param>
        private static void Main(string[] args)
        {
            string assemblyFile = args[0];
            var classDiagramCleator = ClassDiagramCleator.CreateInstance("ClassDiagramCreator.dll");
            classDiagramCleator.OutputFile($"クラス図.puml");
        }
    }
}
