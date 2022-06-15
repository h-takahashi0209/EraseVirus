using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace TakahashiH
{
    /// <summary>
    /// �{�^�� UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButton : ExMonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�j���[�V�������
        /// </summary>
        private enum AnimationType
        {
            Press   ,
            Disable ,
        }


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// uGUI �{�^��
        /// </summary>
        [SerializeField] private Button Button;

        /// <summary>
        /// �A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �A�j���[�V�����������R�[���o�b�N
        /// </summary>
        private Action mOnCompAnimation = null;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �������R�[���o�b�N
        /// </summary>
        public Action OnClick { private get; set; }

        /// <summary>
        /// �A�j���[�V�����������R�[���o�b�N
        /// </summary>
        public Action OnCompAnimation { set { mOnCompAnimation = value; } }


        //====================================
        //! �֐��iMonoBehaviour�j
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
        //! �֐��iinterface�j
        //====================================

        /// <summary>
        /// �{�^������������
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Animator)
            {
                Animator.Play(AnimationType.Press.ToString());
            }
        }

        /// <summary>
        /// �{�^���𗣂����Ƃ�
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            if (Animator)
            {
                Animator.Play(AnimationType.Disable.ToString());
            }
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �A�j���[�V�����C�x���g - �A�j���[�V��������
        /// </summary>
        private void AnimationEvent_OnCompAnimation()
        {
            ActionUtils.CallOnce(ref mOnCompAnimation);
        }
    }
}
