using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート インターフェース
    /// </summary>
    public interface IState : IDisposable
    {
        /// <summary>
        /// 実行
        /// </summary>
        void Process(IStateInputData inputData);

        /// <summary>
        /// 更新
        /// </summary>
        void DoUpdate();

        /// <summary>
        /// 一時停止
        /// </summary>
        void Pause();

        /// <summary>
        /// 再開
        /// </summary>
        void Resume();
    }

    /// <summary>
    /// 他のステートから渡されるデータ インターフェース
    /// </summary>
    public interface IStateInputData {}

    /// <summary>
    /// ステート規定
    /// </summary>
    public abstract class StateBase : IState
    {
        //====================================
        //! 変数（protected）
        //====================================

        /// <summary>
        /// 各ステートに渡す情報
        /// </summary>
        protected StateContext mContext;

        /// <summary>
        /// 完了時コールバック
        /// </summary>
        protected Action<State, IStateInputData> mOnComplete;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">       各ステートに渡す情報    </param>
        /// <param name="onComplete">    完了時コールバック      </param>
        public StateBase(StateContext context, Action<State, IStateInputData> onComplete)
        {
            mContext    = context;
            mOnComplete = onComplete;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            mOnComplete = null;

            DoDispose();
        }


        //====================================
        //! 関数（public virtual）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="inputData"> 他のステートから渡されるデータ </param>
        public abstract void Process(IStateInputData inputData);

        /// <summary>
        /// 更新
        /// </summary>
        public virtual void DoUpdate() {}

        /// <summary>
        /// 一時停止
        /// </summary>
        public virtual void Pause() {}

        /// <summary>
        /// 再開
        /// </summary>
        public virtual void Resume() {}


        //====================================
        //! 関数（protected virtual）
        //====================================

        /// <summary>
        /// 破棄
        /// </summary>
        public virtual void DoDispose() {}
    }
}

