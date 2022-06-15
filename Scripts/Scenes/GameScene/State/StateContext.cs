using System;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// 各ステートに渡す情報
    /// </summary>
    public sealed class StateContext
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// オブジェクトのスタック
        /// </summary>
        public ObjectStack ObjectStack { get; private set; }

        /// <summary>
        /// 操作するカプセル
        /// </summary>
        public Capsule CurrentCapsule { get; private set; }

        /// <summary>
        /// UI 管理
        /// </summary>
        public UIManager UIManager { get; private set; }

        /// <summary>
        /// レベル
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 落下速度種別
        /// </summary>
        public FallSpeedType FallSpeedType { get; private set; }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="objectStack">       オブジェクトのスタック     </param>
        /// <param name="currentCapsule">    操作するカプセル           </param>
        /// <param name="uiManager">         UI 管理                    </param>
        /// <param name="level">             レベル                     </param>
        /// <param name="fallSpeedType">     落下速度種別               </param>
        public StateContext(ObjectStack objectStack, Capsule currentCapsule, UIManager uiManager, int level, FallSpeedType fallSpeedType)
        {
            ObjectStack     = objectStack;
            CurrentCapsule  = currentCapsule;
            UIManager       = uiManager;
            Level           = level;
            FallSpeedType   = fallSpeedType;
        }

        /// <summary>
        /// レベル加算
        /// </summary>
        public void IncLevel()
        {
            Level = Math.Min(Level + 1, CommonDef.MaxLevel);
        }
    }
}

