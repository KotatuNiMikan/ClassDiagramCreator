using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassDiagramCreator
{
    internal class BaseTypeRelationshipCollection : ICollection<BaseTypeRelationship>
    {
        private List<BaseTypeRelationship> collection = new List<BaseTypeRelationship>();
        public int Count => this.collection.Count;

        public bool IsReadOnly => true;

        public void Add(BaseTypeRelationship item)
        {
            if (this.collection.Contains(item)) return;
            this.collection.Add(item);
        }

        public void Clear()
        {
            this.collection = new List<BaseTypeRelationship>();
        }

        public bool Contains(BaseTypeRelationship item)
        {
            return this.collection.Contains(item);
        }

        public void CopyTo(BaseTypeRelationship[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BaseTypeRelationship> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        public List<Type> GetTypeList()
        {
            return this.collection.SelectMany(x => x.ToTypeList()).ToList();
        }

        public bool Remove(BaseTypeRelationship item)
        {
            return this.collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new List<BaseTypeRelationship>().GetEnumerator();
        }
    }
}