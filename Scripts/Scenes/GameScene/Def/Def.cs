
/// <summary>
/// ゲーム内共通定義
/// </summary>
namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// カプセル・敵の色種別
    /// </summary>
    public enum ColorType
    {
        None = -1,
        Red     ,
        Blue    ,
        Yellow  ,
        Sizeof  ,
    }

    /// <summary>
    /// オブジェクト種別
    /// </summary>
    public enum ObjectType
    {
        None        ,
        HalfCapsule ,
        Enemy       ,
    }

    /// <summary>
    /// 移動方向
    /// </summary>
    public enum MoveDirection
    {
        Left    ,
        Right   ,
        Down    ,
    }

    /// <summary>
    /// 回転方向
    /// </summary>
    public enum RotateDirection
    {
        Left    ,
        Right   ,
    }

    /// <summary>
    /// カプセルの向き
    /// </summary>
    public enum CapsuleDirection
    {
        Vertial     ,
        Horizontal  ,
    }

    /// <summary>
    /// ペアになっているブロックがある方向
    /// </summary>
    public enum PairedBlockDirection
    {
        None    ,
        Up      ,
        Down    ,
        Right   ,
        Left    ,
    }

    /// <summary>
    /// ステート
    /// </summary>
    public enum State
    {
        None = -1           ,
        GenerateStage       ,
        AppearCapsule       ,
        MoveCapsule         ,
        DisappearCapsule    ,
        FallCapsule         ,
        GameOver            ,
        GameClear           ,
        Finished            ,
    }

    /// <summary>
    /// 数値定義
    /// </summary>
    public static class GameSceneDef
    {
        /// <summary>
        /// 1ブロックのサイズ
        /// </summary>
        public const int OneBlockSize = 80;

        /// <summary>
        /// 横ブロック数
        /// </summary>
        public const int BlockNumWidth = 8;

        /// <summary>
        /// 縦ブロック数
        /// </summary>
        public const int BlockNumHeight = 16;

        /// <summary>
        /// フィールド横幅
        /// </summary>
        public const int FieldWidth = OneBlockSize * BlockNumWidth;

        /// <summary>
        /// フィールド縦
        /// </summary>
        public const int FieldHeight = OneBlockSize * BlockNumHeight;

        /// <summary>
        /// カプセル出現Xブロック座標
        /// </summary>
        public const int CapsuleAppearedBlockPosX = BlockNumWidth / 2;

        /// <summary>
        /// カプセル出現Yブロック座標
        /// </summary>
        public const int CapsuleAppearedBlockPosY = BlockNumHeight - 1;

        /// <summary>
        /// 同じ色のブロックがいくつ揃ったら消滅させるか
        /// </summary>
        public const int DisappearBlockMatchCount = 4;
    }
}
