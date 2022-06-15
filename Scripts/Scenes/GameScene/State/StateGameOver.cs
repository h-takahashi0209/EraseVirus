using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - ゲームオーバー
    /// </summary>
    public sealed class StateGameOver : StateBase
    {
        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">       各ステートに渡す情報    </param>
        /// <param name="onComplete">    完了時コールバック      </param>
        public StateGameOver(StateContext context, Action<State, IStateInputData> onComplete) : base(context, onComplete) {}


        //====================================
        //! 関数（public override）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="inputData"> 他のステートから渡されるデータ </param>
        public override void Process(IStateInputData inputData)
        {
            mContext.UIManager.Telop.Play(UITelop.TelopType.GameOver, () =>
            {
                mContext.UIManager.Telop.Hide();
                mContext.UIManager.Command.SetupGameOver(Retry, Exit);
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


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// リトライ
        /// </summary>
        private void Retry()
        {
            // レベルは変えず同じステージを再構築
            mOnComplete(State.GenerateStage, null);
        }

        /// <summary>
        /// 終了
        /// </summary>
        private void Exit()
        {
            mOnComplete(State.Finished, null);
        }
    }
}
