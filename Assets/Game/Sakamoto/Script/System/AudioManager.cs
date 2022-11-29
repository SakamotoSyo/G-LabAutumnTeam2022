using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AudioManager
{

    [Tooltip("Audioの情報を格納している変数")] AudioDataBase _params = default;
    [Tooltip("Countする変数")] int _poolCount = 0;
    [Tooltip("生成したObjを保存するPollクラスのList")] List<Pool> pool = new List<Pool>();
    static AudioManager _instance = new AudioManager();

    static public AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AudioManager();
            }
            return _instance;
        }
    }

    /// <summary>Prefabの生成をする</summary>
    AudioManager()
    {
        string data = "Assets/Game/sakamoto/Data/AudioDataBase/AudioDataBase.asset";

        //var path = AssetDatabase.GUIDToAssetPath(data[0]);
#if UNITY_EDITOR
        _params = AssetDatabase.LoadAssetAtPath<AudioDataBase>(data);
#endif
        if (_params == null)
        {
            Debug.LogError("AudioDataBaseが見つかりません");
        }
        CreatePool();
    }

    /// <summary>各種Prefabを生成する</summary>
    public void CreatePool()
    {
        //全ての生成が終わったらreturn
        if (_poolCount >= _params.paramsList.Count)
        {
            return;
        }

        //設定してあるPrefabの生成をする
        for (int i = 0; i < _params.paramsList[_poolCount].MaxCount; i++)
        {
            var obj = Object.Instantiate(_params.paramsList[_poolCount].SoundPrefab);
            obj.SetActive(false);
            SavePool(obj, _params.paramsList[_poolCount].Type);
        }

        _poolCount++;
        CreatePool();
    }

    /// <summary>
    /// 音を流すときに呼び出す関数
    /// </summary>
    /// <param name="type">流したいサウンドの種類</param>
    /// <returns>音を流すGameObject</returns>
    public GameObject PlaySound(SoundPlayType type)
    {
        //Debug.Log("再生");
        foreach (var pool in pool)
        {
            if (pool.Obj.activeSelf == false && pool.Type == type)
            {
                pool.Obj.SetActive(true);
                return pool.Obj;
            }
        }

        //もし生成してある分で足らなくなったら、新しく生成する
        var newObj = Object.Instantiate(_params.paramsList.Find(x => x.Type == type).SoundPrefab);
        SavePool(newObj, type);
        return newObj;
    }

    /// <summary>
    /// 生成したオブジェクトをListに追加して保存する関数
    /// </summary>
    /// <param name="obj">生成したオブジェクト</param>
    /// <param name="type">生成したオブジェクトのType</param>
    void SavePool(GameObject obj, SoundPlayType type)
    {
        pool.Add(new Pool { Obj = obj, Type = type });
    }

    /// <summary>生成した音を保存しておくためのクラス</summary>
    private struct Pool
    {
        public GameObject Obj { get; set; }
        public SoundPlayType Type { get; set; }
    }
}

/// <summary>
/// 鳴らしたい音の種類
/// </summary>
public enum SoundPlayType
{
    PickUp,
    Put,
}

