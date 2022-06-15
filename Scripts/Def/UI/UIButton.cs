using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace TakahashiH
{
    /// <summary>
    /// ボタン UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButton : ExMonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アニメーション種別
        /// </summary>
        private enum AnimationType
        {
            Press   ,
            Disable ,
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// uGUI ボタン
        /// </summary>
        [SerializeField] private Button Button;

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// アニメーション完了時コールバック
        /// </summary>
        private Action mOnCompAnimation = null;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 押下時コールバック
        /// </summary>
        public Action OnClick { private get; set; }

        /// <summary>
        /// アニメーション完了時コールバック
        /// </summary>
        public Action OnCompAnimation { set { mOnCompAnimation = value; } }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Button = GetComponent<Button>();
        }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            Button.onClick.AddListener(() => OnClick?.Invoke());
        }

        /// <summary>
        /// OnEnable
        /// </summary>
        private void OnEnable()
        {
            transform.ResetLocalScale();
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnClick          = null;
            mOnCompAnimation = null;
        }


        //====================================
        //! 関数（interface）
        //====================================

        /// <summary>
        /// ボタン押し下げ時
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Animator)
            {
                Animator.Play(AnimationType.Press.ToString());
            }
        }

        /// <summary>
        /// ボタンを離したとき
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            if (Animator)
            {
                Animator.Play(AnimationType.Disable.ToString());
            }
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// アニメーションイベント - アニメーション完了
        /// </summary>
        private void AnimationEvent_OnCompAnimation()
        {
            ActionUtils.CallOnce(ref mOnCompAnimation);
        }
    }
}
