using UnityEditor;
using UnityEngine;

namespace TRPG.Tools {
    [CustomPropertyDrawer(typeof(Tile))]
    public class TileDrawer : PropertyDrawer
    {
        private const float MIN_COORDINATE_SIZE = 28f;
        private const float MIN_COORDINATE_VALUE_SIZE = 70f;
        private const float LABEL_OFFSET = 0f;// 12f;
        private const float TEXTFIELD_OFFSET = 0f;// 16f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var drawPos = new Vector2(position.x, position.y);

            // EditorGUI.BeginProperty(position, label, property);

            var xProp = property.FindPropertyRelative("position").FindPropertyRelative("x");
            var yProp = property.FindPropertyRelative("position").FindPropertyRelative("y");
            var zProp = property.FindPropertyRelative("position").FindPropertyRelative("z");
            var viewProp = property.FindPropertyRelative("View");

            EditorGUI.LabelField(new Rect(drawPos, new Vector2( MIN_COORDINATE_SIZE, EditorGUIUtility.singleLineHeight)), "X");
            drawPos.x += MIN_COORDINATE_SIZE - TEXTFIELD_OFFSET;
            xProp.intValue = EditorGUI.IntField(new Rect(drawPos, new Vector2(MIN_COORDINATE_VALUE_SIZE, EditorGUIUtility.singleLineHeight)), xProp.intValue);
            drawPos.x += MIN_COORDINATE_VALUE_SIZE - LABEL_OFFSET;
            EditorGUI.LabelField(new Rect(drawPos, new Vector2( MIN_COORDINATE_SIZE, EditorGUIUtility.singleLineHeight)), "Y");
            drawPos.x += MIN_COORDINATE_SIZE - TEXTFIELD_OFFSET;
            yProp.intValue = EditorGUI.IntField(new Rect(drawPos, new Vector2(MIN_COORDINATE_VALUE_SIZE, EditorGUIUtility.singleLineHeight)), yProp.intValue);
            drawPos.x += MIN_COORDINATE_VALUE_SIZE - LABEL_OFFSET;
            EditorGUI.LabelField(new Rect(drawPos, new Vector2( MIN_COORDINATE_SIZE, EditorGUIUtility.singleLineHeight)), "Z");
            drawPos.x += MIN_COORDINATE_SIZE - TEXTFIELD_OFFSET;
            zProp.intValue = EditorGUI.IntField(new Rect(drawPos, new Vector2(MIN_COORDINATE_VALUE_SIZE, EditorGUIUtility.singleLineHeight)), zProp.intValue);
            drawPos.x += MIN_COORDINATE_VALUE_SIZE - LABEL_OFFSET;

            // EditorGUI.EndProperty();

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
