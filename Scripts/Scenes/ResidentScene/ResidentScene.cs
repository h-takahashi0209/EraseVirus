
namespace TakahashiH.Scenes.Resident
{
    /// <summary>
    /// 常駐シーン
    /// </summary>
    public sealed class ResidentScene : SceneBase
    {
        //====================================
        //! 関数（SceneBase）
        //====================================

        /// <summary>
        /// DoAwake
        /// </summary>
        protected override void DoAwake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
