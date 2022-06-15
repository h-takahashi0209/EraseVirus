using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// オブジェクトのスタック
    /// 落下しきったカプセルや敵を管理する
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ObjectStack : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 半カプセルリスト
        /// </summary>
        [SerializeField] private HalfCapsule[] HalfCapsuleList;

        /// <summary>
        /// 敵リスト
        /// </summary>
        [SerializeField] private Enemy[] EnemyList;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 未使用のオブジェクトリスト
        /// </summary>
        private List<Object> mUnusedObjectList = new List<Object>();

        /// <summary>
        /// 使用中のオブジェクトリスト
        /// </summary>
        private List<Object> mUsedObjectList = new List<Object>();


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 使用中のオブジェクトリスト
        /// </summary>
        public IReadOnlyList<IObject> UsedObjectList => mUsedObjectList;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            HalfCapsuleList = GetComponentsInChildren<HalfCapsule>(true).ToArray();
            EnemyList       = GetComponentsInChildren<Enemy>(true).ToArray();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            int blockMax = GameSceneDef.BlockNumWidth * GameSceneDef.BlockNumHeight;

            for (int i = 0; i < blockMax; i++)
            {
                HalfCapsuleList[i]  .SetActive(false);
                EnemyList[i]        .SetActive(false);

                mUnusedObjectList.Add(HalfCapsuleList[i]);
                mUnusedObjectList.Add(EnemyList[i]);
            }

            mUsedObjectList.Clear();
        }

        /// <summary>
        /// スタックをクリア
        /// </summary>
        public void ClearStack()
        {
            for (int i = 0; i < mUsedObjectList.Count; i++)
            {
                var usedObj = mUsedObjectList[i];

                usedObj.SetActive(false);

                mUnusedObjectList.Add(usedObj);
                mUsedObjectList.Remove(usedObj);
            }
        }

        /// <summary>
        /// 半カプセル追加
        /// </summary>
        /// <param name="blockPosX">          Xブロック座標                </param>
        /// <param name="blockPosY">          Yブロック座標                </param>
        /// <param name="colorType">          色種別                       </param>
        /// <param name="pairedBlockPosX">    ペアになるブロックのX座標    </param>
        /// <param name="pairedBlockPosY">    ペアになるブロックのY座標    </param>
        public void PushHalfCapsule(int blockPosX, int blockPosY, ColorType colorType, int pairedBlockPosX, int pairedBlockPosY)
        {
            Push(blockPosX, blockPosY, colorType, ObjectType.HalfCapsule, pairedBlockPosX, pairedBlockPosY);
        }

        /// <summary>
        /// 敵追加
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標     </param>
        /// <param name="blockPosY">    Yブロック座標     </param>
        /// <param name="colorType">    色種別            </param>
        public void PushEnemy(int blockPosX, int blockPosY, ColorType colorType)
        {
            Push(blockPosX, blockPosY, colorType, ObjectType.Enemy, 0, 0);
        }

        /// <summary>
        /// 消滅させる
        /// </summary>
        /// <param name="blockPosX">     Xブロック座標         </param>
        /// <param name="blockPosY">     Yブロック座標         </param>
        /// <param name="onComplete">    完了時コールバック    </param>
        public void Disappear(int blockPosX, int blockPosY, Action onComplete)
        {
            if (blockPosX < 0 || blockPosX >= GameSceneDef.BlockNumWidth) {
                return;
            }

            if (blockPosY < 0 || blockPosY >= GameSceneDef.BlockNumHeight) {
                return;
            }

            Object targetObj = null;

            for (int i = 0; i < mUsedObjectList.Count; i++)
            {
                var usedObj = mUsedObjectList[i];

                if (usedObj.BlockPosX == blockPosX && usedObj.BlockPosY == blockPosY)
                {
                    usedObj.PlayDisappearAnimation(() =>
                    {
                        usedObj.SetActive(false);
                        onComplete();
                    });

                    mUnusedObjectList.Add(usedObj);
                    mUsedObjectList.Remove(usedObj);

                    targetObj = usedObj;

                    break;
                }
            }

            if (targetObj == null) {
                return;
            }

            var targetHalfCapsule = targetObj as HalfCapsule;

            // ペアの半カプセルがあればペアを解除する
            if (targetHalfCapsule != null && (targetHalfCapsule.PairedBlockPosX > 0 || targetHalfCapsule.PairedBlockPosY > 0))
            {
                for (int i = 0; i < mUsedObjectList.Count; i++)
                {
                    var pairedObj = mUsedObjectList[i];

                    if (pairedObj is HalfCapsule && pairedObj.BlockPosX == targetHalfCapsule.PairedBlockPosX && pairedObj.BlockPosY == targetHalfCapsule.PairedBlockPosY)
                    {
                        var pairedHalfCapsule = pairedObj as HalfCapsule;

                        pairedHalfCapsule.ResetPair();

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 指定された箇所にあるブロックの色種別取得
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        public ColorType GetColorType(int blockPosX, int blockPosY)
        {
            for (int i = 0; i < mUsedObjectList.Count; i++)
            {
                var usedBlock = mUsedObjectList[i];

                if (usedBlock.BlockPosX == blockPosX && usedBlock.BlockPosY == blockPosY)
                {
                    return usedBlock.ColorType;
                }
            }

            return ColorType.None;
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            for (int i = 0; i < mUsedObjectList.Count; i++)
            {
                mUsedObjectList[i].Pause();
            }
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            for (int i = 0; i < mUsedObjectList.Count; i++)
            {
                mUsedObjectList[i].Resume();
            }
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="blockPosX">          Xブロック座標                </param>
        /// <param name="blockPosY">          Yブロック座標                </param>
        /// <param name="colorType">          色種別                       </param>
        /// <param name="objectType">         オブジェクト種別             </param>
        /// <param name="pairedBlockPosX">    ペアになるブロックのX座標    </param>
        /// <param name="pairedBlockPosY">    ペアになるブロックのY座標    </param>
        private void Push(int blockPosX, int blockPosY, ColorType colorType, ObjectType objectType, int pairedBlockPosX, int pairedBlockPosY)
        {
            var emptyObjIdx = mUnusedObjectList.FindIndex(obj => obj.ObjectType == objectType);
            if (emptyObjIdx < 0)
            {
                Debug.LogWarning("emptyObjIdx < 0");
                return;
            }

            var emptyObj = mUnusedObjectList[emptyObjIdx];

            float localPosX = (blockPosX * GameSceneDef.OneBlockSize) + (-GameSceneDef.FieldWidth  / 2f + GameSceneDef.OneBlockSize / 2f);
            float localPosY = (blockPosY * GameSceneDef.OneBlockSize) + (-GameSceneDef.FieldHeight / 2f + GameSceneDef.OneBlockSize / 2f);

            emptyObj.SetActive(true);
            emptyObj.SetLocalPosition(new Vector3(localPosX, localPosY, 0f));

            switch (objectType)
            {
                case ObjectType.HalfCapsule : (emptyObj as HalfCapsule) .Setup(colorType, blockPosX, blockPosY, pairedBlockPosX, pairedBlockPosY);  break;
                case ObjectType.Enemy       : (emptyObj as Enemy)       .Setup(colorType, blockPosX, blockPosY);                                    break;
            }

            mUnusedObjectList.RemoveAt(emptyObjIdx);

            mUsedObjectList.Add(emptyObj);
        }
    }
}

