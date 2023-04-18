using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorInHierarchy : MonoBehaviour
{
#if UNITY_EDITOR
    private static Dictionary<Object, ColorInHierarchy> _coloredObjects = new Dictionary<Object, ColorInHierarchy>();

    static ColorInHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID); //인스턴스 아이디를 주면 오브젝트를 반환한다.

        if (obj != null && _coloredObjects.ContainsKey(obj))
        {
            GameObject gameObj = obj as GameObject;
            ColorInHierarchy cih = gameObj.GetComponent<ColorInHierarchy>();

            if (cih != null)
            {
                PaintObject(obj, selectionRect, cih);
            }
            else
            {
                _coloredObjects.Remove(obj);
            }
        }
    }

    public static void PaintObject(Object obj, Rect selectionRect, ColorInHierarchy cih)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        if (Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, cih.backColor);
            string name = $"{cih.prefix}{obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = cih.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }
    public string prefix;
    public Color backColor;
    public Color fontColor;
    private void Reset()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if (_coloredObjects.ContainsKey(this.gameObject) == false)
        {
            _coloredObjects.Add(this.gameObject, this);
        }
    }
#endif
}
