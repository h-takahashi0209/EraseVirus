using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// サウンド管理
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SoundManager : ExMonoBehaviour
    {
        //====================================
        //! 変数（private static）
        //====================================
        private static SoundManager msInstance;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// SE の AudioSource リスト
        /// </summary>
        [SerializeField] private AudioSource[] SeAudioSourceList;

        /// <summary>
        /// BGM の AudioSource
        /// </summary>
        [SerializeField] private AudioSource[] BgmAudioSourceList;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 常駐サウンドデータ
        /// </summary>
        private SceneSoundData mResidentSoundData;

        /// <summary>
        /// シーンごとのサウンドデータ
        /// </summary>
        private SceneSoundData mSceneSoundData;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if(msInstance && msInstance != this)
            {
                Destroy(this);
            }

            msInstance = this;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// シーンごとのサウンドデータ読み込み
        /// </summary>
        /// <param name="sceneType"> シーン種別 </param>
        public static void LoadSceneSoundData(SceneType sceneType)
        {
            if (msInstance == null || msInstance.mSceneSoundData != null) {
                return;
            }

            string path = $"{Path.Common.SoundData}{sceneType}/{sceneType}SoundData";

            var sceneSoundData = Resources.Load<SceneSoundData>(path);
            if (sceneSoundData == null) {
                return;
            }

            msInstance.mSceneSoundData = sceneSoundData;
        }

        /// <summary>
        /// 常駐サウンドデータ読み込み
        /// </summary>
        public static void LoadResidentSoundData()
        {
            if (msInstance == null || msInstance.mResidentSoundData != null) {
                return;
            }

            string path = $"{Path.Common.SoundData}{SceneType.ResidentScene}/{SceneType.ResidentScene}SoundData";

            var sceneSoundData = Resources.Load<SceneSoundData>(path);
            if (sceneSoundData == null) {
                return;
            }

            msInstance.mResidentSoundData = sceneSoundData;
        }

        /// <summary>
        /// シーンごとのサウンドデータ破棄
        /// </summary>
        public static void DisposeSceneSoundData()
        {
            if (msInstance == null) {
                return;
            }

            msInstance.mSceneSoundData = null;
        }

        /// <summary>
        /// SE 再生
        /// </summary>
        /// <param name="soundName">    サウンド名          </param>
        /// <param name="isLoop">       ループさせるか      </param>
        /// <returns>                   サウンドハンドル    </returns>
        public static ISoundHandle PlaySe(string soundName, bool isLoop = false)
        {
            return Play(SoundType.Se, soundName, isLoop);
        }

        /// <summary>
        /// BGM 再生
        /// </summary>
        /// <param name="soundName">    サウンド名          </param>
        /// <param name="isLoop">       ループさせるか      </param>
        /// <returns>                   サウンドハンドル    </returns>
        public static ISoundHandle PlayBgm(string soundName, bool isLoop = false)
        {
            return Play(SoundType.Bgm, soundName, isLoop);
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="soundHandle"> サウンドハンドル </param>
        public static void Stop(ISoundHandle soundHandle)
        {
            if (msInstance == null) {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null) {
                return;
            }

            audioSource.Stop();
        }

        /// <summary>
        /// 再開
        /// </summary>
        /// <param name="soundHandle"> サウンドハンドル </param>
        public static void Replay(ISoundHandle soundHandle)
        {
            if (msInstance == null)
            {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null)
            {
                return;
            }

            audioSource.Play();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        /// <param name="soundHandle"> サウンドハンドル </param>
        public static void Pause(ISoundHandle soundHandle)
        {
            if (msInstance == null) {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null) {
                return;
            }

            audioSource.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        /// <param name="soundHandle"> サウンドハンドル </param>
        public static void Resume(ISoundHandle soundHandle)
        {
            if (msInstance == null) {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null) {
                return;
            }

            audioSource.UnPause();
        }


        //====================================
        //! 関数（private static）
        //====================================

        /// <summary>
        /// 再生
        /// </summary>
        /// <param name="soundType">    サウンド種別        </param>
        /// <param name="soundName">    サウンド名          </param>
        /// <param name="isLoop">       ループさせるか      </param>
        /// <returns>                   サウンドハンドル    </returns>
        private static ISoundHandle Play(SoundType soundType, string soundName, bool isLoop = false)
        {
            var audioClip = GetAudioClip(soundType, soundName);
            if (audioClip == null)
            {
                return SoundHandle.Empty;
            }

            int audioSourceIdx = soundType switch
            {
                SoundType.Se    => msInstance.SeAudioSourceList  .FindIndex(a => !a.isPlaying),
                SoundType.Bgm   => msInstance.BgmAudioSourceList .FindIndex(a => !a.isPlaying),
                _               => -1,
            };

            if (audioSourceIdx < 0)
            {
                return SoundHandle.Empty;
            }

            var audioSource = soundType switch
            {
                SoundType.Se    => msInstance.SeAudioSourceList[audioSourceIdx],
                SoundType.Bgm   => msInstance.BgmAudioSourceList[audioSourceIdx],
                _               => null,
            };

            audioSource.loop = isLoop || soundType == SoundType.Bgm;
            audioSource.clip = audioClip;

            audioSource.Play();

            return new SoundHandle(soundType, audioSourceIdx);
        }

        /// <summary>
        /// オーディオクリップ取得
        /// </summary>
        /// <param name="soundType">    サウンド種別    </param>
        /// <param name="soundName">    サウンド名      </param>
        private static AudioClip GetAudioClip(SoundType soundType, string soundName)
        {
            AudioClip audioClip = null;

            if (msInstance.mSceneSoundData != null)
            {
                audioClip = soundType switch
                {
                    SoundType.Se    => msInstance.mSceneSoundData.SeAudioClipDataList  .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    SoundType.Bgm   => msInstance.mSceneSoundData.BgmAudioClipDataList .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    _               => null,
                };
            }

            if (audioClip == null && msInstance.mResidentSoundData != null)
            {
                audioClip = soundType switch
                {
                    SoundType.Se    => msInstance.mResidentSoundData.SeAudioClipDataList  .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    SoundType.Bgm   => msInstance.mResidentSoundData.BgmAudioClipDataList .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    _               => null,
                };
            }

            return audioClip;
        }

        /// <summary>
        /// オーディオソース取得
        /// </summary>
        /// <param name="soundHandle"> サウンドハンドル </param>
        private static AudioSource GetAudioSource(ISoundHandle soundHandle)
        {
            if (soundHandle == null)
            {
                return null;
            }

            return soundHandle.SoundType switch
            {
                SoundType.Se  => msInstance.SeAudioSourceList  .ElementAtOrDefault(soundHandle.AudioSourceIndex),
                SoundType.Bgm => msInstance.BgmAudioSourceList .ElementAtOrDefault(soundHandle.AudioSourceIndex),
                _             => null,
            };
        }
    }
}
