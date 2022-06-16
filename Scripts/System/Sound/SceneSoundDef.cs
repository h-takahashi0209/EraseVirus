using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// シーンごとのサウンド定義
    /// </summary>
    public static class SoundDef
    {
        /// <summary>
        /// ResidentScene
        /// </summary>
        public static class ResidentScene
        {
            /// <summary>
            /// Se
            /// </summary>
            public enum Se
            {
                Select  ,
                Decide  ,
                Sizeof  ,
            }

            /// <summary>
            /// Bgm
            /// </summary>
            public enum Bgm
            {
                Sizeof,
            }
        }

        /// <summary>
        /// GameScene
        /// </summary>
        public static class GameScene
        {
            /// <summary>
            /// Se
            /// </summary>
            public enum Se
            {
                MoveCapsule         ,
                CompFallCapsule     ,
                DisappearCapsule    ,
                StageClear          ,
                AllClear            ,
                GameOver            ,
                Sizeof              ,
            }

            /// <summary>
            /// Bgm
            /// </summary>
            public enum Bgm
            {
                GameScene   ,
                Sizeof      ,
            }
        }
    }
}
