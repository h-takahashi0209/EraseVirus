using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// ワールド座標移動 Tween
    /// </summary>
    public sealed class TweenWorldPosition : TweenBase<Vector3>
    {
        //====================================
        //! 関数（TweenBase）
        //====================================

        /// <summary>
        /// 開始時処理
        /// </summary>
        protected override void DoBegin()
        {
            this.SetWorldPosition(FromAppliedReverse);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void DoUpdate()
        {
            this.SetWorldPosition(Vector3.Lerp(FromAppliedReverse, ToAppliedReverse, Progress));
        }

        /// <summary>
        /// 完了時処理
        /// </summary>
        protected override void DoComplete()
        {
            this.SetWorldPosition(ToAppliedReverse);
        }
    }
}
