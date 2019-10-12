using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassDiagramCreator
{
    internal class ClassDiagramCleator
    {
        private List<Type> targetTypes;
        private BaseTypeRelationshipCollection baseTypeRelationshipCollection;
        private PartOfRelationshipCollection partOfRelationshipCollection;

        public ClassDiagramCleator(IEnumerable<Type> targetTypes)
        {
            this.targetTypes = targetTypes.ToList();
            this.baseTypeRelationshipCollection = new BaseTypeRelationshipCollection();
            this.partOfRelationshipCollection = new PartOfRelationshipCollection();
        }

        public static ClassDiagramCleator CreateInstance(string assemblyFile, string targetTypeName)
        {
            var assembly = Assembly.LoadFrom(assemblyFile);
            return new ClassDiagramCleator(new List<Type> { typeof(HttpStyleUriParser) });
        }

        public void OutputFile(string file)
        {
            targetTypes.ForEach(this.Inspection);
            File.WriteAllText(file, this.CreateUmlText());
        }

        private void Inspection(Type target)
        {
            this.InspectionBaseTypeRelation(target);
            this.InspectionPartOfRelation(target);
        }

        private void InspectionPartOfRelation(Type target)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var fields = target.GetFields(bindingFlags);
            fields.Select(x => x.FieldType)
                .Where(x => !x.IsPrimitive)
                .Where(x => !x.GetGenericArguments().Any())
                .Select(x => new PartOfRelationship(target, x, fields.Count(y => y.FieldType == x)))
                .ToList()
                .ForEach(partOfRelationshipCollection.Add);
        }

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

        private string CreateUmlText()
        {
            var umlText = new StringBuilder();
            umlText.AppendLine("@startuml");
            umlText.AppendLine($"title {DateTime.Now}");
            this.baseTypeRelationshipCollection.Select(x => x.ToString())
                .ToList()
                .ForEach(x => umlText.AppendLine(x));
            partOfRelationshipCollection.Select(x => x.ToString())
                .ToList()
                .ForEach(x => umlText.AppendLine(x));
            this.baseTypeRelationshipCollection.GetTypeList()
                .ForEach(x => umlText.AppendLine(x.ToUmlText()));
            umlText.AppendLine("@enduml");
            return umlText.ToString();
        }
    }



}