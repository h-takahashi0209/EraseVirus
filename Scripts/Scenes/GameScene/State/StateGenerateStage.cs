using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - ステージ構築
    /// </summary>
    public sealed class StateGenerateStage : StateBase
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// ステージローダー
        /// </summary>
        private StageLoader mStageLoader;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">        各ステートに渡す情報    </param>
        /// <param name="onComplete">     完了時コールバック      </param>
        /// <param name="stageLoader">    ステージローダー        </param>
        public StateGenerateStage(StateContext context, Action<State, IStateInputData> onComplete, StageLoader stageLoader) : base(context, onComplete)
        {
            mStageLoader = stageLoader;
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
            // ステージを読み込んでオブジェクトをスタックに追加
            if (!mStageLoader.Setup(StageLoader.Mode.Random, mContext.Level, mContext.ObjectStack)) {
                return;
            }

            int enemyNum = mContext.ObjectStack.UsedObjectList.Count(obj => obj.ObjectType == ObjectType.Enemy);

            // ステータス設定
            mContext.UIManager.Status.SetLevel(mContext.Level);
            mContext.UIManager.Status.SetFallSpeed(mContext.FallSpeedType);
            mContext.UIManager.Status.SetEnemyNum(enemyNum);

            var nextStateIputData = new StateAppearCapsule.InputData(){IsFirst = true};

            // カプセル出現ステートへ
            mOnComplete(State.AppearCapsule, nextStateIputData);
        }
    }
}
