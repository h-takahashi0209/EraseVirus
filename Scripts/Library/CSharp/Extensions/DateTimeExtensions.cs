using System;


namespace TakahashiH
{
    /// <summary>
    /// DateTime 拡張メソッド定義用
    /// </summary>
    public static class DateTimeExtensions
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// Unix エポック
        /// </summary>
        private static DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// Unix 時間に変換
        /// </summary>
        public static long ToUnixTime(this DateTime self)
        {
            return (long)(self.ToUniversalTime() - UNIX_EPOCH).TotalSeconds;
        }
    }
}
