using System;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - カプセル消滅
    /// </summary>
    public sealed class StateDisappearCapsule : StateBase
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 他のステートから渡されるデータ
        /// </summary>
        public class InputData : IStateInputData
        {
            public IReadOnlyList<IHalfCapsule> HalfCapsuleList { get; set; }
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">       各ステートに渡す情報    </param>
        /// <param name="onComplete">    完了時コールバック      </param>
        public StateDisappearCapsule(StateContext context, Action<State, IStateInputData> onComplete) : base(context, onComplete) {}


        //====================================
        //! 関数（public override）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="inputData"> 他のステートから渡されるデータ </param>
        public override void Process(IStateInputData inputData)
        {
            var myInputData = inputData as InputData;
            var task        = new ParallelTask();

            bool isDisappear = false;

            // 4つ以上揃ったカプセルを消滅させる
            for (int i = 0; i < myInputData.HalfCapsuleList.Count; i++)
            {
                var block = myInputData.HalfCapsuleList[i];

                task.Push(onComp =>
                {
                    isDisappear |= Disappear(block.BlockPosX, block.BlockPosY, block.ColorType, CapsuleDirection.Vertial   , onComp);
                });

                task.Push(onComp =>
                {
                    isDisappear |= Disappear(block.BlockPosX, block.BlockPosY, block.ColorType, CapsuleDirection.Horizontal, onComp);
                });
            }

            task.Push(() =>
            {
                string soundName = isDisappear ? SoundDef.GameScene.Se.DisappearCapsule.ToString() : SoundDef.GameScene.Se.CompFallCapsule.ToString();

                SoundManager.PlaySe(soundName);
            });

            // 次のステートへ
            task.Process(() =>
            {
                int enemyNum  = mContext.ObjectStack.UsedObjectList.Count(obj => obj.ObjectType == ObjectType.Enemy);
                var nextState = (enemyNum <= 0) ? State.GameClear : State.FallCapsule;

                // 敵の残り数表示更新
                mContext.UIManager.Status.SetEnemyNum(enemyNum);

                // 敵の数が0になったらゲームクリア、1体以上いればカプセル落下ステートへ
                mOnComplete(nextState, null);
            });
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Pause()
        {
            mContext.ObjectStack.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public override void Resume()
        {
            mContext.ObjectStack.Resume();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 4つ揃ったカプセルを消滅させる
        /// </summary>
        /// <param name="blockPosX">           Xブロック座標             </param>
        /// <param name="blockPosY">           Yブロック座標             </param>
        /// <param name="colorType">           色種別                    </param>
        /// <param name="checkedDirection">    判定する方向              </param>
        /// <param name="onComplete">          完了時コールバック        </param>
        /// <returns>                          カプセルを消滅させたか    </returns>
        private bool Disappear(int blockPosX, int blockPosY, ColorType colorType, CapsuleDirection checkedDirection, Action onComplete)
        {
            int checkedDirectionX = checkedDirection == CapsuleDirection.Horizontal ? 1 : 0;
            int checkedDirectionY = checkedDirection == CapsuleDirection.Vertial    ? 1 : 0;

            int matchedCount = 0;

            // 指定した方向にある色が一致しているカプセルの数取得
            matchedCount += GetMatchedColorTypeNum(blockPosX, blockPosY, colorType,  checkedDirectionX,  checkedDirectionY);
            matchedCount += GetMatchedColorTypeNum(blockPosX, blockPosY, colorType, -checkedDirectionX, -checkedDirectionY);

            // 4つ以上揃ったら消滅させる
            if (matchedCount >= (GameSceneDef.DisappearBlockMatchCount - 1))
            {
                int checkedBlockPosX = blockPosX + checkedDirectionX;
                int checkedBlockPosY = blockPosY + checkedDirectionY;

                var task = new ParallelTask();

                task.Push(onComp => mContext.ObjectStack.Disappear(blockPosX, blockPosY, onComp));
                task.Push(onComp => Disappear(blockPosX, blockPosY, colorType,  checkedDirectionX,  checkedDirectionY, onComp));
                task.Push(onComp => Disappear(blockPosX, blockPosY, colorType, -checkedDirectionX, -checkedDirectionY, onComp));

                task.Process(onComplete);

                return true;
            }
            else
            {
                onComplete();

                return false;
            }
        }

        /// <summary>
        /// 指定した方向にある色が一致するカプセルの数取得
        /// </summary>
        /// <param name="blockPosX">            Xブロック座標    </param>
        /// <param name="blockPosY">            Yブロック座標    </param>
        /// <param name="colorType">            色種別           </param>
        /// <param name="checkedDirectionX">    X判定方向        </param>
        /// <param name="checkedDirectionY">    Y判定方向        </param>
        private int GetMatchedColorTypeNum(int blockPosX, int blockPosY, ColorType colorType, int checkedDirectionX, int checkedDirectionY)
        {
            int checkedBlockPosX = blockPosX + checkedDirectionX;
            int checkedBlockPosY = blockPosY + checkedDirectionY;

            int matchedCount = 0;

            while (true)
            {
                if (colorType == mContext.ObjectStack.GetColorType(checkedBlockPosX, checkedBlockPosY))
                {
                    checkedBlockPosX += checkedDirectionX;
                    checkedBlockPosY += checkedDirectionY;

                    matchedCount++;
                }
                else
                {
                    break;
                }
            }

            return matchedCount;
        }

        /// <summary>
        /// 指定した方向にある色が一致するカプセルを消滅させる
        /// </summary>
        /// <param name="blockPosX">            Xブロック座標         </param>
        /// <param name="blockPosY">            Yブロック座標         </param>
        /// <param name="colorType">            色種別                </param>
        /// <param name="checkedDirectionX">    X判定方向             </param>
        /// <param name="checkedDirectionY">    Y判定方向             </param>
        /// <param name="onComplete">           完了時コールバック    </param>
        private void Disappear(int blockPosX, int blockPosY, ColorType colorType, int checkedDirectionX, int checkedDirectionY, Action onComplete)
        {
            int checkedBlockPosX = blockPosX + checkedDirectionX;
            int checkedBlockPosY = blockPosY + checkedDirectionY;

            var task = new ParallelTask();

            while (true)

            {
                if (colorType == mContext.ObjectStack.GetColorType(checkedBlockPosX, checkedBlockPosY))
                {
                    int _checkedBlockPosX = checkedBlockPosX;
                    int _checkedBlockPosY = checkedBlockPosY;

                    task.Push(onComp => mContext.ObjectStack.Disappear(_checkedBlockPosX, _checkedBlockPosY, onComp));

                    checkedBlockPosX += checkedDirectionX;
                    checkedBlockPosY += checkedDirectionY;
                }
                else
                {
                    break;
                }
            }

            task.Process(onComplete);
        }
    }
}
