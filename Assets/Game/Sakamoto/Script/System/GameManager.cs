using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("�n�܂�܂ł̕b��")]
    [SerializeField] float _startTime;


    /// <summary>Game���X�^�[�g����Ƃ��ɌĂ�</summary>
    public static event Action GameStart;
    /// <summary>Game��Pause������Ƃ��ɌĂ�</summary>
    public static event Action GamePause;
    /// <summary>Game���I������Ƃ��ɌĂ�</summary>
    public static event Action GameEnd;

    async void Start()
    {
        //�X�^�[�g�܂őҋ@
        await StartDeley();
        OnBiginTurn();
    }

    /// <summary>�Q�[�����J�n����Ƃ��ɌĂԊ֐�</summary>
    public static void OnBiginTurn()
    {
        GameStart();
    }

    /// <summary>�Q�[�����I������Ƃ��ɌĂԊ֐�</summary>
    public static void OnEndTurn()
    {
        GameEnd();
    }

    async UniTask StartDeley() 
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_startTime));
    }
}