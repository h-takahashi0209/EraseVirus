using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// テロップ UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITelop : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// テロップ種別
        /// </summary>
        public enum TelopType
        {
            StageClear  ,
            GameOver    ,
            AllClear    ,
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// テロップアニメーション完了時コールバック
        /// </summary>
        private Action mOnCompTelopAnim;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            mOnCompTelopAnim = null;
        }

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Animator = GetComponent<Animator>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 再生
        /// </summary>
        /// <param name="telopType">     テロップ種別          </param>
        /// <param name="onComplete">    完了時コールバック    </param>
        public void Play(TelopType telopType, Action onComplete)
        {
            mOnCompTelopAnim = onComplete;

            gameObject.SetActive(true);

            Animator.Play(telopType.ToString());
        }

        /// <summary>
        /// 非表示にする
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
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
        /// アニメーションイベント - アニメーション再生完了
        /// </summary>
        /// <param name="telopType"> テロップ種別 </param>
        private void AnimationEvent_OnCompTelopAnim()
        {
            mOnCompTelopAnim();
        }
    }
}

