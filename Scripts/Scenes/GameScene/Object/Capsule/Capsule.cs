using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// �J�v�Z��
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Capsule : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// 1�ڂ̔��J�v�Z��
        /// </summary>
        [SerializeField] private HalfCapsule HalfCapsule1;

        /// <summary>
        /// 2�ڂ̔��J�v�Z��
        /// </summary>
        [SerializeField] private HalfCapsule HalfCapsule2;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ���J�v�Z�����X�g
        /// </summary>
        private HalfCapsule[] mHalfCapsuleList = new HalfCapsule[2];


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ���J�v�Z�����X�g
        /// </summary>
        public IReadOnlyList<IHalfCapsule> HalfCapsuleList => mHalfCapsuleList;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ����
        /// </summary>
        public CapsuleDirection Direction { get; private set; }

        /// <summary>
        /// X�u���b�N���W
        /// </summary>
        public int BlockPositionX { get; set; }

        /// <summary>
        /// Y�u���b�N���W
        /// </summary>
        public int BlockPositionY { get; set; }


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private void Awake()
        {
            mHalfCapsuleList[0] = HalfCapsule1;
            mHalfCapsuleList[1] = HalfCapsule2;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="colorType1">    1�ڂ̐F���    </param>
        /// <param name="colorType2">    2�ڂ̐F���    </param>
        /// <param name="blockPosX">     X�u���b�N���W    </param>
        /// <param name="blockPosY">     Y�u���b�N���W    </param>
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
        /// �ړ�
        /// </summary>
        /// <param name="blockPosX">    X�u���b�N���W    </param>
        /// <param name="blockPosY">    Y�u���b�N���W    </param>
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
        /// ��]
        /// </summary>
        /// <param name="direction"> ��]���� </param>
        public void Rotate(RotateDirection direction)
        {
            // 2�̃u���b�N���A�Ȃ��Ă���J�v�Z���̂݉�]�\
            if (GetColorType(1) == ColorType.None) {
                return;
            }

            // �c �� ��
            if (Direction == CapsuleDirection.Vertial)
            {
                // ����]
                if (direction == RotateDirection.Left)
                {
                    var temp = mHalfCapsuleList[0];
                    mHalfCapsuleList[0] = mHalfCapsuleList[1];
                    mHalfCapsuleList[1] = temp;

                    mHalfCapsuleList[0].SetLocalPosition(Vector3.zero);
                    mHalfCapsuleList[1].SetLocalPosition(new Vector3(GameSceneDef.OneBlockSize, 0f, 0f));
                }
                // �E��]
                else if (direction == RotateDirection.Right)
                {
                    // �E�[�ŉE��]
                    // �J�v�Z�����͂ݏo�Ȃ��悤���Ɉړ�
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
            // �� �� �c
            else if (Direction == CapsuleDirection.Horizontal)
            {
                // ����]
                if (direction == RotateDirection.Left)
                {
                    mHalfCapsuleList[1].SetLocalPosition(new Vector3(0f, GameSceneDef.OneBlockSize, 0f));
                }
                // �E��]
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
        /// �F��ʎ擾
        /// </summary>
        /// <param name="index"> �C���f�b�N�X </param>
        public ColorType GetColorType(int index)
        {
            return mHalfCapsuleList.ElementAtOrDefault(index)?.ColorType ?? ColorType.None;
        }
    }
}

