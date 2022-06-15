using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// シーンごとのサウンド定義
    /// </summary>
    public static class SoundDef
    {
        /// <summary>
        /// MiniGameSelectScene
        /// </summary>
        public static class MiniGameSelectScene
        {
            /// <summary>
            /// Se
            /// </summary>
            public enum Se
            {
                Sizeof,
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
        /// ResidentScene
        /// </summary>
        public static class ResidentScene
        {
            /// <summary>
            /// Se
            /// </summary>
            public enum Se
            {
                ButtonCancel    ,
                ButtonDecide    ,
                Sizeof          ,
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
        /// ThreeDBattleScene
        /// </summary>
        public static class ThreeDBattleScene
        {
            /// <summary>
            /// Se
            /// </summary>
            public enum Se
            {
                Sizeof,
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
        /// TiltRaceScene
        /// </summary>
        public static class TiltRaceScene
        {
            /// <summary>
            /// SE
            /// </summary>
            public enum Se
            {
                Cancel          ,
                Count           ,
                Decide          ,
                Engine          ,
                GameOver        ,
                Hit             ,
                RecoveryLife    ,
                Start           ,
                SpeedDown       ,
                SpeedUp         ,
                Telop           ,
                Sizeof          ,
            }

            /// <summary>
            /// BGM 種別
            /// </summary>
            public enum Bgm
            {
                Race    ,
                Sizeof  ,
            }
        }

        /// <summary>
        /// TitleScene
        /// </summary>
        public static class TitleScene
        {
            /// <summary>
            /// Se
            /// </summary>
            public enum Se
            {
                Sizeof,
            }

            /// <summary>
            /// Bgm
            /// </summary>
            public enum Bgm
            {
                Sizeof,
            }
        }
    }
}
