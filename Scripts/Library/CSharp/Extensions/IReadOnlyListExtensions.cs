using System;
using System.Collections.Generic;


namespace TakahashiH
{
    /// <summary>
    /// IReadOnlyList 拡張メソッド定義用
    /// </summary>
    public static class IReadOnlyListExtensions
    {
        //====================================
        //! 関数（private static）
        //====================================

        /// <summary>
        /// 乱数生成機
        /// </summary>
        private static Random msRandom = new Random((int)DateTime.Now.ToUnixTime());


        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 空か
        /// </summary>
        public static bool IsEmpty<T>(this IReadOnlyList<T> self)
        {
            return self.Count == 0;
        }

        /// <summary>
        /// null または空か
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> self)
        {
            return self == null || self.Count == 0;
        }

        /// <summary>
        /// 要素が1つでもあるか
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IReadOnlyList<T> self)
        {
            return !self.IsNullOrEmpty();
        }

        /// <summary>
        /// 最初の要素を返す
        /// 要素が無い場合はデフォルトの値を返す
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static T FirstOrDefault<T>(this IReadOnlyList<T> self)
        {
            return self.IsNullOrEmpty() ? default(T) : self[0];
        }

        /// <summary>
        /// 指定された条件に一致する最初の要素を返す
        /// 条件に一致する要素が無い場合はデフォルトの値を返す
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static T FirstOrDefault<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            for (int i = 0; i < self.Count; i++)
            {
                if (predicate(self[i]))
                {
                    return self[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// 最後の要素を返す
        /// 要素が無い場合はデフォルトの値を返す
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static T LastOrDefault<T>(this IReadOnlyList<T> self)
        {
            return self.IsNullOrEmpty() ? default(T) : self[self.Count - 1];
        }

        /// <summary>
        /// 指定された条件に一致する最後の要素を返す
        /// 条件に一致する要素が無い場合はデフォルトの値を返す
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static T LastOrDefault<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            for (int i = self.Count - 1; i >= 0; i--)
            {
                if (predicate(self[i]))
                {
                    return self[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// 指定されたインデックスの要素を返す
        /// インデックスが範囲外の場合はデフォルトの値を返す
        /// </summary>
        /// <param name="index"> インデックス </param>
        public static T ElementAtOrDefault<T>(this IReadOnlyList<T> self, int index)
        {
            if (index < 0 || index >= self.Count)
            {
                return default(T);
            }

            return self[index];
        }

        /// <summary>
        /// 要素をランダムで1つ返す
        /// </summary>
        /// <param name="index"> インデックス </param>
        public static T ElementAtRandom<T>(this IReadOnlyList<T> self)
        {
            if (self.IsNullOrEmpty())
            {
                return default(T);
            }

            return self[msRandom.Next(self.Count - 1)];
        }

        /// <summary>
        /// 指定された条件に一致する要素数を返す
        /// </summary>
        /// <param name="index"> インデックス </param>
        public static int Count<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            int count = 0;

            for (int i = self.Count - 1; i >= 0; i--)
            {
                if (predicate(self[i]))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 指定された条件に一致する最初の要素のインデックスを返す
        /// 条件に一致する要素が無い場合は -1 を返す
        /// </summary>
        /// <param name="index"> インデックス </param>
        public static int FindIndex<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            for (int i = 0; i < self.Count; i++)
            {
                if (predicate(self[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 指定された条件に一致する最後の要素のインデックスを返す
        /// 条件に一致する要素が無い場合は -1 を返す
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static int FindLastIndex<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            for (int i = self.Count - 1; i >= 0; i--)
            {
                if (predicate(self[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 指定された条件に一致する要素があるか
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static bool Any<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            for (int i = self.Count - 1; i >= 0; i--)
            {
                if (predicate(self[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 指定された条件に一致する要素が無いか
        /// </summary>
        /// <param name="predicate"> 条件 </param>
        public static bool NotAny<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
        {
            for (int i = self.Count - 1; i >= 0; i--)
            {
                if (predicate(self[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 指定された要素が含まれているか
        /// </summary>
        /// <param name="elem"> 検索対象の要素 </param>
        public static bool Contains<T>(this IReadOnlyList<T> self, T elem)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = self.Count - 1; i >= 0; i--)
            {
                if (comparer.Equals(self[i], elem))
                {
                    return true;
                }
            }

            return true;
        }

        /// <summary>
        /// foreach
        /// </summary>
        /// <param name="action"> アクション </param>
        public static void ForEach<T>(this IReadOnlyList<T> self, Action<T> action)
        {
            for (int i = self.Count - 1; i >= 0; i--)
            {
                action(self[i]);
            }
        }
    }
}
