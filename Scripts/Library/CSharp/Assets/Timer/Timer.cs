using System;


namespace TakahashiH
{
    /// <summary>
    /// �^�C�}�[
    /// </summary>
    public sealed class Timer : IDisposable
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �c�莞�ԁi�b�j
        /// </summary>
        private float mRemainingTimeSec;

        /// <summary>
        /// �������R�[���o�b�N
        /// </summary>
        private Action mOnComplete;

        /// <summary>
        /// �ꎞ��~����
        /// </summary>
        private bool mIsPause;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �v������
        /// </summary>
        public bool IsActive { get; private set; }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �j��
        /// </summary>
        public void Dispose()
        {
            mOnComplete = null;
        }

        /// <summary>
        /// �J�n
        /// </summary>
        /// <param name="timeSec">       �ҋ@����              </param>
        /// <param name="onComplete">    �������R�[���o�b�N    </param>
        public void Begin(float timeSec, Action onComplete)
        {
            mRemainingTimeSec   = timeSec;
            IsActive            = true;
            mIsPause            = false;
            mOnComplete         = onComplete;
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            mIsPause = true;
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            mIsPause = false;
        }

        /// <summary>
        /// �X�V
        /// </summary>
        /// <param name="deltaTimeSec"> �f���^���ԁi�b�j </param>
        public void UpdateTimer(float deltaTimeSec)
        {
            if (!IsActive || mIsPause) {
                return;
            }

            mRemainingTimeSec -= deltaTimeSec;

            if (mRemainingTimeSec <= 0f)
            {
                IsActive = false;
                mOnComplete?.Invoke();
            }
        }
    }
}
