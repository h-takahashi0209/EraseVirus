using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステート - カプセル移動
    /// </summary>
    public sealed class StateMoveCapsule : StateBase
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 入力管理
        /// </summary>
        private InputManager mInputManager;

        /// <summary>
        /// カプセルが自動で1段階落下する時間
        /// </summary>
        private float mAutoFallTimeSec;

        /// <summary>
        /// カプセルの自動落下時間用タイマー
        /// </summary>
        private Timer mAutoFallTimer = new Timer();

        /// <summary>
        /// カプセルの入力による落下時間用タイマー
        /// </summary>
        private Timer mFallByInputTimer = new Timer();

        /// <summary>
        /// 入力によるカプセル落下中か
        /// </summary>
        private bool mIsFallByInput;

        /// <summary>
        /// 前フレームで入力によるカプセルの落下を行ったか
        /// </summary>
        private bool mIsFallByInputPrevFrame;

        /// <summary>
        /// 一時停止中か
        /// </summary>
        private bool mIsPause;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">         各ステートに渡す情報    </param>
        /// <param name="onComplete">      完了時コールバック      </param>
        /// <param name="inputManager">    入力制御                </param>
        public StateMoveCapsule(StateContext context, Action<State, IStateInputData> onComplete, InputManager inputManager) : base(context, onComplete)
        {
            mInputManager    = inputManager;
            mAutoFallTimeSec = Settings.GetAutoFallCapsuleTimeSec(mContext.FallSpeedType);

            mInputManager.OnReqMove   = moveDirection   => Move(moveDirection);
            mInputManager.OnReqFall   = ()              => FallCapsule();
            mInputManager.OnReqRotate = rotateDirection => Rotate(rotateDirection);
        }


        //====================================
        //! 関数（public override）
        //====================================

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="inputData"> 他のステートから渡されるデータ </param>
        public override void Process(IStateInputData inputData)
        {
            mInputManager.SetEnableInput(true);

            mAutoFallTimer.Begin(mAutoFallTimeSec, () => AutoFallCapsule());
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void DoUpdate()
        {
            if (mIsPause) {
                return;
            }

            mAutoFallTimer.UpdateTimer(TimeManager.DeltaTime);

            // 入力中は一定時間ごとにカプセルを落下させる
            if (mIsFallByInput)
            {
                if (!mIsFallByInputPrevFrame)
                {
                    mFallByInputTimer.Begin(Settings.CapsuleForceFallTimeSec, () => InputFallCapsule());
                }

                mFallByInputTimer.UpdateTimer(TimeManager.DeltaTime);
            }

            mIsFallByInputPrevFrame = mIsFallByInput;
            mIsFallByInput          = false;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public override void DoDispose()
        {
            mAutoFallTimer.Dispose();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Pause()
        {
            mIsPause = true;

            mAutoFallTimer    .Pause();
            mFallByInputTimer .Pause();

            mInputManager.SetEnableInput(false);
        }

        /// <summary>
        /// 再開
        /// </summary>
        public override void Resume()
        {
            mIsPause = false;

            mAutoFallTimer    .Resume();
            mFallByInputTimer .Resume();

            mInputManager.SetEnableInput(true);
        }

        /// <summary>
        /// ドラッグによりカプセルを落とす
        /// </summary>
        public void DragFallCapsule()
        {
            FallCapsule();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="moveDirection"> 移動方向 </param>
        private void Move(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                case MoveDirection.Right:
                    {
                        MoveLeftOrRight(moveDirection);
                    }
                    break;

                case MoveDirection.Down:
                    {
                        mIsFallByInput = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// 回転
        /// </summary>
        /// <param name="direction"> 回転方向 </param>
        private void Rotate(RotateDirection direction)
        {
            mContext.CurrentCapsule.Rotate(direction);
        }

        /// <summary>
        /// 左右移動
        /// </summary>
        /// <param name="moveDirection"> 移動方向 </param>
        private void MoveLeftOrRight(MoveDirection direction)
        {
            int blockPosX = mContext.CurrentCapsule.BlockPositionX;
            int blockPosY = mContext.CurrentCapsule.BlockPositionY;

            bool canMoveLeft  = (blockPosX > 0 && GetColorType(blockPosX - 1, blockPosY) == ColorType.None);
            bool canMoveRight = CanMoveRight(blockPosX, blockPosY);

            switch (direction)
            {
                case MoveDirection.Left  : blockPosX = canMoveLeft  ? Math.Max(blockPosX - 1, 0)                              : blockPosX;  break;
                case MoveDirection.Right : blockPosX = canMoveRight ? Math.Min(blockPosX + 1, GameSceneDef.BlockNumWidth - 1) : blockPosX;  break;
            }

            mContext.CurrentCapsule.UpdatePosition(blockPosX, mContext.CurrentCapsule.BlockPositionY);
        }

        /// <summary>
        /// 右移動可能か
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        private bool CanMoveRight(int blockPosX, int blockPosY)
        {
            // 縦向き
            if (mContext.CurrentCapsule.Direction == CapsuleDirection.Vertial)
            {
                // 右端は右移動不可
                if (blockPosX >= GameSceneDef.BlockNumWidth - 1)
                {
                    return false;
                }

                // 1つ右隣にカプセルかウイルスがいたら右移動不可
                if (GetColorType(blockPosX + 1, blockPosY) != ColorType.None)
                {
                    return false;
                }
            }

            // 横向き
            else if (mContext.CurrentCapsule.Direction == CapsuleDirection.Horizontal)
            {
                // 右端の1つ左が実質右端になるので右移動不可
                if (blockPosX >= GameSceneDef.BlockNumWidth - 2)
                {
                    return false;
                }

                // 2つ右隣にカプセルかウイルスがいたら右移動不可
                if (GetColorType(blockPosX + 2, blockPosY) != ColorType.None)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 時間経過によりカプセルを落とす
        /// </summary>
        private void AutoFallCapsule()
        {
            FallCapsule();

            mAutoFallTimer.Begin(mAutoFallTimeSec, () => AutoFallCapsule());
        }

        /// <summary>
        /// 下入力によりカプセルを落とす
        /// </summary>
        private void InputFallCapsule()
        {
            FallCapsule();

            mFallByInputTimer.Begin(Settings.CapsuleForceFallTimeSec, () => InputFallCapsule());
        }

        /// <summary>
        /// カプセルを落とす
        /// </summary>
        private void FallCapsule()
        {
            int currentBlockPosX = mContext.CurrentCapsule.BlockPositionX;
            int currentBlockPosY = Math.Max(mContext.CurrentCapsule.BlockPositionY - 1, 0);

            mContext.CurrentCapsule.UpdatePosition(currentBlockPosX, currentBlockPosY);

            bool isCompFall = (currentBlockPosY <= 0);
            isCompFall |= (currentBlockPosY > 0 && GetColorType(currentBlockPosX, currentBlockPosY - 1) != ColorType.None);
            isCompFall |= (currentBlockPosY > 0 && mContext.CurrentCapsule.Direction == CapsuleDirection.Horizontal && GetColorType(currentBlockPosX + 1, currentBlockPosY - 1) != ColorType.None);

            // カプセル着地
            if (isCompFall)
            {
                int pairedBlockPosX = currentBlockPosX + (mContext.CurrentCapsule.Direction == CapsuleDirection.Horizontal ? 1 : 0);
                int pairedBlockPosY = currentBlockPosY + (mContext.CurrentCapsule.Direction == CapsuleDirection.Vertial    ? 1 : 0);

                mContext.ObjectStack.PushHalfCapsule(currentBlockPosX, currentBlockPosY, mContext.CurrentCapsule.GetColorType(0), pairedBlockPosX , pairedBlockPosY);
                mContext.ObjectStack.PushHalfCapsule(pairedBlockPosX , pairedBlockPosY , mContext.CurrentCapsule.GetColorType(1), currentBlockPosX, currentBlockPosY);

                mContext.CurrentCapsule.SetActive(false);

                var inputData = new StateDisappearCapsule.InputData()
                {
                    HalfCapsuleList = mContext.CurrentCapsule.HalfCapsuleList
                };

                mInputManager.SetEnableInput(false);

                // カプセル消滅ステートへ遷移
                mOnComplete(State.DisappearCapsule, inputData);
            }
        }

        /// <summary>
        /// 指定された箇所にあるカプセルの色種別取得
        /// </summary>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        private ColorType GetColorType(int blockPosX, int blockPosY)
        {
            return mContext.ObjectStack.GetColorType(blockPosX, blockPosY);
        }
    }
}
