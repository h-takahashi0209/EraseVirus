using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// ローカル座標移動 Tween
    /// </summary>
    public sealed class TweenLocalPosition : TweenBase<Vector3>
    {
        //====================================
        //! 関数（TweenBase）
        //====================================

        /// <summary>
        /// 開始時処理
        /// </summary>
        protected override void DoBegin()
        {
            this.SetLocalPosition(FromAppliedReverse);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void DoUpdate()
        {
            this.SetLocalPosition(Vector3.Lerp(FromAppliedReverse, ToAppliedReverse, Progress));
        }

        /// <summary>
        /// 完了時処理
        /// </summary>
        protected override void DoComplete()
        {
            this.SetLocalPosition(ToAppliedReverse);
        }
    }
}
