using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;


namespace TakahashiH.Build
{
    /// <summary>
    /// アプリケーションのビルドを行う
    /// </summary>
    public sealed class ApplicationBuild
    {
        //====================================
        //! 関数（public)
        //====================================

        /// <summary>
        /// Windows 向けビルド
        /// </summary>
        [MenuItem("Tools/Build/Application/Windows")]
        public static void BuildWindows()
        {
            BuildImpl(RuntimePlatform.WindowsPlayer);
        }

        /// <summary>
        /// Android 向けビルド
        /// </summary>
        [MenuItem("Tools/Build/Application/Android")]
        public static void BuildAndroid()
        {
            BuildImpl(RuntimePlatform.Android);
        }

        /// <summary>
        /// iOS 向けビルド
        /// </summary>
        [MenuItem("Tools/Build/Application/iOS")]
        public static void BuildIOS()
        {
            BuildImpl(RuntimePlatform.IPhonePlayer);
        }


        //====================================
        //! 関数（public)
        //====================================

        /// <summary>
        /// ビルド内部実装
        /// </summary>
        /// <param name="platform"> プラットフォーム </param>
        private static void BuildImpl(RuntimePlatform platform)
        {
            Debug.Log($"Begin {platform} Application Build.");

            var scenePathList   = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
            var outputPath      = GetOutputPath(platform);
            var buildTarget     = GetBuildTarget(platform);
            var buildOptions    = GetBuildOptions(platform);

            for (int i = 0; i < scenePathList.Length; i++)
            {
                Debug.Log($"scenePathList({i}) : {scenePathList[i]}");
            }

            Debug.Log($"outputPath : {outputPath}");
            Debug.Log($"buildTarget : {buildTarget}");
            Debug.Log($"buildOptions : {buildOptions}");

            var buildReport = BuildPipeline.BuildPlayer
            (
                levels              : scenePathList     ,
                locationPathName    : outputPath        ,
                target              : buildTarget       ,
                options             : buildOptions
            );

            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Success ApplicationBuild!");
            }
            else
            {
                Debug.Log("Failed ApplicationBuild...");
                Debug.Log($"buildReport.summary.result : {buildReport.summary.result}");
            }
        }

        /// <summary>
        /// 出力先のパス取得
        /// </summary>
        /// <param name="platform"> プラットフォーム </param>
        private static string GetOutputPath(RuntimePlatform platform)
        {
            var basePath        = $"{Application.dataPath}/../Output/";
            var assetFolderDI   = new System.IO.DirectoryInfo(System.IO.Path.GetFullPath(Application.dataPath));
            var projectName     = assetFolderDI.Parent.Name;

            return platform switch
            {
                RuntimePlatform.WindowsPlayer   => $"{basePath}/Windows/{projectName}.exe"  ,
                RuntimePlatform.Android         => $"{basePath}/Android/{projectName}.apk"  ,
                RuntimePlatform.IPhonePlayer    => $"{basePath}/iOS/{projectName}.ipa"      ,
                _                               => string.Empty                             ,
            };
        }

        /// <summary>
        /// ビルドターゲット取得
        /// </summary>
        /// <param name="platform"> プラットフォーム </param>
        private static BuildTarget GetBuildTarget(RuntimePlatform platform)
        {
            return platform switch
            {
                RuntimePlatform.WindowsPlayer   => BuildTarget.StandaloneWindows    ,
                RuntimePlatform.Android         => BuildTarget.Android              ,
                RuntimePlatform.IPhonePlayer    => BuildTarget.iOS                  ,
                _                               => BuildTarget.NoTarget             ,
            };
        }

        /// <summary>
        /// ビルドオプション取得
        /// </summary>
        /// <param name="platform"> プラットフォーム </param>
        private static BuildOptions GetBuildOptions(RuntimePlatform platform)
        {
            return BuildOptions.None;
        }
    }
}
