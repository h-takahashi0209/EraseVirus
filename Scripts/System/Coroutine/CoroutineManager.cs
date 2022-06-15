using System;
using System.Collections;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// コルーチン管理
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CoroutineManager : ExMonoBehaviour
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// インスタンス
        /// </summary>
        public static CoroutineManager Instance { get; private set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(this);
            }

            Instance = this;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 指定時間待機
        /// </summary>
        /// <param name="timeSec">       待機時間（秒）        </param>
        /// <param name="onComplete">    完了時コールバック    </param>
        public IEnumerator CallWaitForSeconds(float timeSec, Action onComplete)
        {
            var enumerator = _CallWaitForSeconds(timeSec, onComplete);

            StartCoroutine(enumerator);

            return enumerator;
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        /// <param name="coroutine"> コルーチン </param>
        public void PauseCoroutie(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }

        /// <summary>
        /// 再開
        /// </summary>
        /// <param name="coroutine"> コルーチン </param>
        public void ResumeCoroutie(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 指定時間待機
        /// </summary>
        /// <param name="timeSec">       待機時間（秒）        </param>
        /// <param name="onComplete">    完了時コールバック    </param>
        public IEnumerator _CallWaitForSeconds(float timeSec, Action onComplete)
        {
            yield return new WaitForSeconds(timeSec);

            onComplete();
        }
    }
}
