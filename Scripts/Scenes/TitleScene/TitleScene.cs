using UnityEngine;


namespace TakahashiH.Scenes.TitleScene
{
    /// <summary>
    /// タイトルシーン
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TitleScene : SceneBase
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 画面全体を覆うボタン
        /// </summary>
        [SerializeField] private UIButton ScreenButton;


        //====================================
        //! 関数（SceneBase）
        //====================================

        /// <summary>
        /// DoStart
        /// </summary>
        protected override void DoStart()
        {
            UIFade.FadeIn(Color.black, CommonDef.FadeTimeSec, () =>
            {
                ScreenButton.OnClick = () =>
                {
                    UIFade.FadeOut(Color.black, CommonDef.FadeTimeSec, () =>
                    {
                        SceneManager.Load(SceneType.SettingScene);
                    });
                };
            });
        }
    }
}

