
namespace TakahashiH
{
    /// <summary>
    /// string 拡張メソッド定義用
    /// </summary>
    public static class StringExtensions
    {
        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// フォーマット形式で返す
        /// </summary>
        /// <param name="args"> 引数 </param>
        public static string Format(this string self, object args)
        {
            return string.Format(self, args);
        }

        /// <summary>
        /// フォーマット形式で返す
        /// </summary>
        /// <param name="args1">    引数1    </param>
        /// <param name="args2">    引数2    </param>
        public static string Format(this string self, object args1, object args2)
        {
            return string.Format(self, args1, args2);
        }

        /// <summary>
        /// フォーマット形式で返す
        /// </summary>
        /// <param name="args1">    引数1    </param>
        /// <param name="args2">    引数2    </param>
        /// <param name="args3">    引数3    </param>
        public static string Format(this string self, object args1, object args2, object args3)
        {
            return string.Format(self, args1, args2, args3);
        }
    }
}
