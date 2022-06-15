using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// カプセル
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Capsule : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 1つ目の半カプセル
        /// </summary>
        [SerializeField] private HalfCapsule HalfCapsule1;

        /// <summary>
        /// 2つ目の半カプセル
        /// </summary>
        [SerializeField] private HalfCapsule HalfCapsule2;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 半カプセルリスト
        /// </summary>
        private HalfCapsule[] mHalfCapsuleList = new HalfCapsule[2];


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 半カプセルリスト
        /// </summary>
        public IReadOnlyList<IHalfCapsule> HalfCapsuleList => mHalfCapsuleList;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 向き
        /// </summary>
        public CapsuleDirection Direction { get; private set; }

        /// <summary>
        /// Xブロック座標
        /// </summary>
        public int BlockPositionX { get; set; }

        /// <summary>
        /// Yブロック座標
        /// </summary>
        public int BlockPositionY { get; set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private void Awake()
        {
            mHalfCapsuleList[0] = HalfCapsule1;
            mHalfCapsuleList[1] = HalfCapsule2;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="colorType1">    1つ目の色種別    </param>
        /// <param name="colorType2">    2つ目の色種別    </param>
        /// <param name="blockPosX">     Xブロック座標    </param>
        /// <param name="blockPosY">     Yブロック座標    </param>
        public void Setup(ColorType colorType1, ColorType colorType2, int blockPosX, int blockPosY)
        {
            Direction = CapsuleDirection.Horizontal;

            UpdatePosition(blockPosX, blockPosY);

            mHalfCapsuleList[0].Setup(colorType1, blockPosX, blockPosY, blockPosX + 1, blockPosY);
            mHalfCapsuleList[0].SetLocalPosition(Vector3.zero);

            mHalfCapsuleList[1].Setup(colorType2, blockPosX + 1, blockPosY, blockPosX, blockPosY);
            mHalfCapsuleList[1].SetLocalPosition(new Vector3(GameSceneDef.OneBlockSize, 0f, 0f));
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        public void UpdatePosition(int blockPosX, int blockPosY)
        {
            BlockPositionX = blockPosX;
            BlockPositionY = blockPosY;

            mHalfCapsuleList[0].SetBlockPos(blockPosX, blockPosY);

            if (mHalfCapsuleList[1].isActiveAndEnabled)
            {
                int pairedBlockPosX = blockPosX + (Direction == CapsuleDirection.Horizontal ? 1 : 0);
                int pairedBlockPosY = blockPosY + (Direction == CapsuleDirection.Vertial    ? 1 : 0);

                mHalfCapsuleList[1].SetBlockPos(pairedBlockPosX, pairedBlockPosY);
            }

            float posX = (BlockPositionX * GameSceneDef.OneBlockSize) + (-GameSceneDef.FieldWidth / 2f + GameSceneDef.OneBlockSize / 2f);
            float posY = (BlockPositionY * GameSceneDef.OneBlockSize) + (-GameSceneDef.FieldHeight / 2f + GameSceneDef.OneBlockSize / 2f);

            transform.SetLocalPosition(new Vector3(posX, posY, 0f));
        }

        /// <summary>
        /// 回転
        /// </summary>
        /// <param name="direction"> 回転方向 </param>
        public void Rotate(RotateDirection direction)
        {
            // 2個のブロックが連なっているカプセルのみ回転可能
            if (GetColorType(1) == ColorType.None) {
                return;
            }

            // 縦 → 横
            if (Direction == CapsuleDirection.Vertial)
            {
                // 左回転
                if (direction == RotateDirection.Left)
                {
                    var temp = mHalfCapsuleList[0];
                    mHalfCapsuleList[0] = mHalfCapsuleList[1];
                    mHalfCapsuleList[1] = temp;

                    mHalfCapsuleList[0].SetLocalPosition(Vector3.zero);
                    mHalfCapsuleList[1].SetLocalPosition(new Vector3(GameSceneDef.OneBlockSize, 0f, 0f));
                }
                // 右回転
                else if (direction == RotateDirection.Right)
                {
                    // 右端で右回転
                    // カプセルがはみ出ないよう左に移動
                    if (BlockPositionX == (GameSceneDef.BlockNumWidth - 1))
                    {
                        UpdatePosition(BlockPositionX - 1, BlockPositionY);

                        mHalfCapsuleList[0].SetLocalPosition(Vector3.zero);
                        mHalfCapsuleList[1].SetLocalPosition(new Vector3(GameSceneDef.OneBlockSize, 0f, 0f));
                    }
                    else
                    {
                        mHalfCapsuleList[1].SetLocalPosition(new Vector3(GameSceneDef.OneBlockSize, 0f, 0f));
                    }
                }

                mHalfCapsuleList[0].SetEulerAnglesZ(-90f);
                mHalfCapsuleList[1].SetEulerAnglesZ( 90f);
            }
            // 横 → 縦
            else if (Direction == CapsuleDirection.Horizontal)
            {
                // 左回転
                if (direction == RotateDirection.Left)
                {
                    mHalfCapsuleList[1].SetLocalPosition(new Vector3(0f, GameSceneDef.OneBlockSize, 0f));
                }
                // 右回転
                else if (direction == RotateDirection.Right)
                {
                    var temp = mHalfCapsuleList[0];
                    mHalfCapsuleList[0] = mHalfCapsuleList[1];
                    mHalfCapsuleList[1] = temp;

                    mHalfCapsuleList[0].SetLocalPosition(Vector3.zero);
                    mHalfCapsuleList[1].SetLocalPosition(new Vector3(0f, GameSceneDef.OneBlockSize, 0f));
                }

                mHalfCapsuleList[0].SetEulerAnglesZ(0f);
                mHalfCapsuleList[1].SetEulerAnglesZ(180f);
            }

            Direction = Direction switch
            {
                CapsuleDirection.Vertial    => CapsuleDirection.Horizontal  ,
                CapsuleDirection.Horizontal => CapsuleDirection.Vertial     ,
                _                           => Direction
            };
        }

        /// <summary>
        /// 色種別取得
        /// </summary>
        /// <param name="index"> インデックス </param>
        public ColorType GetColorType(int index)
        {
            return mHalfCapsuleList.ElementAtOrDefault(index)?.ColorType ?? ColorType.None;
        }
    }
}

