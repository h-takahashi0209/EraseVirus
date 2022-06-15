using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// シーン基底
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class SceneBase : ExMonoBehaviour
    {
        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            // 常駐シーンを一度だけ読み込み
            if (!SceneManager.IsLoadedResidentUIScene)
            {
                SceneManager.LoadAdditive(SceneType.ResidentScene);
            }

            SceneManager.SetLoadedResidentUIScene();

            // 常駐サウンドを読み込み
            SoundManager.LoadResidentSoundData();

            DoAwake();
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            DoStart();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            DoUpdate();
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            DoOnDestroy();
        }


        //====================================
        //! 関数（protected virtual）
        //====================================

        /// <summary>
        /// DoAwake
        /// </summary>
        protected virtual void DoAwake() {}

        /// <summary>
        /// DoStart
        /// </summary>
        protected virtual void DoStart() {}

        /// <summary>
        /// DoUpdate
        /// </summary>
        protected virtual void DoUpdate() {}

        /// <summary>
        /// DoOnDestroy
        /// </summary>
        protected virtual void DoOnDestroy() {}
    }
}
