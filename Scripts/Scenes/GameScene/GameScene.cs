using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ゲームシーン
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class GameScene : SceneBase
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 他シーンから渡されるデータ
        /// </summary>
        public class InputData
        {
            /// <summary>
            /// レベル
            /// </summary>
            public int Level;

            /// <summary>
            /// カプセル落下速度種別
            /// </summary>
            public FallSpeedType FallSpeedType;
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 入力管理
        /// </summary>
        [SerializeField] private InputManager InputManager;

        /// <summary>
        /// オブジェクトのスタック
        /// </summary>
        [SerializeField] private ObjectStack ObjectStack;

        /// <summary>
        /// 操作するカプセル
        /// </summary>
        [SerializeField] private Capsule CurrentCapsule;

        /// <summary>
        /// UI 管理
        /// </summary>
        [SerializeField] UIManager UIManager;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// ステージローダー
        /// </summary>
        private StageLoader mStageLoader = new StageLoader();

        /// <summary>
        /// 実行中のステート
        /// </summary>
        private IState mCurrentState;

        /// <summary>
        /// ステートリスト
        /// </summary>
        private IReadOnlyList<IState> mStateList;

        /// <summary>
        /// 各ステートに渡す情報
        /// </summary>
        private StateContext mContext;


        //====================================
        //! 関数（SceneBase）
        //====================================

        /// <summary>
        /// DoStart
        /// </summary>
        protected override void DoStart()
        {
            var sceneInputData = SceneManager.SceneInputData as InputData;
            if (sceneInputData == null)
            {
                Debug.LogWarning("ゲームシーンの入力データ受け取りに失敗しました。");

                sceneInputData = new InputData()
                {
                    FallSpeedType = FallSpeedType.Low,
                    Level         = 1
                };
            }

            SpriteLoader.CreateInstance();

            Settings     .Load();
            SpriteLoader .Load();

            ObjectStack.Setup();

            UIManager.Setup(Pause, Resume, Exit);

            mContext = new StateContext(ObjectStack, CurrentCapsule, UIManager, sceneInputData.Level, sceneInputData.FallSpeedType);

            mStateList = new StateBase[]
            {
                new StateGenerateStage      (mContext, ProcessNextState, mStageLoader),
                new StateAppearCapsule      (mContext, ProcessNextState, UIManager.NextCapsule),
                new StateMoveCapsule        (mContext, ProcessNextState, InputManager),
                new StateDisappearCapsule   (mContext, ProcessNextState),
                new StateFallCapsule        (mContext, ProcessNextState),
                new StateGameOver           (mContext, ProcessNextState),
                new StateGameClear          (mContext, ProcessNextState),
            };

            ProcessNextState(State.GenerateStage);

            UIFade.FadeIn(Color.black, CommonDef.FadeTimeSec);
        }

        /// <summary>
        /// DoUpdate
        /// </summary>
        protected override void DoUpdate()
        {
            mCurrentState?.DoUpdate();
        }

        /// <summary>
        /// DoOnDestroy
        /// </summary>
        protected override void DoOnDestroy()
        {
            for (int i = 0; i < mStateList.Count; i++)
            {
                mStateList[i].Dispose();
            }

            Settings     .Dispose();
            SpriteLoader .Dispose();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 次のステート実行
        /// </summary>
        /// <param name="nextState">    実行するステート                  </param>
        /// <param name="inputData">    他のステートから渡されるデータ    </param>
        private void ProcessNextState(State nextState, IStateInputData inputData = null)
        {
            if (nextState == State.Finished)
            {
                Exit();
                return;
            }

            mCurrentState = mStateList[(int)nextState];

            mCurrentState.Process(inputData);
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        private void Pause()
        {
            mCurrentState?.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        private void Resume()
        {
            mCurrentState?.Resume();
        }

        /// <summary>
        /// ゲーム終了
        /// </summary>
        private void Exit()
        {
            UIFade.FadeOut(Color.black, CommonDef.FadeTimeSec, () =>
            {
                SceneManager.Load(SceneType.TitleScene);
            });
        }
    }
}

