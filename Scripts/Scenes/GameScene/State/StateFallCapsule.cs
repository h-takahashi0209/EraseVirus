using System;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - カプセル落下
    /// </summary>
    public sealed class StateFallCapsule : StateBase
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 落下させる半カプセルの最大グループ数
        /// </summary>
        private const int FellHalfCapsuleGroupMax = 8;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 落下させる半カプセルリスト
        /// </summary>
        private List<IHalfCapsule>[] mFellHalfCapsuleList = new List<IHalfCapsule>[FellHalfCapsuleGroupMax];

        /// <summary>
        /// 落下が完了した半カプセルリスト
        /// </summary>
        private List<IHalfCapsule> mCompFallHalfCapsuleList = new List<IHalfCapsule>();

        /// <summary>
        /// カプセルの自動落下時間用タイマー
        /// </summary>
        private Timer mAutoFallTimer = new Timer();


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">       各ステートに渡す情報    </param>
        /// <param name="onComplete">    完了時コールバック      </param>
        public StateFallCapsule(StateContext context, Action<State, IStateInputData> onComplete) : base(context, onComplete)
        {
            for (int i = 0; i < FellHalfCapsuleGroupMax; i++)
            {
                mFellHalfCapsuleList[i] = new List<IHalfCapsule>();
            }
        }


        //====================================
        //! 関数（public override）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="inputData"> 他のステートから渡されるデータ </param>
        public override void Process(IStateInputData inputData)
        {
            for (int i = 0; i < FellHalfCapsuleGroupMax; i++)
            {
                mFellHalfCapsuleList[i].Clear();
            }

            mCompFallHalfCapsuleList.Clear();

            var usedObjectList = mContext.ObjectStack.UsedObjectList;

            int listIdx = 0;

            // 落下対象のカプセルを取得
            for (int i = 0; i < usedObjectList.Count; i++)
            {
                var usedHalfCapsule = usedObjectList[i] as HalfCapsule;

                // ペアがない半カプセル
                if (usedHalfCapsule != null && usedHalfCapsule.BlockPosY > 0 && usedHalfCapsule.PairedBlockPosX <= 0 && usedHalfCapsule.PairedBlockPosY <= 0)
                {
                    // かつ1つ下に何もなければ落下対象
                    if (mContext.ObjectStack.GetColorType(usedHalfCapsule.BlockPosX, usedHalfCapsule.BlockPosY - 1) == ColorType.None)
                    {
                        AddFellHalfCapsule(usedHalfCapsule, mFellHalfCapsuleList[listIdx]);

                        listIdx++;
                    }
                }
            }

            // 落下対象のカプセルが1つでもあれば落下開始
            if (listIdx > 0)
            {
                mAutoFallTimer.Begin(Settings.CapsuleDisappearFallTimeSec, () => FallCapsule());
            }
            // なければ新しいカプセル出現
            else
            {
                var nextStateIputData = new StateAppearCapsule.InputData(){IsFirst = false};

                mOnComplete(State.AppearCapsule, nextStateIputData);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void DoUpdate()
        {
            mAutoFallTimer.UpdateTimer(TimeManager.DeltaTime);
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public override void DoDispose()
        {
            mAutoFallTimer.Dispose();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Pause()
        {
            mAutoFallTimer.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public override void Resume()
        {
            mAutoFallTimer.Resume();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 対象のカプセルを落下させる
        /// </summary>
        private void FallCapsule()
        {
            bool isCompFallAll = true;

            for (int i = 0; i < FellHalfCapsuleGroupMax; i++)
            {
                if (mFellHalfCapsuleList[i].Count <= 0) {
                    continue;
                }

                bool isCompFall = false;

                for (int j = 0; j < mFellHalfCapsuleList[i].Count; j++)
                {
                    var halfCapsule = mFellHalfCapsuleList[i][j];

                    int blockPosX = halfCapsule.BlockPosX;
                    int blockPosY = halfCapsule.BlockPosY - 1;

                    float localPosX = (blockPosX * GameSceneDef.OneBlockSize) + (-GameSceneDef.FieldWidth / 2f + GameSceneDef.OneBlockSize / 2f);
                    float localPosY = (blockPosY * GameSceneDef.OneBlockSize) + (-GameSceneDef.FieldHeight / 2f + GameSceneDef.OneBlockSize / 2f);

                    halfCapsule.SetBlockPos(blockPosX, blockPosY);
                    halfCapsule.SetLocalPosition(new Vector3(localPosX, localPosY, 0f));

                    // 一番下の半カプセルが着地したら落下完了とする
                    if (halfCapsule.BlockPosY <= 0 || mContext.ObjectStack.GetColorType(halfCapsule.BlockPosX, halfCapsule.BlockPosY - 1) != ColorType.None)
                    {
                        isCompFall = true;
                    }
                }

                if (isCompFall)
                {
                    mCompFallHalfCapsuleList.AddRange(mFellHalfCapsuleList[i]);
                    mFellHalfCapsuleList[i].Clear();
                }
                else
                {
                    isCompFallAll = false;
                }
            }

            // 全てのカプセルが着地したらカプセル消滅ステートへ遷移
            if (isCompFallAll)
            {
                var inputData = new StateDisappearCapsule.InputData()
                {
                    HalfCapsuleList = mCompFallHalfCapsuleList
                };

                mOnComplete(State.DisappearCapsule, inputData);
            }
            else
            {
                mAutoFallTimer.Begin(Settings.CapsuleDisappearFallTimeSec, () => FallCapsule());

                SoundManager.PlaySe(SoundDef.GameScene.Se.MoveCapsule.ToString());
            }
        }

        /// <summary>
        /// 指定した半カプセルとそれに追従して落下させる半カプセルをリストに追加
        /// </summary>
        /// <param name="halfCapsule">        半カプセル          </param>
        /// <param name="halfCapsuleList">    半カプセルリスト    </param>
        private void AddFellHalfCapsule(IHalfCapsule halfCapsule, List<IHalfCapsule> halfCapsuleList)
        {
            // 指定した半カプセル
            if (halfCapsuleList.NotAny(b => b.BlockPosX == halfCapsule.BlockPosX && b.BlockPosY == halfCapsule.BlockPosY))
            {
                halfCapsuleList.Add(halfCapsule);
            }
            else
            {
                return;
            }

            // ペアの半カプセル
            if (halfCapsule.PairedBlockPosX > 0 || halfCapsule.PairedBlockPosY > 0)
            {
                var pairedHalfCapsule = GetUsedHalfCapsule(halfCapsule.PairedBlockPosX, halfCapsule.PairedBlockPosY);
                if (pairedHalfCapsule != null)
                {
                    // 1つ下に半カプセルがなければ追加
                    if (mContext.ObjectStack.GetColorType(pairedHalfCapsule.BlockPosX, pairedHalfCapsule.BlockPosY - 1) == ColorType.None)
                    {
                        AddFellHalfCapsule(pairedHalfCapsule, halfCapsuleList);
                    }
                }
            }

            // 1つ上にある半カプセル
            var topHalfCapsule = GetUsedHalfCapsule(halfCapsule.BlockPosX, halfCapsule.BlockPosY + 1);
            if (topHalfCapsule != null)
            {
                // ペアがなければ追加
                if (topHalfCapsule.PairedBlockPosX <= 0 && topHalfCapsule.PairedBlockPosY <= 0)
                {
                    AddFellHalfCapsule(topHalfCapsule, halfCapsuleList);
                }
                // ペアがある
                else
                {
                    var topPairedHalfCapsule = GetUsedHalfCapsule(topHalfCapsule.PairedBlockPosX, topHalfCapsule.PairedBlockPosY);
                    if (topPairedHalfCapsule != null)
                    {
                        // 1つ下に半カプセルがなければ追加
                        if (mContext.ObjectStack.GetColorType(topPairedHalfCapsule.BlockPosX, topPairedHalfCapsule.BlockPosY - 1) == ColorType.None)
                        {
                            AddFellHalfCapsule(topHalfCapsule, halfCapsuleList);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 使用中の半カプセル取得
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        private IHalfCapsule GetUsedHalfCapsule(int blockPosX, int blockPosY)
        {
            var usedObjectList = mContext.ObjectStack.UsedObjectList;

            for (int i = 0; i < usedObjectList.Count; i++)
            {
                var usedHalfCapsule = usedObjectList[i] as IHalfCapsule;

                if (usedHalfCapsule != null && usedHalfCapsule.BlockPosX == blockPosX && usedHalfCapsule.BlockPosY == blockPosY)
                {
                    return usedHalfCapsule;
                }
            }

            return null;
        }
    }
}
