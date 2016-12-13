using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Index))]
public class IndexInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var contentRect = EditorGUI.PrefixLabel(position, label);
        contentRect.width *= 0.5f;

        var lineRect = contentRect;

        var columnRect = contentRect;
        columnRect.x += lineRect.width;

        EditorGUIUtility.labelWidth = 28f;

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(lineRect, property.FindPropertyRelative("Line"), new GUIContent("Lin."));
        EditorGUI.PropertyField(columnRect, property.FindPropertyRelative("Column"), new GUIContent("Col."));
        EditorGUI.EndProperty();
    }
}

