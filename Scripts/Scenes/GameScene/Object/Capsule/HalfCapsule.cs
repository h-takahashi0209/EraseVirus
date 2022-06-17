using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// カプセルの半分 インターフェース
    /// </summary>
    public interface IHalfCapsule : IObject
    {
        /// <summary>
        /// ペアのブロックがあるXブロック座標
        /// </summary>
        public int PairedBlockPosX { get; }

        /// <summary>
        /// ペアのブロックがあるYブロック座標
        /// </summary>
        public int PairedBlockPosY { get; }

        /// <summary>
        /// ブロック座標設定
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        public void SetBlockPos(int blockPosX, int blockPosY);

        /// <summary>
        /// ブロック座標設定
        /// </summary>
        /// <param name="pos"> 座標 </param>
        public void SetLocalPosition(Vector3 pos);
    }

    /// <summary>
    /// 半カプセル
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HalfCapsule : Object, IHalfCapsule
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// オブジェクト種別
        /// </summary>
        public override ObjectType ObjectType => ObjectType.HalfCapsule;

        /// <summary>
        /// ペアのブロックがあるXブロック座標
        /// </summary>
        public int PairedBlockPosX { get; private set; } = -1;

        /// <summary>
        /// ペアのブロックがあるYブロック座標
        /// </summary>
        public int PairedBlockPosY { get; private set; } = -1;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="colorType">          色種別                           </param>
        /// <param name="blockPosX">          Xブロック座標                    </param>
        /// <param name="blockPosY">          Yブロック座標                    </param>
        /// <param name="pairedBlockPosX">    ペアになるブロックがあるX座標    </param>
        /// <param name="pairedBlockPosY">    ペアになるブロックがあるY座標    </param>
        public void Setup(ColorType colorType, int blockPosX, int blockPosY, int pairedBlockPosX, int pairedBlockPosY)
        {
            ColorType       = colorType;
            BlockPosX       = blockPosX;
            BlockPosY       = blockPosY;
            PairedBlockPosX = pairedBlockPosX;
            PairedBlockPosY = pairedBlockPosY;

            Sprite sprite;

            // ペア無し
            if (pairedBlockPosX <= 0 && pairedBlockPosY <= 0)
            {
                sprite = SpriteLoader.GetHalfCapsuleSprite(colorType);
            }
            // ペア有り
            // ペア用の画像を読み込み、ペアがある方向に回転させる
            else
            {
                sprite = SpriteLoader.GetPairedHalfCapsuleSprite(colorType);

                float angle = 0f;

                if (pairedBlockPosY > blockPosY)
                {
                    angle = 0f;
                }
                else if (pairedBlockPosY < blockPosY)
                {
                    angle = 180f;
                }
                else if (pairedBlockPosX < blockPosX)
                {
                    angle = -90f;
                }
                else if (pairedBlockPosX > blockPosX)
                {
                    angle = 90f;
                }

                UIObject.SetEulerAnglesZ(-angle);
            }

            UIObject.Setup(sprite);
        }

        /// <summary>
        /// ブロック座標設定
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        public void SetBlockPos(int blockPosX, int blockPosY)
        {
            BlockPosX = blockPosX;
            BlockPosY = blockPosY;
        }

        /// <summary>
        /// ローカル座標設定
        /// </summary>
        /// <param name="localPos"> ローカル座標 </param>
        public void SetLocalPosition(Vector3 localPos)
        {
            transform.SetLocalPosition(localPos);
        }

        /// <summary>
        /// ペアをリセット
        /// </summary>
        public void ResetPair()
        {
            PairedBlockPosX = -1;
            PairedBlockPosY = -1;

            var sprite = SpriteLoader.GetHalfCapsuleSprite(ColorType);

            UIObject.Setup(sprite);
        }
    }
}
