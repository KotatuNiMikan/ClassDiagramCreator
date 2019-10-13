// -----------------------------------------------------------------------
// <copyright file="PartOfRelationship.cs" company="TODO">
// Copyright (c) TODO. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ClassDiagramCreator
{
    /// <summary>
    /// 集約関係性クラスです。
    /// </summary>
    internal class PartOfRelationship
    {
        /// <summary>
        /// 集約先です。
        /// </summary>
        private readonly Type target;

        /// <summary>
        /// 集約対象です。
        /// </summary>
        private readonly Type x;

        /// <summary>
        /// 集約数です。
        /// </summary>
        private readonly int fieldsCount;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="target">集約先です。</param>
        /// <param name="x">集約物です。</param>
        /// <param name="fieldsCount">集約数です。</param>
        public PartOfRelationship(Type target, Type x, int fieldsCount)
        {
            this.target = target;
            this.x = x;
            this.fieldsCount = fieldsCount;
        }

        /// <summary>
        /// 加算します。
        /// </summary>
        /// <param name="left">左辺です。</param>
        /// <param name="right">右辺です。</param>
        /// <returns>関係の列挙です。</returns>
        public static IEnumerable<PartOfRelationship> operator +(PartOfRelationship left, PartOfRelationship right)
        {
            if (left.target == right.target && left.x == right.x)
            {
                return new List<PartOfRelationship> { new PartOfRelationship(left.target, left.x, left.fieldsCount + right.fieldsCount) };
            }

            return new List<PartOfRelationship> { left, right };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (this.target.IsNested)
            {
                return string.Empty;
            }

            if (this.x.IsNested)
            {
                return string.Empty;
            }

            return $"{this.target.GetNameForUml()} o-- {this.x.GetNameForUml()}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 型のリストにします。
        /// </summary>
        /// <returns>型の列挙です。</returns>
        internal IEnumerable<Type> ToTypeList()
        {
            return new List<Type> { this.target, this.x };
        }
    }
}