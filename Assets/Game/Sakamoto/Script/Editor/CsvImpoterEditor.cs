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
            string path = "Assets/Game/Sakamoto/Data/Item/ItemData/" + split[0] + ".asset";
            //アイテムのインスタンスをメモリ上に作成
            var itemData = CreateInstance<ItemData>();

            //合成アイテム1
            itemData.ItemName = split[0];

            //加工できるか
            if (split[1] == "できる")
            {
                itemData.Processing = true;
            }
            else 
            {
                itemData.Processing = false;
            } 
            //製造できるか
            if (split[2] == "できる")
            {
                itemData.Craft = true;
            }
            else 
            {
                itemData.Craft = false;
            }
            
            if (split[3] == "できる")
            {
                itemData.Packing = true;
            }
            else 
            {
                itemData.Packing = false;
            }

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
