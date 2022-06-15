using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace TakahashiH
{
    /// <summary>
    /// インスペクタのラベル名を指定した文字列で置き換えるアトリビュート
    /// </summary>
    public sealed class LabelAttribute : PropertyAttribute
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 表示する文字列
        /// </summary>
        public string Label;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="label"> 表示する文字列 </param>
        public LabelAttribute(string label)
        {
            Label = label;
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// LabelAttribute 描画用クラス
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public sealed class LabelDrawer : PropertyDrawer
    {
        //====================================
        //! 関数（PropertyDrawer）
        //====================================

        /// <summary>
        /// 描画
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var labelAttribute = attribute as LabelAttribute;

            EditorGUI.PropertyField(position, property, new GUIContent(labelAttribute.Label));

            EditorGUI.EndProperty();
        }
    }

#endif

}