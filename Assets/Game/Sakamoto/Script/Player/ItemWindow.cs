using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemWindow : EditorWindow
{
    [SerializeField] int items;

    [SerializeField]
    GameObject _item1;

    [SerializeField]
    GameObject _item2;

    [SerializeField]
    GameObject _result;

    Vector2 size = new Vector2(300, 20);

    bool folding = false;

    [MenuItem("Window/ItemFusion")]
    static void Init()
    {
        var window = GetWindow<ItemWindow>("Some Window");
    }

    private void OnGUI()
    {
        size = EditorGUILayout.Vector2Field("Size", size);
        items = EditorGUILayout.IntField("s", items);

        EditorGUILayout.PrefixLabel("ÉAÉCÉeÉÄçáê¨");

        var so = new SerializedObject(this);
        so.Update();

        for (int i = 0; i < items; i++)
        {

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal(GUI.skin.box);

            EditorGUILayout.PropertyField(so.FindProperty("_item1"), true, GUILayout.Width(size.x), GUILayout.Height(size.y));
            EditorGUILayout.PropertyField(so.FindProperty("_item2"), true, GUILayout.Width(size.x), GUILayout.Height(size.y));
            EditorGUILayout.PropertyField(so.FindProperty("_result"), true, GUILayout.Width(size.x), GUILayout.Height(size.y));


            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        so.ApplyModifiedProperties();



    }


}
