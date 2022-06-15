using System;
using System.Collections.Generic;


namespace TakahashiH
{
    /// <summary>
    /// IReadOnlyCollection 拡張メソッド定義用
    /// </summary>
    public static class IReadOnlyCollectionExtensions
    {
        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 空か
        /// </summary>
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> self)
        {
            return self.Count == 0;
        }

        /// <summary>
        /// null または空か
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> self)
        {
            return self == null || self.Count == 0;
        }

        /// <summary>
        /// 要素が1つでもあるか
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IReadOnlyCollection<T> self)
        {
            return !self.IsNullOrEmpty();
        }
    }
}
