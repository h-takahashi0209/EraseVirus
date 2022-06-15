using System.Diagnostics;
using UnityEngine;


namespace TakahashiH
{
    public interface IInputUnLocker
    {
        void Unlock();
    }

    /// <summary>
    /// 入力制御コンポーネント
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InputManager : ExMonoBehaviour, IInputUnLocker
    {
        //====================================
        //! 変数（private static）
        //====================================

        /// <summary>
        /// インスタンス
        /// </summary>
        private static InputManager msInstance;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// ロック回数
        /// </summary>
        private int mLockCount;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (msInstance && msInstance != this)
            {
                Destroy(this);
            }

            msInstance = this;

            this.SetActive(false);
        }


        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 入力制限
        /// </summary>
        public static IInputUnLocker Lock()
        {
            if (!msInstance) {
                return null;
            }

            msInstance.SetActive(true);

            msInstance.mLockCount++;

            msInstance.UpdateLockCount();

            return msInstance;
        }

        /// <summary>
        /// キーが押下されたか
        /// </summary>
        /// <param name="keyCode"> キーコード </param>
        public static bool IsKeyDown(KeyCode keyCode)
        {
            if (!msInstance || msInstance.mLockCount > 0)
            {
                return false;
            }

            return Input.GetKeyDown(keyCode);
        }

        /// <summary>
        /// キーが離されたか
        /// </summary>
        /// <param name="keyCode"> キーコード </param>
        public static bool IsKeyUp(KeyCode keyCode)
        {
            if (!msInstance || msInstance.mLockCount > 0)
            {
                return false;
            }

            return Input.GetKeyUp(keyCode);
        }

        /// <summary>
        /// キー押下中か
        /// </summary>
        /// <param name="keyCode"> キーコード </param>
        public static bool IsKeyPress(KeyCode keyCode)
        {
            if (!msInstance || msInstance.mLockCount > 0)
            {
                return false;
            }

            return Input.GetKey(keyCode);
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 入力制限解除
        /// </summary>
        public void Unlock()
        {
            mLockCount--;

            UpdateLockCount();

            if (mLockCount > 0) {
                return;
            }

            this.SetActive(false);
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// ロック数表示更新
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        private void UpdateLockCount()
        {
            if (mLockCount <= 0)
            {
                msInstance.name = "InputManager(UnLock)";
            }
            else
            {
                msInstance.name = $"InputManager(Lock({msInstance.mLockCount}))";
            }
        }
    }
}
