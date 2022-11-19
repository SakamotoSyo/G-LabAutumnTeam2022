using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("�n�܂�܂ł̕b��")]
    [SerializeField] float _startTime;

    [Tooltip("�X�R�A��ۑ����Ă����ϐ�")]
    private static int score;

    /// <summary>Game���X�^�[�g����Ƃ��ɌĂ�Action</summary>
    public static event Action GameStart;
    /// <summary>Game��Pause������Ƃ��ɌĂ�Action</summary>
    public static event Action GamePause;
    /// <summary>Game���I������Ƃ��ɌĂ�Action</summary>
    public static event Action GameEnd;

    async void Start()
    {
        //�X�^�[�g�܂őҋ@
        await StartDeley();
        OnBiginTurn();
    }

    public void AddScore(int scoreNum) 
    {
        score += scoreNum;
    }

    /// <summary>�Q�[�����J�n����Ƃ��ɌĂԊ֐�</summary>
    public static void OnBiginTurn()
    {
        GameStart?.Invoke();
    }

    /// <summary>�Q�[�����I������Ƃ��ɌĂԊ֐�</summary>
    public static void OnEndTurn()
    {
        GameEnd?.Invoke();
    }

    async UniTask StartDeley() 
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_startTime));
    }
}