using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(CsvImporter))]
public class CsvImpoterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var importer = (CsvImporter)target;
        DrawDefaultInspector();

        if (GUILayout.Button("アイテム合成データの作成")) 
        {
            Debug.Log("aaaa");
            SetData(importer);
        }
    }

    /// <summary>csvファイルのデータからScriptableObjectを作成する</summary>
    void SetData(CsvImporter csvImpoter) 
    {
        if(csvImpoter.csvFile == null) 
        {
            Debug.LogError("CSVファイルがセットされていません");
            return;
        }

        //テキストの読み込み
        StringReader sr = new StringReader(csvImpoter.csvFile.text);
        //最初の一行目はスキップ
        sr.ReadLine();

        while(true) 
        {
            //一行ずつ読み込む
            string line = sr.ReadLine();
            if (string.IsNullOrEmpty(line)) 
            {
                break;
            }

            string[] split = line.Split(',');
            Debug.Log(split[0]);
            //ファイルを作成するためのパス
            string path = "Assets/Game/Sakamoto/Item/ItemData/" + split[2] + ".asset";
            //アイテムのインスタンスをメモリ上に作成
            var itemData = CreateInstance<ItemSynthetic>();

            //合成アイテム1
            itemData._item1 = split[0];
            //合成アイテム２
            itemData._item2 = split[1];
            //結果のアイテム
            itemData._resultItem = split[2];

            var asset = (ItemSynthetic)AssetDatabase.LoadAssetAtPath(path, typeof(ItemSynthetic));

            if (asset == null)
            {
                //指定したパスが存在しなかった場合新規作成
                AssetDatabase.CreateAsset(itemData, path);
            }
            else 
            {
                //指定したパスと同名のファイルが存在した場合はデータの更新
                EditorUtility.CopySerialized(itemData, asset);
                AssetDatabase.SaveAssets();
            }

            AssetDatabase.Refresh();
        }
        Debug.Log("アイテム合成データの作成完了");
    }
}
