using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace TakahashiH
{
    /// <summary>
    /// 受け取ったタスクを非同期実行する
    /// </summary>
    public sealed class ParallelTask
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// タスクキュー
        /// </summary>
        private List<Action<Action>> mTaskQueue = new List<Action<Action>>();


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// タスク追加（完了時コールバック無し）
        /// </summary>
        /// <param name="task"> タスク </param>
        public void Push(Action task)
        {
            mTaskQueue.Add(onNext =>
            {
                task();
                onNext();
            });
        }

        /// <summary>
        /// タスク追加（完了時コールバック有り）
        /// </summary>
        /// <param name="task"> タスク </param>
        public void Push(Action<Action> task)
        {
            mTaskQueue.Add(task);
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
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Process(Action onComplete = null)
        {
            int compTaskNum = 0;

            if (mTaskQueue.Count == 0)
            {
                onComplete?.Invoke();
                return;
            }

            for (int i = 0; i < mTaskQueue.Count; i++)
            {
                mTaskQueue[i](() =>
                {
                    compTaskNum++;

                    if(compTaskNum >= mTaskQueue.Count)
                    {
                        onComplete?.Invoke();
                    }
                });
            }
        }
    }
}
