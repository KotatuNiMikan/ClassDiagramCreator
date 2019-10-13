// -----------------------------------------------------------------------
// <copyright file="BaseTypeRelationship.cs" company="TODO">
// Copyright (c) TODO. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ClassDiagramCreator
{
    /// <summary>
    /// 汎化や実現などの親子関係性です。
    /// </summary>
    internal class BaseTypeRelationship
    {
        /// <summary>
        /// 子です。
        /// </summary>
        private readonly Type target;

        /// <summary>
        /// 親です。
        /// </summary>
        private readonly Type baseType;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="target">対象クラス（子）です。</param>
        /// <param name="baseType">基底の型（親）です。</param>
        public BaseTypeRelationship(Type target, Type baseType)
        {
            this.target = target;
            this.baseType = baseType;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var relationText = this.target.IsInterface == this.baseType.IsInterface ? "<|--" : "<|..";
            return $"{this.baseType.Name.Split('`')[0]} {relationText} {this.target.Name.Split('`')[0]}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 型の列挙にします。
        /// </summary>
        /// <returns>型の列挙です。</returns>
        internal IEnumerable<Type> ToTypeList()
        {
            return new List<Type> { this.target, this.baseType };
        }
    }
}