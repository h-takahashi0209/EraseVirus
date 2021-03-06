using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// 設定
    /// </summary>
    [CreateAssetMenu(fileName = "GameSceneSettings", menuName = "ScriptableObjects/Scenes/GameScene/Settings")]
    public sealed class Settings : ScriptableObject
    {
        //====================================
        //! 変数（private static）
        //====================================

        /// <summary>
        /// インスタンス
        /// </summary>
        private static Settings msInstance;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// カプセル自動落下にかかる時間（秒）リスト
        /// </summary>
        [SerializeField] private float[] mAutoFallCapsuleTimeSecList;

        /// <summary>
        /// カプセル強制落下にかかる時間（秒）
        /// </summary>
        [SerializeField] private float mCapsuleForceFallTimeSec;

        /// <summary>
        /// カプセル消滅時の落下にかかる時間（秒）
        /// </summary>
        [SerializeField] private float mCapsuleDisappearFallTimeSec;

        /// <summary>
        /// カプセル着地後の移動可能時間（秒）
        /// </summary>
        [SerializeField] private float mCapsuleCompFallMoveTimeSec;

        /// <summary>
        /// ステージ構築モード
        /// </summary>
        [SerializeField] private GenerateStageMode mGenerateStageMode;

        /// <summary>
        /// 強制固定させるカプセル色リスト
        /// </summary>
        [SerializeField] private ColorType[] mForceColorTypeList = new ColorType[2];


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// カプセル強制落下にかかる時間（秒）
        /// </summary>
        public static float CapsuleForceFallTimeSec => msInstance.mCapsuleForceFallTimeSec;

        /// <summary>
        /// カプセル消滅時の落下にかかる時間（秒）
        /// </summary>
        public static float CapsuleDisappearFallTimeSec => msInstance.mCapsuleDisappearFallTimeSec;

        /// <summary>
        /// カプセル着地後の移動可能時間（秒）
        /// </summary>
        public static float CapsuleCompFallMoveTimeSec => msInstance.mCapsuleCompFallMoveTimeSec;

        /// <summary>
        /// ステージ構築モード
        /// </summary>
        public static GenerateStageMode GenerateStageMode => msInstance.mGenerateStageMode;

        /// <summary>
        /// 強制固定させるカプセル色リスト
        /// </summary>
        public static IReadOnlyList<ColorType> ForceColorTypeList => msInstance.mForceColorTypeList;


        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 読み込み
        /// </summary>
        public static void Load()
        {
            msInstance = Resources.Load<Settings>(Path.Scenes.GameScene.Settings);
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public static void Dispose()
        {
            msInstance = null;
        }

        /// <summary>
        /// 自動落下にかかる時間（秒）取得
        /// </summary>
        /// <param name="fallSpeedType"> 落下速度種別 </param>
        public static float GetAutoFallCapsuleTimeSec(FallSpeedType fallSpeedType)
        {
            return msInstance.mAutoFallCapsuleTimeSecList.ElementAtOrDefault((int)fallSpeedType);
        }
    }
}
