using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// 敵
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Enemy : Object
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// オブジェクト種別
        /// </summary>
        public override ObjectType ObjectType => ObjectType.Enemy;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="colorType">    色種別           </param>
        /// <param name="blockPosX">    Xブロック座標    </param>
        /// <param name="blockPosY">    Yブロック座標    </param>
        public void Setup(ColorType colorType, int blockPosX, int blockPosY)
        {
            ColorType = colorType;
            BlockPosX = blockPosX;
            BlockPosY = blockPosY;

            var sprite = SpriteLoader.GetEnemySprite(colorType);

            UIObject.Setup(sprite);
        }
    }
}
