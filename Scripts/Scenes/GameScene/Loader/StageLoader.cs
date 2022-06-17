using System;
using System.Collections.Generic;
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

        /// <summary>
        /// ウイルスのレベルごとの抽選数
        /// </summary>
        private const int LotteryVirusNum = 4;

        /// <summary>
        /// ウイルス抽選時のレベルごとの縦ブロック上限
        /// </summary>
        private IReadOnlyList<int> LotteryVirusHeightLimitList = new int[]
        {
            10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 12, 12, 13, 13
        };


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="objectStack">    オブジェクトスタック    </param>
        /// <returns>                     読み込みに成功したか    </returns>
        public bool Setup(int level, ObjectStack objectStack)
        {
            objectStack.ClearStack();

            switch (Settings.GenerateStageMode)
            {
                case GenerateStageMode.LoadFile : return LoadFile (level, objectStack);
                case GenerateStageMode.Random   : return Lottery  (level, objectStack);
            }

            return false;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// ファイルを読み込んでオブジェクトをステージに配置
        /// </summary>
        /// <param name="level">          レベル                  </param>
        /// <param name="objectStack">    オブジェクトスタック    </param>
        /// <returns>                     読み込みに成功したか    </returns>
        private bool LoadFile(int level, ObjectStack objectStack)
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

            for (int y = 0; y < lineList.Length; y++)
            {
                var line = lineList[y];

                for (int x = 0; x < line.Length; x++)
                {
                    if (int.TryParse(line.Substring(x, 1), out int objectId))
                    {
                        PushObject((ObjectId)objectId, objectStack, x, y);
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

        /// <summary>
        /// オブジェクトを抽選してステージに配置
        /// </summary>
        /// <param name="level">          レベル                  </param>
        /// <param name="objectStack">    オブジェクトスタック    </param>
        /// <returns>                     抽選に成功したか        </returns>
        private bool Lottery(int level, ObjectStack objectStack)
        {
            if (level < 0 || level >= LotteryVirusHeightLimitList.Count)
            {
                Debug.Assert(false, $"範囲外のレベルが渡されました。Level : {level}");
                return false;
            }

            int virusNum     = (level + 1) * LotteryVirusNum;
            int blockMax     = GameSceneDef.BlockNumWidth * LotteryVirusHeightLimitList[level];
            var lotteryBlock = new List<int>();

            for (int i = 0; i < virusNum; i++)
            {
                int block;

                while (true)
                {
                    block = UnityEngine.Random.Range(0, blockMax - 1);

                    if (!lotteryBlock.Contains(block)) {
                        break;
                    }
                }

                int x = block % GameSceneDef.BlockNumWidth;
                int y = block / GameSceneDef.BlockNumWidth;

                var objectId = (ObjectId)UnityEngine.Random.Range((int)ObjectId.EnemyRed, (int)ObjectId.EnemyYellow + 1);

                PushObject(objectId, objectStack, x, y);

                lotteryBlock.Add(block);
            }

            return true;
        }

        /// <summary>
        /// オブジェクトをスタックに追加
        /// </summary>
        /// <param name="objectId">       オブジェクト ID         </param>
        /// <param name="objectStack">    オブジェクトスタック    </param>
        /// <param name="x">              Xブロック座標           </param>
        /// <param name="y">              Yブロック座標           </param>
        private void PushObject(ObjectId objectId, ObjectStack objectStack, int x, int y)
        {
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

