using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// スケール拡縮 Tween
    /// </summary>
    public sealed class TweenScale : TweenBase<Vector3>
    {
        //====================================
        //! 関数（TweenBase）
        //====================================

        /// <summary>
        /// 開始時処理
        /// </summary>
        protected override void DoBegin()
        {
            this.SetLocalScale(FromAppliedReverse);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void DoUpdate()
        {
            this.SetLocalScale(Vector3.Lerp(FromAppliedReverse, ToAppliedReverse, Progress));
        }

        /// <summary>
        /// 完了時処理
        /// </summary>
        protected override void DoComplete()
        {
            this.SetLocalScale(ToAppliedReverse);
        }
    }
}
