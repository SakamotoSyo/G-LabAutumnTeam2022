using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class GameManager
{
    public static IObservable<int> Score => score;
    [Tooltip("�X�R�A��ۑ����Ă����ϐ�")]
    private static ReactiveProperty<int> score = new();

    /// <summary>Game���X�^�[�g����Ƃ��ɌĂ�Action</summary>
    public static event Action GameStart;
    /// <summary>Game��Pause������Ƃ��ɌĂ�Action</summary>
    public static event Action GamePause;
    /// <summary>Game���I������Ƃ��ɌĂ�Action</summary>
    public static event Action GameEnd;

    public static void AddScore(int scoreNum)
    {
        score.Value += scoreNum;
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


}