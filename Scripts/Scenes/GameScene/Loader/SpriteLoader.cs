using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// スプライトローダー
    /// </summary>
    public sealed class SpriteLoader
    {
        //====================================
        //! 変数（private static）
        //====================================

        /// <summary>
        /// インスタンス
        /// </summary>
        private static SpriteLoader msInstance;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 半カプセルのスプライトリスト
        /// </summary>
        private Sprite[] mHalfCapsuleSpriteList = new Sprite[(int)ColorType.Sizeof];

        /// <summary>
        /// ペアの半カプセルのスプライトリスト
        /// </summary>
        private Sprite[] mPairedHalfCapsuleSpriteList = new Sprite[(int)ColorType.Sizeof];

        /// <summary>
        /// 敵のスプライトリスト
        /// </summary>
        private Sprite[] mEnemySpriteList = new Sprite[(int)ColorType.Sizeof];


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// インスタンス生成
        /// </summary>
        public static void CreateInstance()
        {
            msInstance = new SpriteLoader();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public static void Dispose()
        {
            for (int i = 0; i < (int)ColorType.Sizeof; i++)
            {
                msInstance.mHalfCapsuleSpriteList[i]        = null;
                msInstance.mPairedHalfCapsuleSpriteList[i]  = null;
                msInstance.mEnemySpriteList[i]              = null;
            }

            msInstance = null;
        }

        /// <summary>
        /// 読み込み
        /// </summary>
        public static void Load()
        {
            for (int i = 0; i < (int)ColorType.Sizeof; i++)
            {
                string halfCapsulePath       = Path.Scenes.GameScene.HalfCapsuleImage       + ((ColorType)i).ToString();
                string pairedHalfCapsulePath = Path.Scenes.GameScene.PairedHalfCapsuleImage + ((ColorType)i).ToString();
                string enemyPath             = Path.Scenes.GameScene.EnemyImage             + ((ColorType)i).ToString();

                msInstance.mHalfCapsuleSpriteList[i]        = Resources.Load<Sprite>(halfCapsulePath);
                msInstance.mPairedHalfCapsuleSpriteList[i]  = Resources.Load<Sprite>(pairedHalfCapsulePath);
                msInstance.mEnemySpriteList[i]              = Resources.Load<Sprite>(enemyPath);
            }
        }

        /// <summary>
        /// 半カプセルのスプライト取得
        /// </summary>
        /// <param name="colorType"> 色種別 </param>
        public static Sprite GetHalfCapsuleSprite(ColorType colorType)
        {
            return msInstance.mHalfCapsuleSpriteList.ElementAtOrDefault((int)colorType);
        }

        /// <summary>
        /// ペアの半カプセルのスプライト取得
        /// </summary>
        /// <param name="colorType"> 色種別 </param>
        public static Sprite GetPairedHalfCapsuleSprite(ColorType colorType)
        {
            return msInstance.mPairedHalfCapsuleSpriteList.ElementAtOrDefault((int)colorType);
        }

        /// <summary>
        /// 敵のスプライト取得
        /// </summary>
        /// <param name="colorType"> 色種別 </param>
        public static Sprite GetEnemySprite(ColorType colorType)
        {
            return msInstance.mEnemySpriteList.ElementAtOrDefault((int)colorType);
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private SpriteLoader() {}
    }
}

