using System;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// シーンごとのサウンドデータ
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SceneSoundData), menuName = "ScriptableObjects/System/Sound/" + nameof(SceneSoundData))]
    public sealed class SceneSoundData : ScriptableObject
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// AudioClip 単体データ
        /// </summary>
        [Serializable]
        public class AudioClipData
        {
            public string       Name;
            public AudioClip    AudioClip;
        }


        //====================================
        //! 変数（public）
        //====================================

        /// <summary>
        /// SE の AudioClip リスト
        /// </summary>
        public AudioClipData[] SeAudioClipDataList;

        /// <summary>
        /// BGM の AudioClip リスト
        /// </summary>
        public AudioClipData[] BgmAudioClipDataList;
    }
}
