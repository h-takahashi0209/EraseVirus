using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Unity �� MonoBehaviour ���g����������
    /// ��{�I�ɂ�������p��������
    /// </summary>
    public abstract class ExMonoBehaviour : MonoBehaviour
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// Transform �̃L���b�V��
        /// </summary>
        private Transform mTransformCache;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// Transform
        /// </summary>
        public new Transform transform
        {
            get
            {
                if (!mTransformCache)
                {
                    mTransformCache = GetComponent<Transform>();
                }

                return mTransformCache;
            }
        }
    }
}
