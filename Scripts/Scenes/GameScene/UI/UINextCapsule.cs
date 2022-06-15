using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// 次に出現するカプセル UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UINextCapsule : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アニメーション種別
        /// </summary>
        public enum AnimationType
        {
            Wait    ,   // 待機
            Appear  ,   // 出現
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 1つ目の半カプセル UI
        /// </summary>
        [SerializeField] private UIObject UIObject1;

        /// <summary>
        /// 2つ目の半カプセル UI
        /// </summary>
        [SerializeField] private UIObject UIObject2;

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 出現アニメーション完了時コールバック
        /// </summary>
        private Action mOnCompAppearAnim;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            mOnCompAppearAnim = null;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="sprite1">    1つ目の半カプセルのスプライト    </param>
        /// <param name="sprite2">    2つ目の半カプセルのスプライト    </param>
        public void Setup(Sprite sprite1, Sprite sprite2)
        {
            UIObject1.Setup(sprite1);
            UIObject2.Setup(sprite2);

            Animator.Play(AnimationType.Wait.ToString());
        }

        /// <summary>
        /// 出現アニメーション再生
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        public void PlayAppearAnimation(Action onComplete)
        {
            mOnCompAppearAnim = onComplete;

            Animator.Play(AnimationType.Appear.ToString());
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            Animator.speed = 0f;
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            Animator.speed = 1f;
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// アニメーションイベント - 出現アニメーション完了
        /// </summary>
        private void AnimationEvent_OnCompAppearAnim()
        {
            ActionUtils.CallOnce(ref mOnCompAppearAnim);
        }
    }
}

