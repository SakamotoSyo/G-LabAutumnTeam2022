using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AudioManager
{

    [Tooltip("Audio�̏����i�[���Ă���ϐ�")] AudioDataBase _params = default;
    [Tooltip("Count����ϐ�")] int _poolCount = 0;
    [Tooltip("��������Obj��ۑ�����Poll�N���X��List")] List<Pool> pool = new List<Pool>();
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

    /// <summary>Prefab�̐���������</summary>
    AudioManager()
    {
        //resources�t�H���_����ǂݍ��ޏ����������ɋL�q����B
        _params = Resources.Load<AudioDataBase>("AudioDataBase");

        if (_params == null)
        {
            Debug.LogError("AudioDataBase��������܂���");
        }
        CreatePool();
    }

    /// <summary>�e��Prefab�𐶐�����</summary>
    public void CreatePool()
    {
        //�S�Ă̐������I�������return
        if (_poolCount >= _params.paramsList.Count)
        {
            return;
        }

        //�ݒ肵�Ă���Prefab�̐���������
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
    /// ���𗬂��Ƃ��ɌĂяo���֐�
    /// </summary>
    /// <param name="type">���������T�E���h�̎��</param>
    /// <returns>���𗬂�GameObject</returns>
    public GameObject PlaySound(SoundPlayType type)
    {
        //Debug.Log("�Đ�");
        foreach (var pool in pool)
        {
            if (pool.Obj.activeSelf == false && pool.Type == type)
            {
                pool.Obj.SetActive(true);
                return pool.Obj;
            }
        }

        //�����������Ă��镪�ő���Ȃ��Ȃ�����A�V������������
        var newObj = Object.Instantiate(_params.paramsList.Find(x => x.Type == type).SoundPrefab);
        SavePool(newObj, type);
        return newObj;
    }

    /// <summary>
    /// ���������I�u�W�F�N�g��List�ɒǉ����ĕۑ�����֐�
    /// </summary>
    /// <param name="obj">���������I�u�W�F�N�g</param>
    /// <param name="type">���������I�u�W�F�N�g��Type</param>
    void SavePool(GameObject obj, SoundPlayType type)
    {
        pool.Add(new Pool { Obj = obj, Type = type });
    }

    public void Reset()
    {
        pool.Clear();
    }

    /// <summary>������������ۑ����Ă������߂̃N���X</summary>
    private struct Pool
    {
        public GameObject Obj { get; set; }
        public SoundPlayType Type { get; set; }
    }
}

/// <summary>
/// �炵�������̎��
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

