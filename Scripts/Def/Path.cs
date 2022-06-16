
namespace TakahashiH
{
    /// <summary>
    /// パス定義用クラス
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// 共通
        /// </summary>
        public static class Common
        {
            /// <summary>
            /// サウンドデータ
            /// </summary>
            public static string SoundData = "ScriptableObjects/Scenes/";
        }

        /// <summary>
        /// シーン別
        /// </summary>
        public static class Scenes
        {
            #region GameScene

            /// <summary>
            /// GameScene
            /// </summary>
            public static class GameScene
            {
                /// <summary>
                /// 設定ファイル
                /// </summary>
                public static string Settings = "ScriptableObjects/Scenes/GameScene/GameSceneSettings";

                /// <summary>
                /// 半カプセル画像
                /// </summary>
                public static string HalfCapsuleImage = "Textures/Scenes/GameScene/HalfCapsule/HalfCapsule";

                /// <summary>
                /// ペアの半カプセル画像
                /// </summary>
                public static string PairedHalfCapsuleImage = "Textures/Scenes/GameScene/HalfCapsule/HalfCapsulePaired";

                /// <summary>
                /// 敵画像
                /// </summary>
                public static string EnemyImage = "Textures/Scenes/GameScene/Enemy/Enemy";

                /// <summary>
                /// ステージ csv
                /// </summary>
                public static string StageCsv = "Stage/Stage";
            }

            #endregion
        }
    }
}
