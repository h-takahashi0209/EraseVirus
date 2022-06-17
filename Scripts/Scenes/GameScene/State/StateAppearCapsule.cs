using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - カプセル出現
    /// </summary>
    public sealed class StateAppearCapsule : StateBase
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 開始までの待機時間（秒）
        /// </summary>
        private const float WaitTimeSec = 1f;

        /// <summary>
        /// 他のステートから渡されるデータ
        /// </summary>
        public class InputData : IStateInputData
        {
            public bool IsFirst { get; set; }
        }


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// タイマー
        /// </summary>
        private Timer mTimer = new Timer();

        /// <summary>
        /// 1つ目の半カプセルの色種別
        /// </summary>
        private ColorType mColorType1 = ColorType.None;

        /// <summary>
        /// 2つ目の半カプセルの色種別
        /// </summary>
        private ColorType mColorType2 = ColorType.None;

        /// <summary>
        /// 次に出現するカプセル UI
        /// </summary>
        private UINextCapsule mUINextCapsule;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">          各ステートに渡す情報       </param>
        /// <param name="onComplete">       完了時コールバック         </param>
        /// <param name="uiNextCapsule">    次に出現するカプセル UI    </param>
        public StateAppearCapsule(StateContext context, Action<State, IStateInputData> onComplete, UINextCapsule uiNextCapsule) : base(context, onComplete)
        {
            mUINextCapsule = uiNextCapsule;
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
            var myInputData = inputData as InputData;

            var task = new StepTask();

            mContext.CurrentCapsule.SetActive(false);

            // 初回はカプセル抽選 + 一定時間待機
            if (myInputData.IsFirst)
            {
                task.Push(onNext =>
                {
                    LotteryNextCapsule();

                    mTimer.Begin(WaitTimeSec, () =>
                    {
                        mContext.UIManager.SetActivePauseButton(true);
                        onNext();
                    });
                });
            }

            // 出現させるカプセルのセットアップ
            task.Push(() => SetupCurrentCapsule());

            // カプセル出現アニメーション再生
            task.Push(onNext => mUINextCapsule.PlayAppearAnimation(onNext));

            // 出現したカプセルをセットアップし次のステートへ
            // 次のカプセルを抽選してカプセル移動ステートへ
            task.Process(() =>
            {
                int blockPosX = GameSceneDef.BlockNumWidth  / 2 - 1;
                int blockPosY = GameSceneDef.BlockNumHeight - 1;
                var nextState = (mContext.ObjectStack.GetColorType(blockPosX, blockPosY) != ColorType.None) ? State.GameOver : State.MoveCapsule;

                LotteryNextCapsule();

                mContext.CurrentCapsule.SetActive(true);

                // 出現したカプセルの位置に既にカプセルがあればゲームオーバー
                // 無ければカプセル移動のステートへ
                mOnComplete(nextState, null);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void DoUpdate()
        {
            mTimer.UpdateTimer(TimeManager.DeltaTime);
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Pause()
        {
            mUINextCapsule  .Pause();
            mTimer          .Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public override void Resume()
        {
            mUINextCapsule  .Resume();
            mTimer          .Resume();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 次に出現させるカプセルを抽選
        /// </summary>
        private void LotteryNextCapsule()
        {
            mColorType1 = Settings.ForceColorTypeList.ElementAtOrDefault(0) != ColorType.None ? Settings.ForceColorTypeList.ElementAtOrDefault(0) : (ColorType)UnityEngine.Random.Range(0, (int)ColorType.Sizeof);
            mColorType2 = Settings.ForceColorTypeList.ElementAtOrDefault(1) != ColorType.None ? Settings.ForceColorTypeList.ElementAtOrDefault(1) : (ColorType)UnityEngine.Random.Range(0, (int)ColorType.Sizeof);

            var sprite1 = SpriteLoader.GetPairedHalfCapsuleSprite(mColorType1);
            var sprite2 = SpriteLoader.GetPairedHalfCapsuleSprite(mColorType2);

            mUINextCapsule.Setup(sprite1, sprite2);
        }

        /// <summary>
        /// 出現したカプセルをセットアップ
        /// </summary>
        private void SetupCurrentCapsule()
        {
            int blockPosX = GameSceneDef.BlockNumWidth  / 2 - 1;
            int blockPosY = GameSceneDef.BlockNumHeight - 1;

            mContext.CurrentCapsule.Setup(mColorType1, mColorType2, blockPosX, blockPosY);
        }
    }
}
