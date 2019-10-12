using System;
using System.Collections.Generic;

namespace ClassDiagramCreator
{
    internal class BaseTypeRelationship
    {
        private Type target;
        private Type baseType;

        public BaseTypeRelationship(Type target, Type baseType)
        {
            this.target = target;
            this.baseType = baseType;
        }

        internal IEnumerable<Type> ToTypeList()
        {
            return new List<Type> { target, this.baseType};
        }

        public override string ToString()
        {
            var relationText = target.IsInterface == this.baseType.IsInterface ? "<|--" : "<|..";
            return $"{baseType.Name} {relationText} {target.Name}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}