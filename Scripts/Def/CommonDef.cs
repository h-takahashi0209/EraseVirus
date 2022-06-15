
/// <summary>
/// 全体共通定義
/// </summary>
namespace TakahashiH
{
    /// <summary>
    /// シーン種別
    /// </summary>
    public enum SceneType
    {
        None            = -1,
        ResidentScene   ,
        TitleScene      ,
        SettingScene    ,
        GameScene       ,
        Sizeof          ,
    }

    /// <summary>
    /// サウンド種別
    /// </summary>
    public enum SoundType
    {
        None    = -1,
        Bgm     ,
        Se      ,
        Sizeof  ,
    }

    /// <summary>
    /// カプセル落下速度種別
    /// </summary>
    public enum FallSpeedType
    {
        Low ,
        Mid ,
        Hi  ,
    }

    /// <summary>
    /// 数値定義
    /// </summary>
    public static class CommonDef
    {
        /// <summary>
        /// 最大レベル
        /// </summary>
        public const int MaxLevel = 20;

        /// <summary>
        /// フェードにかける時間（秒）
        /// </summary>
        public const float FadeTimeSec = 0.5f;
    }
}
