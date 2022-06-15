using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// コマンド UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UICommand : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 上ボタン
        /// </summary>
        [SerializeField] private UIButton UITopButton;

        /// <summary>
        /// 下ボタン
        /// </summary>
        [SerializeField] private UIButton UIBottomButton;

        /// <summary>
        /// 上ボタン文言
        /// </summary>
        [SerializeField] private Text UITopButtonText;

        /// <summary>
        /// 下ボタン文言
        /// </summary>
        [SerializeField] private Text UIBottomButtonText;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 一時停止画面のセットアップ
        /// </summary>
        /// <param name="onClickResumeButton">    再開ボタン押下時コールバック    </param>
        /// <param name="onClickExitButton">      終了ボタン押下時コールバック    </param>
        public void SetupPause(Action onClickResumeButton, Action onClickExitButton)
        {
            Setup("Resume", "Exit", onClickResumeButton, onClickExitButton);
        }

        /// <summary>
        /// ゲームオーバー画面のセットアップ
        /// </summary>
        /// <param name="onClickRetryButton">    リトライボタン押下時コールバック    </param>
        /// <param name="onClickExitButton">     終了ボタン押下時コールバック        </param>
        public void SetupGameOver(Action onClickRetryButton, Action onClickExitButton)
        {
            Setup("Retry", "Exit", onClickRetryButton, onClickExitButton);
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="topButtonText">          上ボタンテキスト              </param>
        /// <param name="bottomButtonText">       下ボタンテキスト              </param>
        /// <param name="onClickTopButton">       上ボタン押下時コールバック    </param>
        /// <param name="onClickBottomButton">    上ボタン押下時コールバック    </param>
        private void Setup(string topButtonText, string bottomButtonText, Action onClickTopButton, Action onClickBottomButton)
        {
            gameObject.SetActive(true);

            UITopButtonText    .text = topButtonText;
            UIBottomButtonText .text = bottomButtonText;

            UITopButton.OnClick = () =>
            {
                UITopButton.OnCompAnimation = () =>
                {
                    gameObject.SetActive(false);
                    onClickTopButton();
                };
            };

            UIBottomButton.OnClick = () =>
            {
                UIBottomButton.OnCompAnimation = () =>
                {
                    gameObject.SetActive(false);
                    onClickBottomButton();
                };
            };
        }
    }
}
