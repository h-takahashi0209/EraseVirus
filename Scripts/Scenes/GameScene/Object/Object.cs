using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// オブジェクト インターフェース
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// 色種別
        /// </summary>
        public ColorType ColorType { get; }

        /// <summary>
        /// オブジェクト種別
        /// </summary>
        public ObjectType ObjectType { get; }

        /// <summary>
        /// Xブロック座標
        /// </summary>
        public int BlockPosX { get; }

        /// <summary>
        /// Yブロック座標
        /// </summary>
        public int BlockPosY { get; }
    }

    /// <summary>
    /// オブジェクト基底
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIObject))]
    public abstract class Object : ExMonoBehaviour, IObject
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// オブジェクト UI
        /// </summary>
        [SerializeField] protected UIObject UIObject;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 色種別
        /// </summary>
        public ColorType ColorType { get; protected set; }

        /// <summary>
        /// オブジェクト種別
        /// </summary>
        public virtual ObjectType ObjectType => ObjectType.None;

        /// <summary>
        /// Xブロック座標
        /// </summary>
        public int BlockPosX { get; protected set; }

        /// <summary>
        /// Yブロック座標
        /// </summary>
        public int BlockPosY { get; protected set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UIObject = GetComponent<UIObject>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 待機アニメーション再生
        /// </summary>
        public void PlayWaitAnimation()
        {
            UIObject.PlayWaitAnimation();
        }

        /// <summary>
        /// 消滅アニメーション再生
        /// </summary>
        /// <param name="onComplete"> 完了時コールバック </param>
        public void PlayDisappearAnimation(Action onComplete)
        {
            UIObject.PlayDisappearAnimation(onComplete);
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            UIObject.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            UIObject.Resume();
        }
    }
}

