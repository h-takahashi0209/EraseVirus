using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Component 拡張メソッド定義用
    /// </summary>
    public static class ComponentExtensions
    {
        //====================================
        //! 関数（public static）
        //====================================

        #region active

        /// <summary>
        /// アクティブ制御
        /// </summary>
        /// <param name="isActive"> アクティブか </param>
        public static void SetActive(this Component self, bool isActive)
        {
            self.gameObject.SetActive(isActive);
        }

        /// <summary>
        /// アクティブか
        /// </summary>
        public static bool IsActiveInHierarchy(this Component self)
        {
            return self.gameObject.activeInHierarchy;
        }

        #endregion

        #region localPosition

        /// <summary>
        /// ローカル座標取得
        /// </summary>
        public static Vector3 GetLocalPosition(this Component self)
        {
            return self.transform.localPosition;
        }

        /// <summary>
        /// ローカル X 座標取得
        /// </summary>
        public static float GetLocalPositionX(this Component self)
        {
            return self.transform.localPosition.x;
        }

        /// <summary>
        /// ローカル Y 座標取得
        /// </summary>
        public static float GetLocalPositionY(this Component self)
        {
            return self.transform.localPosition.y;
        }

        /// <summary>
        /// ローカル Z 座標取得
        /// </summary>
        public static float GetLocalPositionZ(this Component self)
        {
            return self.transform.localPosition.z;
        }

        /// <summary>
        /// ローカル座標設定
        /// </summary>
        /// <param name="localPosition"> ローカル座標 </param>
        public static void SetLocalPosition(this Component self, Vector3 localPosition)
        {
            self.transform.localPosition = localPosition;
        }

        /// <summary>
        /// ローカル X 座標設定
        /// </summary>
        /// <param name="localPositionX"> ローカル X 座標 </param>
        public static void SetLocalPositionX(this Component self, float localPositionX)
        {
            self.transform.localPosition = new Vector3(localPositionX, self.transform.localPosition.y, self.transform.localPosition.z);
        }

        /// <summary>
        /// ローカル Y 座標設定
        /// </summary>
        /// <param name="localPositionY"> ローカル Y 座標 </param>
        public static void SetLocalPositionY(this Component self, float localPositionY)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, localPositionY, self.transform.localPosition.z);
        }

        /// <summary>
        /// ローカル Z 座標設定
        /// </summary>
        /// <param name="localPositionZ"> ローカル Z 座標 </param>
        public static void SetLocalPositionZ(this Component self, float localPositionZ)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, localPositionZ);
        }

        /// <summary>
        /// ローカル座標加算
        /// </summary>
        /// <param name="localPosition"> ローカル座標 </param>
        public static void AddLocalPosition(this Component self, Vector3 localPosition)
        {
            self.transform.localPosition += localPosition;
        }

        /// <summary>
        /// ローカル X 座標加算
        /// </summary>
        /// <param name="localPositionX"> ローカル X 座標 </param>
        public static void AddLocalPositionX(this Component self, float localPositionX)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x + localPositionX, self.transform.localPosition.y, self.transform.localPosition.z);
        }

        /// <summary>
        /// ローカル Y 座標加算
        /// </summary>
        /// <param name="localPositionY"> ローカル Y 座標 </param>
        public static void AddLocalPositionY(this Component self, float localPositionY)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y + localPositionY, self.transform.localPosition.z);
        }

        /// <summary>
        /// ローカル Z 座標加算
        /// </summary>
        /// <param name="localPositionZ"> ローカル Z 座標 </param>
        public static void AddLocalPositionZ(this Component self, float localPositionZ)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, self.transform.localPosition.z + localPositionZ);
        }

        /// <summary>
        /// ローカル座標リセット
        /// </summary>
        public static void ResetLocalPosition(this Component self)
        {
            self.transform.localPosition = Vector3.zero;
        }

        #endregion

        #region position

        /// <summary>
        /// ワールド座標取得
        /// </summary>
        public static Vector3 GetWorldPosition(this Component self)
        {
            return self.transform.position;
        }

        /// <summary>
        /// ワールド X 座標取得
        /// </summary>
        public static float GetWorldPositionX(this Component self)
        {
            return self.transform.position.x;
        }

        /// <summary>
        /// ワールド Y 座標取得
        /// </summary>
        public static float GetWorldPositionY(this Component self)
        {
            return self.transform.position.y;
        }

        /// <summary>
        /// ワールド Z 座標取得
        /// </summary>
        public static float GetWorldPositionZ(this Component self)
        {
            return self.transform.position.z;
        }

        /// <summary>
        /// ワールド座標設定
        /// </summary>
        /// <param name="position"> ワールド座標 </param>
        public static void SetWorldPosition(this Component self, Vector3 position)
        {
            self.transform.position = position;
        }

        /// <summary>
        /// ワールド X 座標設定
        /// </summary>
        /// <param name="positionX"> ワールド X 座標 </param>
        public static void SetWorldPositionX(this Component self, float positionX)
        {
            self.transform.position = new Vector3(positionX, self.transform.position.y, self.transform.position.z);
        }

        /// <summary>
        /// ワールド Y 座標設定
        /// </summary>
        /// <param name="positionY"> ワールド Y 座標 </param>
        public static void SetWorldPositionY(this Component self, float positionY)
        {
            self.transform.position = new Vector3(self.transform.position.x, positionY, self.transform.position.z);
        }

        /// <summary>
        /// ワールド Z 座標設定
        /// </summary>
        /// <param name="positionZ"> ワールド Z 座標 </param>
        public static void SetWorldPositionZ(this Component self, float positionZ)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, positionZ);
        }

        /// <summary>
        /// ワールド座標加算
        /// </summary>
        /// <param name="position"> ワールド座標 </param>
        public static void AddWorldPosition(this Component self, Vector3 position)
        {
            self.transform.position += position;
        }

        /// <summary>
        /// ワールド X 座標加算
        /// </summary>
        /// <param name="positionX"> ワールド X 座標 </param>
        public static void AddWorldPositionX(this Component self, float positionX)
        {
            self.transform.position = new Vector3(self.transform.position.x + positionX, self.transform.position.y, self.transform.position.z);
        }

        /// <summary>
        /// ワールド Y 座標加算
        /// </summary>
        /// <param name="positionY"> ワールド Y 座標 </param>
        public static void AddWorldPositionY(this Component self, float positionY)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y + positionY, self.transform.position.z);
        }

        /// <summary>
        /// ワールド Z 座標加算
        /// </summary>
        /// <param name="positionZ"> ワールド Z 座標 </param>
        public static void AddWorldPositionZ(this Component self, float positionZ)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, self.transform.position.z + positionZ);
        }

        /// <summary>
        /// ワールド座標リセット
        /// </summary>
        public static void ResetWorldPosition(this Component self)
        {
            self.transform.position = Vector3.zero;
        }

        #endregion

        #region localScale

        /// <summary>
        /// スケール取得
        /// </summary>
        public static Vector3 GetLocalScale(this Component self)
        {
            return self.transform.localScale;
        }

        /// <summary>
        /// X スケール取得
        /// </summary>
        public static float GetLocalScaleX(this Component self)
        {
            return self.transform.localScale.x;
        }

        /// <summary>
        /// Y スケール取得
        /// </summary>
        public static float GetLocalScaleY(this Component self)
        {
            return self.transform.localScale.y;
        }

        /// <summary>
        /// Z スケール取得
        /// </summary>
        public static float GetLocalScaleZ(this Component self)
        {
            return self.transform.localScale.z;
        }

        /// <summary>
        /// スケール設定
        /// </summary>
        /// <param name="localScale"> スケール </param>
        public static void SetLocalScale(this Component self, Vector3 localScale)
        {
            self.transform.localScale = localScale;
        }

        /// <summary>
        /// X スケール設定
        /// </summary>
        /// <param name="localScaleX"> ローカル X スケール </param>
        public static void SetLocalScaleX(this Component self, float localScaleX)
        {
            self.transform.localScale = new Vector3(localScaleX, self.transform.localScale.y, self.transform.localScale.z);
        }

        /// <summary>
        /// Y スケール設定
        /// </summary>
        /// <param name="localScaleY"> ローカル Y スケール </param>
        public static void SetLocalScaleY(this Component self, float localScaleY)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, localScaleY, self.transform.localScale.z);
        }

        /// <summary>
        /// Z スケール設定
        /// </summary>
        /// <param name="localScaleZ"> ローカル Z スケール </param>
        public static void SetLocalScaleZ(this Component self, float localScaleZ)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y, localScaleZ);
        }

        /// <summary>
        /// ローカルスケール加算
        /// </summary>
        /// <param name="localScale"> ローカルスケール </param>
        public static void AddLocalScale(this Component self, Vector3 localScale)
        {
            self.transform.localScale += localScale;
        }

        /// <summary>
        /// ローカル X スケール加算
        /// </summary>
        /// <param name="localScaleX"> ローカル X スケール </param>
        public static void AddLocalScaleX(this Component self, float localScaleX)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x + localScaleX, self.transform.localScale.y, self.transform.localScale.z);
        }

        /// <summary>
        /// ローカル Y スケール加算
        /// </summary>
        /// <param name="localScaleY"> ローカル Y スケール </param>
        public static void AddLocalScaleY(this Component self, float localScaleY)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y + localScaleY, self.transform.localScale.z);
        }

        /// <summary>
        /// ローカル Z スケール加算
        /// </summary>
        /// <param name="localScaleZ"> ローカル Z スケール </param>
        public static void AddLocalScaleZ(this Component self, float localScaleZ)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y, self.transform.localScale.z + localScaleZ);
        }

        /// <summary>
        /// スケールリセット
        /// </summary>
        public static void ResetLocalScale(this Component self)
        {
            self.transform.localScale = Vector3.one;
        }

        #endregion

        #region eulerAngles

        /// <summary>
        /// オイラー角取得
        /// </summary>
        public static Vector3 GetEulerAngles(this Component self)
        {
            return self.transform.eulerAngles;
        }

        /// <summary>
        /// オイラー X 角取得
        /// </summary>
        public static float GetEulerAnglesX(this Component self)
        {
            return self.transform.eulerAngles.x;
        }

        /// <summary>
        /// オイラー Y 角取得
        /// </summary>
        public static float GetEulerAnglesY(this Component self)
        {
            return self.transform.eulerAngles.y;
        }

        /// <summary>
        /// オイラー Z 角取得
        /// </summary>
        public static float GetEulerAnglesZ(this Component self)
        {
            return self.transform.eulerAngles.z;
        }

        /// <summary>
        /// オイラー角設定
        /// </summary>
        /// <param name="eulerAngles"> オイラー角 </param>
        public static void SetEulerAngles(this Component self, Vector3 eulerAngles)
        {
            self.transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// オイラー X 角設定
        /// </summary>
        /// <param name="eulerAnglesX"> オイラー X 角 </param>
        public static void SetEulerAnglesX(this Component self, float eulerAnglesX)
        {
            self.transform.eulerAngles = new Vector3(eulerAnglesX, self.transform.eulerAngles.y, self.transform.eulerAngles.z);
        }

        /// <summary>
        /// オイラー Y 角設定
        /// </summary>
        /// <param name="eulerAnglesY"> オイラー Y 角 </param>
        public static void SetEulerAnglesY(this Component self, float eulerAnglesY)
        {
            self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, eulerAnglesY, self.transform.eulerAngles.z);
        }

        /// <summary>
        /// オイラー Z 角設定
        /// </summary>
        /// <param name="eulerAnglesZ"> オイラー Z 角 </param>
        public static void SetEulerAnglesZ(this Component self, float eulerAnglesZ)
        {
            self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, self.transform.eulerAngles.y, eulerAnglesZ);
        }

        #endregion

        #region transform

        /// <summary>
        /// 親設定
        /// </summary>
        /// <param name="parent"> 親 </param>
        public static void SetParent(this Component self, Transform parent)
        {
            self.transform.SetParent(parent);
        }

        /// <summary>
        /// ルート取得
        /// </summary>
        public static Transform GetRoot(this Component self)
        {
            return self.transform.root;
        }

        #endregion
    }
}
