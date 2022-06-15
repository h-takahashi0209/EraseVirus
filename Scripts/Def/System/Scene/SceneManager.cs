using UnityEngine.SceneManagement;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;


namespace TakahashiH
{
    /// <summary>
    /// シーン管理
    /// </summary>
    public static class SceneManager
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 常駐 UI シーンを読み込み済みか
        /// </summary>
        public static bool IsLoadedResidentUIScene { get; private set; }

        /// <summary>
        /// シーン間で受け渡しするデータ
        /// </summary>
        public static object SceneInputData { get; private set; }


        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="sceneType">         シーン種別                      </param>
        /// <param name="sceneInputData">    シーン間で受け渡しするデータ    </param>
        public static void Load(SceneType sceneType, object sceneInputData = null)
        {
            SceneInputData = sceneInputData;

            UnitySceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Single);
        }

        /// <summary>
        /// 加算読み込み
        /// </summary>
        /// <param name="sceneType">         シーン種別                      </param>
        /// <param name="sceneInputData">    シーン間で受け渡しするデータ    </param>
        public static void LoadAdditive(SceneType sceneType, object sceneInputData = null)
        {
            if (Exists(sceneType)) {
                return;
            }

            SceneInputData = sceneInputData;

            UnitySceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);
        }

        /// <summary>
        /// 破棄
        /// </summary>
        /// <param name="sceneType"> シーン種別 </param>
        public static void Unload(SceneType sceneType)
        {
            if (!Exists(sceneType)) {
                return;
            }

            UnitySceneManager.UnloadSceneAsync(sceneType.ToString());
        }

        /// <summary>
        /// 常駐 UI シーンを読み込み済みとする
        /// </summary>
        public static void SetLoadedResidentUIScene()
        {
            IsLoadedResidentUIScene = true;
        }


        //====================================
        //! 関数（private static）
        //====================================

        /// <summary>
        /// 読み込み済みのシーンか
        /// </summary>
        /// <param name="sceneType"> シーン種別 </param>
        private static bool Exists(SceneType sceneType)
        {
            for (int i = 0; i < UnitySceneManager.sceneCount; i++)
            {
                var scene = UnitySceneManager.GetSceneAt(i);

                if(scene.name == sceneType.ToString())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
