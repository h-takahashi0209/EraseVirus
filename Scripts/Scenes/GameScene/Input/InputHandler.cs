using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ゲームシーン - 入力ハンドラ
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InputHandler : ExMonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 移動リクエストを行うドラッグ距離
        /// </summary>
        private const float ReqMoveDragDistance = 100f;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 前回のドラッグ開始座標
        /// </summary>
        private Vector2 mBeginDragPos;

        /// <summary>
        /// 入力が有効か
        /// </summary>
        private bool mEnableInput;

        /// <summary>
        /// ドラッグ中か
        /// </summary>
        private bool mIsDragging;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 回転リクエスト
        /// </summary>
        public Action<RotateDirection> OnReqRotate { private get; set; }

        /// <summary>
        /// 移動リクエスト
        /// </summary>
        public Action<MoveDirection> OnReqMove { private get; set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnReqRotate = null;
            OnReqMove   = null;
        }


        //====================================
        //! 関数（interface）
        //====================================

        /// <summary>
        /// タップ時
        /// </summary>
        /// <param name="eventData"> 入力イベントデータ </param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!mEnableInput || mIsDragging) {
                return;
            }

            var rotateDirection = (eventData.position.x - GameSceneDef.FieldWidth / 2f) < 0f ? RotateDirection.Left : RotateDirection.Right;

            OnReqRotate(rotateDirection);
        }

        /// <summary>
        /// ドラッグ開始時
        /// </summary>
        /// <param name="eventData"> 入力イベントデータ </param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!mEnableInput) {
                return;
            }

            mBeginDragPos = eventData.position;
            mIsDragging   = true;
        }

        /// <summary>
        /// ドラッグ中
        /// </summary>
        /// <param name="eventData"> 入力イベントデータ </param>
        public void OnDrag(PointerEventData eventData)
        {
            if (!mEnableInput) {
                return;
            }

            var dragPos = eventData.position;

            // 右
            if ((dragPos.x - mBeginDragPos.x) >= ReqMoveDragDistance)
            {
                OnReqMove(MoveDirection.Right);

                mBeginDragPos = eventData.position;
            }

            // 左
            else if ((mBeginDragPos.x - dragPos.x) >= ReqMoveDragDistance)
            {
                OnReqMove(MoveDirection.Left);

                mBeginDragPos = eventData.position;
            }

            // 下
            else if((mBeginDragPos.y - dragPos.y) >= ReqMoveDragDistance)
            {
                OnReqMove(MoveDirection.Down);

                mBeginDragPos = eventData.position;
            }
        }

        /// <summary>
        /// ドラッグ終了時
        /// </summary>
        /// <param name="eventData"> 入力イベントデータ </param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!mEnableInput) {
                return;
            }

            mIsDragging = false;
        }

        /// <summary>
        /// 入力制御
        /// </summary>
        /// <param name="enable"> 入力を有効にするか </param>
        public void SetEnableInput(bool enable)
        {
            mEnableInput = enable;

            if (enable)
            {
                mIsDragging = false;
            }
        }
    }
}

