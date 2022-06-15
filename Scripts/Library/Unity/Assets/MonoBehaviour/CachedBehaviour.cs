using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Unity の MonoBehaviour を拡張したもの
    /// 基本的にこちらを継承させる
    /// </summary>
    public abstract class ExMonoBehaviour : MonoBehaviour
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// Transform のキャッシュ
        /// </summary>
        private Transform mTransformCache;


        //====================================
        //! プロパティ
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
