using System;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// フェード UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TweenImageColor))]
    public sealed class UIFade : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// デフォルトのフェード時間
        /// </summary>
        private readonly float DefDurationTimeSec = 0.5f;

        /// <summary>
        /// デフォルトのフェード色
        /// </summary>
        private readonly Color DefColor = Color.black;


        //====================================
        //! 変数（private static）
        //====================================

        /// <summary>
        /// インスタンス
        /// </summary>
        private static UIFade msInstance;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 開始色
        /// </summary>
        private Color mFromColor;

        /// <summary>
        /// 目標色
        /// </summary>
        private Color mToColor;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 色変更 Tween
        /// </summary>
        [SerializeField] private TweenImageColor TweenImageColor;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            TweenImageColor = GetComponent<TweenImageColor>();
        }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (msInstance && msInstance != this)
            {
                Destroy(this);
            }

            msInstance = this;
        }


        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="color">            フェード色                   </param>
        /// <param name="durationTimeSec">  フェードにかける時間（秒）   </param>
        /// <param name="onComplete">       完了時コールバック           </param>
        public static void FadeIn(Color color, float durationTimeSec, Action onComplete = null)
        {
            Fade(true, color, durationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="color">        フェード色           </param>
        /// <param name="onComplete">   完了時コールバック   </param>
        public static void FadeIn(Color color, Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeIn(color, msInstance.DefDurationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="durationTimeSec">  フェードにかける時間（秒）   </param>
        /// <param name="onComplete">       完了時コールバック           </param>
        public static void FadeIn(float durationTimeSec, Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeIn(msInstance.DefColor, durationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        public static void FadeIn(Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeIn(msInstance.DefColor, msInstance.DefDurationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="color">            フェード色                   </param>
        /// <param name="durationTimeSec">  フェードにかける時間（秒）   </param>
        /// <param name="onComplete">       完了時コールバック           </param>
        public static void FadeOut(Color color, float durationTimeSec, Action onComplete = null)
        {
            Fade(false, color, durationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="color">        フェード色           </param>
        /// <param name="onComplete">   完了時コールバック   </param>
        public static void FadeOut(Color color, Action onComplete = null)
        {
            FadeOut(color, msInstance.DefDurationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="durationTimeSec">  フェードにかける時間（秒）   </param>
        /// <param name="onComplete">       完了時コールバック           </param>
        public static void FadeOut(float durationTimeSec, Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeOut(msInstance.DefColor, durationTimeSec, onComplete);
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        public static void FadeOut(Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeOut(msInstance.DefColor, msInstance.DefDurationTimeSec, onComplete);
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// フェード
        /// </summary>
        /// <param name="isFadeIn">           フェードインさせるか          </param>
        /// <param name="color">              フェード色                    </param>
        /// <param name="durationTimeSec">    フェードにかける時間（秒）    </param>
        /// <param name="onComplete">         完了時コールバック            </param>
        private static void Fade(bool isFadeIn, Color color, float durationTimeSec, Action onComplete)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            var inputUnlocker = InputManager.Lock();

            msInstance.mFromColor = color;
            msInstance.mFromColor.a = 1f;

            msInstance.mToColor = color;
            msInstance.mToColor.a = 0f;

            msInstance.TweenImageColor.From             = msInstance.mFromColor;
            msInstance.TweenImageColor.To               = msInstance.mToColor;
            msInstance.TweenImageColor.DurationTimeSec  = durationTimeSec;

            msInstance.TweenImageColor.OnComplete = () =>
            {
                inputUnlocker.Unlock();
                onComplete?.Invoke();
            };

            if (isFadeIn)
            {
                msInstance.TweenImageColor.Begin();
            }
            else
            {
                msInstance.TweenImageColor.BeginReverse();
            }
        }
    }
}
