// -----------------------------------------------------------------------
// <copyright file="BaseTypeRelationshipCollection.cs" company="TODO">
// Copyright (c) TODO. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassDiagramCreator
{
    /// <summary>
    /// 親子関係性のコレクションです。
    /// </summary>
    internal class BaseTypeRelationshipCollection : ICollection<BaseTypeRelationship>
    {
        /// <summary>
        /// 関係のリストです。
        /// </summary>
        private List<BaseTypeRelationship> collection = new List<BaseTypeRelationship>();

        /// <inheritdoc/>
        public int Count => this.collection.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => true;

        /// <inheritdoc/>
        public void Add(BaseTypeRelationship item)
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
            this.collection = new List<BaseTypeRelationship>();
        }

        /// <inheritdoc/>
        public bool Contains(BaseTypeRelationship item)
        {
            return this.collection.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(BaseTypeRelationship[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<BaseTypeRelationship> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        /// <summary>
        /// 型の列挙を取得します。
        /// </summary>
        /// <returns>型の列挙です。</returns>
        public IEnumerable<Type> GetTypeList()
        {
            return this.collection.SelectMany(x => x.ToTypeList());
        }

        /// <inheritdoc/>
        public bool Remove(BaseTypeRelationship item)
        {
            return this.collection.Remove(item);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new List<BaseTypeRelationship>().GetEnumerator();
        }
    }
}