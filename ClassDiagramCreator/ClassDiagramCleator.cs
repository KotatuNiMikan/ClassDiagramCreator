//-----------------------------------------------------------------------
// <copyright file="ClassDiagramCleator.cs" company="TODO">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassDiagramCreator
{
    /// <summary>
    /// クラス図作成者です。
    /// </summary>
    internal class ClassDiagramCleator
    {
        /// <summary>
        /// 対象のアセンブリです。
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// 親子関係のコレクションです。
        /// </summary>
        private readonly BaseTypeRelationshipCollection baseTypeRelationshipCollection;

        /// <summary>
        /// 集約関係のコレクションです。
        /// </summary>
        private readonly PartOfRelationshipCollection partOfRelationshipCollection;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="assembly">対象のアセンブリです。</param>
        public ClassDiagramCleator(Assembly assembly)
        {
            this.baseTypeRelationshipCollection = new BaseTypeRelationshipCollection();
            this.partOfRelationshipCollection = new PartOfRelationshipCollection();
            this.assembly = assembly;
        }

        /// <summary>
        /// インスタンスを作成します。
        /// </summary>
        /// <param name="assemblyFile">アセンブリーへのパスです。</param>        /// 
        /// <returns>インスタンスです。</returns>
        public static ClassDiagramCleator CreateInstance(string assemblyFile)
        {
            var assembly = Assembly.LoadFrom(assemblyFile);
            return new ClassDiagramCleator(assembly);
        }

        /// <summary>
        /// クラス図のファイルを出力します。
        /// </summary>
        /// <param name="file">ファイルパスです。</param>
        public void OutputFile(string file)
        {
            this.assembly
                .GetTypes()
                .ToList()
                .ForEach(this.Inspection);
            File.WriteAllText(file, this.CreateUmlText());
        }

        /// <summary>
        /// 検証します。
        /// </summary>
        /// <param name="target">対象のクラスです。</param>
        private void Inspection(Type target)
        {
            this.InspectionBaseTypeRelation(target);
            this.InspectionPartOfRelation(target);
        }

        /// <summary>
        /// 集約関係を検証します。
        /// </summary>
        /// <param name="target">対象のクラスです。</param>
        private void InspectionPartOfRelation(Type target)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var fields = target.GetFields(bindingFlags);
            fields.Select(x => x.FieldType)
                .Where(x => !x.IsPrimitive)
                .Where(x => !x.GetGenericArguments().Any())
                .Select(x => new PartOfRelationship(target, x, fields.Count(y => y.FieldType == x)))
                .ToList()
                .ForEach(this.partOfRelationshipCollection.Add);
        }

        /// <summary>
        /// 親子関係を検証します。
        /// </summary>
        /// <param name="target">対象の型です。</param>
        private void InspectionBaseTypeRelation(Type target)
        {
            if (target.BaseType != null && target.BaseType != typeof(object))
            {
                this.baseTypeRelationshipCollection.Add(new BaseTypeRelationship(target, target.BaseType));
                this.Inspection(target.BaseType);
            }

            IEnumerable<Type> interfaces = target.GetInterfaces();
            var baseInterfaces = interfaces.SelectMany(x => x.GetInterfaces());
            interfaces = interfaces.Where(x => !baseInterfaces.Contains(x));
            interfaces.Select(x => new BaseTypeRelationship(target, x))
                .ToList()
                .ForEach(this.baseTypeRelationshipCollection.Add);
            interfaces.ToList()
                .ForEach(this.Inspection);
        }

        /// <summary>
        /// クラス図用の文字列を作成します。
        /// </summary>
        /// <returns>文字列です。</returns>
        private string CreateUmlText()
        {
            var umlText = new StringBuilder();
            umlText.AppendLine("@startuml");
            umlText.AppendLine($"title {assembly.FullName} {DateTime.Now}");
            this.baseTypeRelationshipCollection.Select(x => x.ToString())
                .Union(this.partOfRelationshipCollection.Select(x => x.ToString()))
                .Distinct()
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList()
                .ForEach(x => umlText.AppendLine(x));
            this.assembly
                .GetTypes()
                .Where(x => !x.IsNested)
                .ToList()
                /*/
            this.baseTypeRelationshipCollection.GetTypeList()
                .Union(this.partOfRelationshipCollection.GetTypeList())
                .Where(x => !x.IsNested)
                // TODO 下記要確認
                .Where(x => !x.IsGenericType)
                //.Where(x=> x.Namespace == assembly.GetManifestResourceNames())
                .ToList()
                //*/
                .ForEach(x => umlText.AppendLine(x.ToUmlText()));
            umlText.AppendLine("@enduml");
            return umlText.ToString();
        }
    }
}