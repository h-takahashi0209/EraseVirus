using System;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Tween 基底
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class TweenBase<T> : ExMonoBehaviour
    {
        //====================================
        //! 変数（public）
        //====================================

        /// <summary>
        /// 所要時間（秒）
        /// </summary>
        public float DurationTimeSec;

        /// <summary>
        /// 遅延時間（秒）
        /// </summary>
        public float DelayTimeSec;

        /// <summary>
        /// 開始値
        /// </summary>
        public T From;

        /// <summary>
        /// 目標値
        /// </summary>
        public T To;


        //====================================
        //! 変数（protected）
        //====================================

        /// <summary>
        /// To -> From か
        /// </summary>
        protected bool mIsReverse;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 経過時間（秒）
        /// </summary>
        private float mElapsedTimeSec;


        //====================================
        //! プロパティ（public）
        //====================================

        /// <summary>
        /// 再生中か
        /// </summary>
        public bool IsPlaying { private get; set; }

        /// <summary>
        /// 完了時コールバック
        /// </summary>
        public Action OnComplete { private get; set; }


        //====================================
        //! プロパティ（protected）
        //====================================

        /// <summary>
        /// 開始値（IsReverse 適用）
        /// </summary>
        protected T FromAppliedReverse => mIsReverse ? To : From;

        /// <summary>
        /// 目標値（IsReverse 適用）
        /// </summary>
        protected T ToAppliedReverse => mIsReverse ? From : To;

        /// <summary>
        /// 進捗
        /// TODO : 線形補間 のみでなく、AnimationCurve 等による補間にも対応させる
        /// </summary>
        protected float Progress => Mathf.Clamp01(mElapsedTimeSec / DurationTimeSec);


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (!IsPlaying) {
                return;
            }

            mElapsedTimeSec += TimeManager.DeltaTime;

            DoUpdate();

            if (mElapsedTimeSec >= DurationTimeSec)
            {
                DoComplete();

                IsPlaying = false;

                OnComplete?.Invoke();
            }
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            DoOnDestroy();

            OnComplete = null;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 開始（From -> To）
        /// </summary>
        public void Begin()
        {
            DoBegin();

            IsPlaying       = true;
            mElapsedTimeSec = -DelayTimeSec;
            mIsReverse      = false;
        }

        /// <summary>
        /// 開始（To -> From）
        /// </summary>
        public void BeginReverse()
        {
            DoBegin();

            IsPlaying       = true;
            mElapsedTimeSec = -DelayTimeSec;
            mIsReverse      = true;
        }


        //====================================
        //! 関数（protected virtual）
        //====================================

        /// <summary>
        /// 開始時処理
        /// </summary>
        protected virtual void DoBegin() {}

        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void DoUpdate() {}

        /// <summary>
        /// 完了時処理
        /// </summary>
        protected virtual void DoComplete() {}

        /// <summary>
        /// 破棄処理
        /// </summary>
        protected virtual void DoOnDestroy() {}
    }
}
