using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Queue 拡張メソッド定義用
    /// </summary>
    public static class QueueExtensions
    {
        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 先頭の要素を削除して返す
        /// 要素が無い場合はデフォルトの値を返す
        /// </summary>
        public static T DequeueOrDefault<T>(this Queue<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Dequeue();
        }

        /// <summary>
        /// 先頭の要素を削除せず返す
        /// 要素が無い場合はデフォルトの値を返す
        /// </summary>
        public static T PeekOrDefault<T>(this Queue<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Peek();
        }
    }
}
