using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// オブジェクト UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    public sealed class UIObject : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アニメーション種別
        /// </summary>
        public enum AnimationType
        {
            Wait        ,   // 待機
            Disappear   ,   // 消滅
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// イメージ
        /// </summary>
        [SerializeField] private Image UIImage;

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 消滅アニメーション完了時コールバック
        /// </summary>
        public Action mOnCompDisappearAnim;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            UIImage.sprite = null;

            mOnCompDisappearAnim = null;
        }

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UIImage  = GetComponent<Image>();
            Animator = GetComponent<Animator>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="sprite"> スプライト </param>
        public void Setup(Sprite sprite)
        {
            UIImage.sprite = sprite;
        }

        /// <summary>
        /// 待機アニメーション再生
        /// </summary>
        public void PlayWaitAnimation()
        {
            Animator.Play(AnimationType.Wait.ToString());
        }

        /// <summary>
        /// 消滅アニメーション再生
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        public void PlayDisappearAnimation(Action onComplete)
        {
            mOnCompDisappearAnim = onComplete;

            Animator.Play(AnimationType.Disappear.ToString());
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
        /// アニメーションイベント - 消滅アニメーション完了
        /// </summary>
        private void AnimationEvent_CompDisappearAnim()
        {
            ActionUtils.CallOnce(ref mOnCompDisappearAnim);
        }
    }
}
