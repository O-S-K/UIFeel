#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RandomRangeAttribute))]
public class RandomRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RandomRangeAttribute range = (RandomRangeAttribute)attribute;

        // Hiển thị thuộc tính và nút Random
        EditorGUI.BeginProperty(position, label, property);
        Rect fieldRect = new Rect(position.x, position.y, position.width - 60, position.height);
        Rect buttonRect = new Rect(position.x + position.width - 55, position.y, 55, position.height);

        // Hiển thị trường giá trị
        property.floatValue = EditorGUI.FloatField(fieldRect, label, property.floatValue);

        // Hiển thị nút Random
        if (GUI.Button(buttonRect, "Random"))
        {
            property.floatValue = Random.Range(range.Min, range.Max);
        }

        EditorGUI.EndProperty();
    }
}
#endif