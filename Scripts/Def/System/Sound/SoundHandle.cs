
namespace TakahashiH
{
    public interface ISoundHandle
    {
        public SoundType    SoundType           { get; }
        public int          AudioSourceIndex    { get; }
    }

    /// <summary>
    /// サウンドハンドル
    /// </summary>
    public struct SoundHandle : ISoundHandle
    {
        //====================================
        //! 変数（public static）
        //====================================

        /// <summary>
        /// 空データ
        /// </summary>
        public static SoundHandle Empty = new SoundHandle(SoundType.None, -1);


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// サウンドハンドル
        /// </summary>
        public SoundType SoundType { get; private set; }

        /// <summary>
        /// オーディオソースのインデックス
        /// </summary>
        public int AudioSourceIndex { get; private set; }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="soundType">           サウンド種別                      </param>
        /// <param name="audioSourceIndex">    オーディオソースのインデックス    </param>
        public SoundHandle(SoundType soundType, int audioSourceIndex)
        {
            SoundType           = soundType;
            AudioSourceIndex    = audioSourceIndex;
        }
    }
}
