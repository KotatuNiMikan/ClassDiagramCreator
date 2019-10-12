using System;

namespace ClassDiagramCreator
{
    internal class PartOfRelationship
    {
        private Type target;
        private Type x;
        private int fieldsCount;

        public PartOfRelationship(Type target, Type x, int fieldsCount)
        {
            this.target = target;
            this.x = x;
            this.fieldsCount = fieldsCount;
        }

        public override string ToString()
        {
            return $"{target.GetNameForUml()} o-- {x.GetNameForUml()}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /*
        operator + (PartOfRelationship left, PartOfRelationship right)
        {
        }
        //*/
    }
}