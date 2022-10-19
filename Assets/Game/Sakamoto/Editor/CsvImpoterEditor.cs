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

        if (GUILayout.Button("�A�C�e�������f�[�^�̍쐬")) 
        {
            Debug.Log("aaaa");
            SetData(importer);
        }
    }

    /// <summary>csv�t�@�C���̃f�[�^����ScriptableObject���쐬����</summary>
    void SetData(CsvImporter csvImpoter) 
    {
        if(csvImpoter.csvFile == null) 
        {
            Debug.LogError("CSV�t�@�C�����Z�b�g����Ă��܂���");
            return;
        }

        //�e�L�X�g�̓ǂݍ���
        StringReader sr = new StringReader(csvImpoter.csvFile.text);
        //�ŏ��̈�s�ڂ̓X�L�b�v
        sr.ReadLine();

        while(true) 
        {
            //��s���ǂݍ���
            string line = sr.ReadLine();
            if (string.IsNullOrEmpty(line)) 
            {
                break;
            }

            string[] split = line.Split(',');
            Debug.Log(split[0]);
            //�t�@�C�����쐬���邽�߂̃p�X
            string path = "Assets/Game/Sakamoto/Item/ItemData/" + split[2] + ".asset";
            //�A�C�e���̃C���X�^���X����������ɍ쐬
            var itemData = CreateInstance<ItemSynthetic>();

            //�����A�C�e��1
            itemData._item1 = split[0];
            //�����A�C�e���Q
            itemData._item2 = split[1];
            //���ʂ̃A�C�e��
            itemData._resultItem = split[2];

            var asset = (ItemSynthetic)AssetDatabase.LoadAssetAtPath(path, typeof(ItemSynthetic));

            if (asset == null)
            {
                //�w�肵���p�X�����݂��Ȃ������ꍇ�V�K�쐬
                AssetDatabase.CreateAsset(itemData, path);
            }
            else 
            {
                //�w�肵���p�X�Ɠ����̃t�@�C�������݂����ꍇ�̓f�[�^�̍X�V
                EditorUtility.CopySerialized(itemData, asset);
                AssetDatabase.SaveAssets();
            }

            AssetDatabase.Refresh();
        }
        Debug.Log("�A�C�e�������f�[�^�̍쐬����");
    }
}
