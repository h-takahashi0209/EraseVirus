using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Stack 拡張メソッド定義用
    /// </summary>
    public static class StackExtensions
    {
        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 先頭の要素を削除して返す
        /// 要素が無い場合はデフォルトの値を返す
        /// </summary>
        public static T PopOrDefault<T>(this Stack<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Pop();
        }

        /// <summary>
        /// 先頭の要素を削除せず返す
        /// 要素が無い場合はデフォルトの値を返す
        /// </summary>
        public static T PeekOrDefault<T>(this Stack<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Peek();
        }
    }
}
