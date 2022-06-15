using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステータス UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UIStatus : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// レベルテキスト UI
        /// </summary>
        [SerializeField] private Text UILevelText;

        /// <summary>
        /// 落下速度テキスト UI
        /// </summary>
        [SerializeField] private Text UIFallSpeedText;

        /// <summary>
        /// 残り的数テキスト UI
        /// </summary>
        [SerializeField] private Text UIEnemyNum;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// レベル設定
        /// </summary>
        /// <param name="level"> レベル </param>
        public void SetLevel(int level)
        {
            UILevelText.text = level.ToString();
        }

        /// <summary>
        /// 落下速度設定
        /// </summary>
        /// <param name="fallSpeedType"> 落下速度種別 </param>
        public void SetFallSpeed(FallSpeedType fallSpeedType)
        {
            UIFallSpeedText.text = fallSpeedType.ToString();
        }

        /// <summary>
        /// 残り敵数設定
        /// </summary>
        /// <param name="enemyNum"> 残り的数 </param>
        public void SetEnemyNum(int enemyNum)
        {
            UIEnemyNum.text = enemyNum.ToString();
        }
    }
}

