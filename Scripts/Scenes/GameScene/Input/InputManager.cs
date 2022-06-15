using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// 入力管理
    /// </summary>
    public sealed class InputManager : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 入力ハンドラ
        /// </summary>
        [SerializeField] private InputHandler InputHandler;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 入力が有効か
        /// </summary>
        private bool mEnableInput;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// カプセル移動リクエスト
        /// </summary>
        public Action<MoveDirection> OnReqMove { private get; set; }

        /// <summary>
        /// カプセル落下リクエスト
        /// </summary>
        public Action OnReqFall { private get; set; }

        /// <summary>
        /// カプセル回転リクエスト
        /// </summary>
        public Action<RotateDirection> OnReqRotate { private get; set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            InputHandler.OnReqMove = moveDirection =>
            {
                switch (moveDirection)
                {
                    case MoveDirection.Left  : OnReqMove(moveDirection);    break;
                    case MoveDirection.Right : OnReqMove(moveDirection);    break;
                    case MoveDirection.Down  : OnReqFall();                 break;
                }
            };

            InputHandler.OnReqRotate = rotateDirection => OnReqRotate(rotateDirection);
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
#if UNITY_EDITOR
            if (!mEnableInput) {
                return;
            }

            if (TakahashiH.InputManager.IsKeyDown(KeyCode.LeftArrow))
            {
                OnReqMove?.Invoke(MoveDirection.Left);
            }

            if (TakahashiH.InputManager.IsKeyDown(KeyCode.RightArrow))
            {
                OnReqMove?.Invoke(MoveDirection.Right);
            }

            if (TakahashiH.InputManager.IsKeyPress(KeyCode.DownArrow))
            {
                OnReqMove?.Invoke(MoveDirection.Down);
            }

            if (TakahashiH.InputManager.IsKeyDown(KeyCode.X) || TakahashiH.InputManager.IsKeyDown(KeyCode.Space))
            {
                OnReqRotate?.Invoke(RotateDirection.Right);
            }

            if (TakahashiH.InputManager.IsKeyDown(KeyCode.Z))
            {
                OnReqRotate?.Invoke(RotateDirection.Left);
            }
#endif
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnReqMove   = null;
            OnReqFall   = null;
            OnReqRotate = null;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 入力制御
        /// </summary>
        /// <param name="enable"> 入力を有効にするか </param>
        public void SetEnableInput(bool enable)
        {
            mEnableInput = enable;

            InputHandler.SetEnableInput(enable);
        }
    }
}

