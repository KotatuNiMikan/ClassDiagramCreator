using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassDiagramCreator
{
    internal class PartOfRelationshipCollection : ICollection<PartOfRelationship>
    {
        private List<PartOfRelationship> collection = new List<PartOfRelationship>();
        public int Count => this.collection.Count;

        public bool IsReadOnly => true;

        public void Add(PartOfRelationship item)
        {
            if (this.collection.Contains(item))
            {
                Console.WriteLine($"***{item.ToString()}:{item.GetHashCode()}");
                return;
            }

            this.collection.Add(item);
            Console.WriteLine($"{item.ToString()}:{item.GetHashCode()}");
        }

        public void Clear()
        {
            this.collection = new List<PartOfRelationship>();
        }

        public bool Contains(PartOfRelationship item)
        {
            return this.collection.Contains(item);
        }

        public void CopyTo(PartOfRelationship[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<PartOfRelationship> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }
        /*
        public List<Type> GetTypeList()
        {
            return collection.SelectMany(x => x.ToTypeList()).ToList();
        }
        */

        public bool Remove(PartOfRelationship item)
        {
            return this.collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new List<PartOfRelationship>().GetEnumerator();
        }
    }
}