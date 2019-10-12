//-----------------------------------------------------------------------
// <copyright file="PartOfRelationshipCollection.cs" company="TODO">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassDiagramCreator
{
    /// <summary>
    /// 集約関係性クラスのコレクションです。
    /// </summary>
    internal class PartOfRelationshipCollection : ICollection<PartOfRelationship>
    {
        /// <summary>
        /// 関係のリストです。
        /// </summary>
        private List<PartOfRelationship> collection = new List<PartOfRelationship>();

        /// <inheritdoc/>
        public int Count => this.collection.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => true;

        /// <inheritdoc/>
        public void Add(PartOfRelationship item)
        {
            if (this.collection.Contains(item))
            {
                return;
            }

            this.collection.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.collection = new List<PartOfRelationship>();
        }

        /// <inheritdoc/>
        public bool Contains(PartOfRelationship item)
        {
            return this.collection.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(PartOfRelationship[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<PartOfRelationship> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(PartOfRelationship item)
        {
            return this.collection.Remove(item);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new List<PartOfRelationship>().GetEnumerator();
        }

        /// <summary>
        /// 型の列挙を取得します。
        /// </summary>
        /// <returns>型の列挙です。</returns>
        internal IEnumerable<Type> GetTypes()
        {
            return this.collection.SelectMany(item => item.ToTypeList()).Distinct();
        }
    }
}