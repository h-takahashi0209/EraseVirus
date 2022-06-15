using System;
using UnityEngine;


namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// ステージローダー
    /// </summary>
    public sealed class StageLoader
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// オブジェクト識別子
        /// </summary>
        private enum ObjectId
        {
            None                ,
            EnemyRed            ,
            EnemyBlue           ,
            EnemyYellow         ,
            HalfCapsuleRed      ,
            HalfCapsuleBlue     ,
            HalfCapsuleYellow   ,
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// ステージを読み込んでオブジェクトをスタックに追加
        /// </summary>
        /// <param name="level">          レベル                  </param>
        /// <param name="objectStack">    オブジェクトスタック    </param>
        /// <returns>                     読み込みに成功したか    </returns>
        public bool LoadAndPushStack(int level, ObjectStack objectStack)
        {
            var path            = $"{Path.Scenes.GameScene.StageCsv}{level}";
            var stageTextAsset  = Resources.Load<TextAsset>(path);

            // ファイル読み込み失敗
            if (stageTextAsset == null || stageTextAsset.text.Length <= 0)
            {
                Debug.Assert(false, $"ステージファイルの読み込みに失敗しました。Path : {path}");
                return false;
            }

            var lineList = stageTextAsset.text.Split("\r\n");

            // ゲーム上は左下を (0, 0) とする
            // テキスト上だと先頭は (15, 0) なので反転させる
            Array.Reverse(lineList);

            objectStack.ClearStack();

            for (int y = 0; y < lineList.Length; y++)
            {
                var line = lineList[y];

                for (int x = 0; x < line.Length; x++)
                {
                    if (int.TryParse(line.Substring(x, 1), out int result))
                    {
                        var objectId = (ObjectId)result;

                        switch (objectId)
                        {
                            case ObjectId.EnemyRed           : objectStack.PushEnemy(x, y, ColorType.Red);                  break;
                            case ObjectId.EnemyBlue          : objectStack.PushEnemy(x, y, ColorType.Blue);                 break;
                            case ObjectId.EnemyYellow        : objectStack.PushEnemy(x, y, ColorType.Yellow);               break;
                            case ObjectId.HalfCapsuleRed     : objectStack.PushHalfCapsule(x, y, ColorType.Red, 0, 0);      break;
                            case ObjectId.HalfCapsuleBlue    : objectStack.PushHalfCapsule(x, y, ColorType.Blue, 0, 0);     break;
                            case ObjectId.HalfCapsuleYellow  : objectStack.PushHalfCapsule(x, y, ColorType.Yellow, 0, 0);   break;
                        }
                    }
                }
            }

            // 敵が1体も配置されていなければゲームを進行できないので止める
            if (objectStack.UsedObjectList.NotAny(obj => obj.ObjectType == ObjectType.Enemy))
            {
                Debug.Assert(false, "ステージに敵が1体も配置されていません。");
                return false;
            }

            return true;
        }
    }
}

