using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace TakahashiH
{
    /// <summary>
    /// 受け取ったタスクを同期処理する
    /// </summary>
    public sealed class StepTask
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// タスクキュー
        /// </summary>
        private Queue<Action<Action>> mTaskQueue = new Queue<Action<Action>>();


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// タスク追加（完了時コールバック無し）
        /// </summary>
        /// <param name="action"> タスク </param>
        public void Push(Action action)
        {
            mTaskQueue.Enqueue(onNext =>
            {
                action();
                onNext();
            });
        }

        /// <summary>
        /// タスク追加（完了時コールバック有り）
        /// </summary>
        /// <param name="action"> タスク </param>
        public void Push(Action<Action> action)
        {
            mTaskQueue.Enqueue(action);
        }

        /// <summary>
        /// クリア
        /// </summary>
        public void Clear()
        {
            mTaskQueue.Clear();
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        public void Process(Action onComplete = null)
        {
            _Process(onComplete);
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        [DebuggerHidden]
        [DebuggerStepThrough]
        private void _Process(Action onComplete)
        {
            var action = mTaskQueue.Dequeue();

            action(() =>
            {
                if(mTaskQueue.Count > 0)
                {
                    _Process(onComplete);
                }
                else
                {
                    onComplete?.Invoke();
                }
            });
        }
    }
}
