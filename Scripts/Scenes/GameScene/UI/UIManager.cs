using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// UI 管理
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UIManager : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 次に出現するカプセル UI
        /// </summary>
        [SerializeField] private UINextCapsule UINextCapsule;

        /// <summary>
        /// ステータス UI
        /// </summary>
        [SerializeField] private UIStatus UIStatus;

        /// <summary>
        /// テロップ UI
        /// </summary>
        [SerializeField] private UITelop UITelop;

        /// <summary>
        /// コマンド UI
        /// </summary>
        [SerializeField] private UICommand UICommand;

        /// <summary>
        /// 次へ進むボタン
        /// </summary>
        [SerializeField] private UIButton UINextButton;

        /// <summary>
        /// 一時停止ボタン
        /// </summary>
        [SerializeField] private UIButton UIPauseButton;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 次に出現するカプセル UI
        /// </summary>
        public UINextCapsule NextCapsule => UINextCapsule;

        /// <summary>
        /// ステータス UI
        /// </summary>
        public UIStatus Status => UIStatus;

        /// <summary>
        /// テロップ UI
        /// </summary>
        public UITelop Telop => UITelop;

        /// <summary>
        /// コマンド UI
        /// </summary>
        public UICommand Command => UICommand;

        /// <summary>
        /// 次へ進むボタン押下時コールバック
        /// </summary>
        public Action OnClickNextButton { set { UINextButton.OnClick = value; } }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UINextCapsule   = transform.root.GetComponentInChildren<UINextCapsule>(true);
            UIStatus        = transform.root.GetComponentInChildren<UIStatus>(true);
            UITelop         = transform.root.GetComponentInChildren<UITelop>(true);
            UICommand       = transform.root.GetComponentInChildren<UICommand>(true);
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="onReqPause">     一時停止リクエスト    </param>
        /// <param name="onReqResume">    再開リクエスト        </param>
        /// <param name="onReqExit">      終了リクエスト        </param>
        public void Setup(Action onReqPause, Action onReqResume, Action onReqExit)
        {
            UIPauseButton.OnClick = () =>
            {
                onReqPause();

                UICommand.SetupPause(onReqResume, onReqExit);
            };

            UICommand    .SetActive(false);
            UITelop      .SetActive(false);
            UINextButton .SetActive(false);
        }

        /// <summary>
        /// 次へボタン有効化
        /// </summary>
        /// <param name="onClick"> クリック時コールバック </param>
        public void EnableNextButton(Action onClick)
        {
            UINextButton.SetActive(true);

            UINextButton.OnClick = () =>
            {
                UINextButton.SetActive(false);

                onClick();
            };
        }

        /// <summary>
        /// 一時停止ボタンアクティブ制御
        /// </summary>
        /// <param name="isActive"> アクティブか </param>
        public void SetActivePauseButton(bool isActive)
        {
            UIPauseButton.SetActive(isActive);
        }
    }
}

