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
        //resourcesフォルダから読み込む処理をここに記述する。
        _params = Resources.Load<AudioDataBase>("AudioDataBase");

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

    public void Reset()
    {
        pool.Clear();
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
    SE_snowman_walk,
    SE_snowman_yukiwalk,
    SE_lost,
    SE_door_open,
    SE_door_close,
    SE_conveyor_run,
    SE_conveyor_stop,
    SE_conveyor_start,
    SE_processing_cloth,
    SE_processing_wood,
    SE_processing_iron,
    SE_manufacture_run,
    SE_manufacture_lamp,
    SE_manufacture_lastlamp,
    SE_manufacture_explosion,
    SE_packing,
    SE_snowman_collide,
    SE_present_give,
    SE_snowman_damage,
    SE_fire_finish,
    SE_fire,
    SE_recipe,
    SE_snowman_recover,
    SE_snowman_big,
    SE_snowman_death,
    SE_snowman_small,
    SE_countdown,
    SE_stage_finish,
    SE_flash,
    SE_1minute,
    SE_manufacture_repair,
    SE_manufacture_perfect,
    SE_manufacture_repair_end,
    SE_manufacture_alert_01,
    SE_manufacture_alert_02,
    SE_manufacture_alert_03,
    SE_manufacture_alert_04,
    SE_recipe_delete,
    SE_result_rank,
    SE_enter,
    SE_select,
}

