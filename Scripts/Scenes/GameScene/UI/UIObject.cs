using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// �I�u�W�F�N�g UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    public sealed class UIObject : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�j���[�V�������
        /// </summary>
        public enum AnimationType
        {
            Wait        ,   // �ҋ@
            Disappear   ,   // ����
        }


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �C���[�W
        /// </summary>
        [SerializeField] private Image UIImage;

        /// <summary>
        /// �A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ���ŃA�j���[�V�����������R�[���o�b�N
        /// </summary>
        public Action mOnCompDisappearAnim;


        //====================================
        //! �֐��iMonoBehaviour�j
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
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="sprite"> �X�v���C�g </param>
        public void Setup(Sprite sprite)
        {
            UIImage.sprite = sprite;
        }

        /// <summary>
        /// �ҋ@�A�j���[�V�����Đ�
        /// </summary>
        public void PlayWaitAnimation()
        {
            Animator.Play(AnimationType.Wait.ToString());
        }

        /// <summary>
        /// ���ŃA�j���[�V�����Đ�
        /// </summary>
        /// <param name="onComplete"> �������R�[���o�b�N </param>
        public void PlayDisappearAnimation(Action onComplete)
        {
            mOnCompDisappearAnim = onComplete;

            Animator.Play(AnimationType.Disappear.ToString());
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            Animator.speed = 0f;
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            Animator.speed = 1f;
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �A�j���[�V�����C�x���g - ���ŃA�j���[�V��������
        /// </summary>
        private void AnimationEvent_CompDisappearAnim()
        {
            ActionUtils.CallOnce(ref mOnCompDisappearAnim);
        }
    }
}
