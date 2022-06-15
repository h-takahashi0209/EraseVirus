using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - ゲームクリア
    /// </summary>
    public sealed class StateGameClear : StateBase
    {
        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">       各ステートに渡す情報    </param>
        /// <param name="onComplete">    完了時コールバック      </param>
        public StateGameClear(StateContext context, Action<State, IStateInputData> onComplete) : base(context, onComplete) {}


        //====================================
        //! 関数（public override）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="inputData"> 他のステートから渡されるデータ </param>
        public override void Process(IStateInputData inputData)
        {
            var isAllClear = (mContext.Level >= CommonDef.MaxLevel);
            var telopType  = isAllClear ? UITelop.TelopType.AllClear : UITelop.TelopType.StageClear;

            mContext.UIManager.SetActivePauseButton(false);

            // テロップ再生
            mContext.UIManager.Telop.Play(telopType, () =>
            {
                // 次へ進むボタン有効化
                mContext.UIManager.EnableNextButton(() =>
                {
                    var nextState = isAllClear ? State.Finished : State.GenerateStage;

                    mContext.UIManager.Telop.Hide();

                    // 全クリアでなければレベルを増やして次のステージへ
                    // 全クリアしたら終了
                    mContext.IncLevel();
                    mOnComplete(nextState, null);
                });
            });
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Pause()
        {
            mContext.UIManager.Telop.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public override void Resume()
        {
            mContext.UIManager.Telop.Resume();
        }
    }
}
