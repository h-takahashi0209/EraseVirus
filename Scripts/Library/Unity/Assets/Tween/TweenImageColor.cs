using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH
{
    /// <summary>
    /// 画像の色変更 Tween
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class TweenImageColor : TweenBase<Color>
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 画像
        /// </summary>
        [SerializeField] private Image Image;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Image = GetComponent<Image>();
        }


        //====================================
        //! 関数（TweenBase）
        //====================================

        /// <summary>
        /// 開始時処理
        /// </summary>
        protected override void DoBegin()
        {
            Image.color = FromAppliedReverse;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void DoUpdate()
        {
            Image.color = Color.Lerp(FromAppliedReverse, ToAppliedReverse, Progress);
        }

        /// <summary>
        /// 完了時処理
        /// </summary>
        protected override void DoComplete()
        {
            Image.color = ToAppliedReverse;
        }
    }
}
