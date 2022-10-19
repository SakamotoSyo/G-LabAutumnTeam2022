using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//没スクリプト
public class ItemWindow : EditorWindow
{
    [SerializeField] int items;

    [SerializeField]
    List<GameObject> _item1 = new List<GameObject>();

    [SerializeField]
    List<GameObject> _item2 = new List<GameObject>();

    [SerializeField]
    List<GameObject> _result = new List<GameObject>();

    Vector2 size = new Vector2(300, 20);

    //bool folding = false;

    [MenuItem("Window/ItemFusion")]
    static void Init()
    {
        var window = GetWindow<ItemWindow>("Some Window");
    }

    private void OnGUI()
    {
        size = EditorGUILayout.Vector2Field("Size", size);
        items = EditorGUILayout.IntField("s", items);

        EditorGUILayout.PrefixLabel("アイテム合成");

        var so = new SerializedObject(this);
        so.Update();

        using (new EditorGUILayout.HorizontalScope()) 
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(so.FindProperty("_item1"), true, GUILayout.Width(size.x), GUILayout.Height(size.y));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(so.FindProperty("_item2"), true, GUILayout.Width(size.x), GUILayout.Height(size.y));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(so.FindProperty("_result"), true, GUILayout.Width(size.x), GUILayout.Height(size.y));
          GUILayout.FlexibleSpace();
        }


        so.ApplyModifiedProperties();

    }

}
