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

        /// <summary>
        /// BGM ハンドル
        /// </summary>
        private ISoundHandle mBgmHandle;


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
                Debug.LogWarning("GameScene の入力データ受け取りに失敗しました。");

                sceneInputData = new InputData()
                {
                    Level = 0,
                    FallSpeedType = FallSpeedType.Low
                };
            }

            Load();
            Setup(sceneInputData);
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

            SoundManager.DisposeSceneSoundData();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 読み込み
        /// </summary>
        private void Load()
        {
            Settings.Load();

            SpriteLoader.CreateInstance();
            SpriteLoader.Load();

            SoundManager.LoadSceneSoundData(SceneType.GameScene);
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="inputData"> 他シーンから渡されるデータ </param>
        private void Setup(InputData inputData)
        {
            ObjectStack.Setup();

            UIManager.Setup(Pause, Resume, Exit);

            mContext = new StateContext(ObjectStack, CurrentCapsule, UIManager, inputData.Level, inputData.FallSpeedType);

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
        }

        /// <summary>
        /// 次のステート実行
        /// </summary>
        /// <param name="nextState">    実行するステート                  </param>
        /// <param name="inputData">    他のステートから渡されるデータ    </param>
        private void ProcessNextState(State nextState, IStateInputData inputData = null)
        {
            PlayAndStopBgm(nextState);

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

            SoundManager.Pause(mBgmHandle);
        }

        /// <summary>
        /// 再開
        /// </summary>
        private void Resume()
        {
            mCurrentState?.Resume();

            SoundManager.Resume(mBgmHandle);
        }

        /// <summary>
        /// ステートに合わせて BGM を再生 / 停止
        /// </summary>
        /// <param name="state"> ステート </param>
        private void PlayAndStopBgm(State state)
        {
            switch (state)
            {
                case State.GenerateStage:
                    {
                        if (mBgmHandle == null)
                        {
                            mBgmHandle = SoundManager.PlayBgm(SoundDef.GameScene.Bgm.GameScene.ToString(), true);
                        }
                        else
                        {
                            SoundManager.Replay(mBgmHandle);
                        }
                    }
                    break;

                case State.GameClear:
                case State.GameOver:
                case State.Finished:
                    {
                        SoundManager.Stop(mBgmHandle);
                    }
                    break;
            }
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

