using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.SettingScene
{
    /// <summary>
    /// 設定シーン
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SettingScene : SceneBase
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// レベルのカーソルの間隔
        /// </summary>
        private const float LevelCursorInterval = 30f;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// レベルのカーソル
        /// </summary>
        [SerializeField] private GameObject UILevelCursorObj;

        /// <summary>
        /// レベルテキスト
        /// </summary>
        [SerializeField] private Text UILevelText;

        /// <summary>
        /// レベル加算ボタン
        /// </summary>
        [SerializeField] private UIButton UIIncLevelButton;

        /// <summary>
        /// レベル減算ボタン
        /// </summary>
        [SerializeField] private UIButton UIDecLevelButton;

        /// <summary>
        /// 落下速度ボタンリスト
        /// </summary>
        [SerializeField] private UIButton[] UIFallSpeedButtonList;

        /// <summary>
        /// 落下速度ボタンカーソル
        /// </summary>
        [SerializeField] private GameObject UIFallSpeedButtonCursorObj;

        /// <summary>
        /// 開始ボタン
        /// </summary>
        [SerializeField] private UIButton UIStartButton;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// レベル
        /// </summary>
        private int mLevel;

        /// <summary>
        /// 落下速度種別
        /// </summary>
        private FallSpeedType mFallSpeedType;


        //====================================
        //! 関数（SceneBase）
        //====================================

        /// <summary>
        /// DoStart
        /// </summary>
        protected override void DoStart()
        {
            ChangeLevel(0, false);
            ChangeFallSpeed(FallSpeedType.Low, false);

            UIFade.FadeIn(Color.black, CommonDef.FadeTimeSec, () =>
            {
                UIIncLevelButton.OnClick = () => ChangeLevel( 1, true);
                UIDecLevelButton.OnClick = () => ChangeLevel(-1, true);

                for (int i = 0; i < UIFallSpeedButtonList.Length; i++)
                {
                    var fallSpeedType = (FallSpeedType)i;

                    UIFallSpeedButtonList[i].OnClick = () => ChangeFallSpeed(fallSpeedType, true);
                }

                UIStartButton.OnClick = () => StartGame();
            });
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// レベル変更
        /// </summary>
        /// <param name="addedLevel">    加算するレベル     </param>
        /// <param name="isPlaySe">      SE を再生するか    </param>
        private void ChangeLevel(int addedLevel, bool isPlaySe)
        {
            mLevel = Mathf.Clamp(mLevel + addedLevel, 0, CommonDef.MaxLevel);

            UILevelText.text = $"Level:{mLevel}";

            float cursorPosX = mLevel * LevelCursorInterval;

            UILevelCursorObj.transform.SetLocalPositionX(cursorPosX);

            UIIncLevelButton.SetActive(mLevel < CommonDef.MaxLevel);
            UIDecLevelButton.SetActive(mLevel > 0);

            if (isPlaySe)
            {
                SoundManager.PlaySe(SoundDef.ResidentScene.Se.Select.ToString());
            }
        }

        /// <summary>
        /// 落下速度種別変更
        /// </summary>
        /// <param name="fallSpeedType">    落下速度種別       </param>
        /// <param name="isPlaySe">         SE を再生するか    </param>
        private void ChangeFallSpeed(FallSpeedType fallSpeedType, bool isPlaySe)
        {
            mFallSpeedType = fallSpeedType;

            float cursorPosX = UIFallSpeedButtonList[(int)fallSpeedType].GetLocalPositionX();

            UIFallSpeedButtonCursorObj.transform.SetLocalPositionX(cursorPosX);

            if (isPlaySe)
            {
                SoundManager.PlaySe(SoundDef.ResidentScene.Se.Select.ToString());
            }
        }

        /// <summary>
        /// ゲーム開始
        /// </summary>
        private void StartGame()
        {
            UIStartButton.enabled = false;

            var inputData = new GameScene.GameScene.InputData()
            {
                Level         = mLevel,
                FallSpeedType = mFallSpeedType
            };

            UIFade.FadeOut(Color.black, CommonDef.FadeTimeSec, () =>
            {
                SceneManager.Load(SceneType.GameScene, inputData);
            });

            SoundManager.PlaySe(SoundDef.ResidentScene.Se.Decide.ToString());
        }
    }
}

