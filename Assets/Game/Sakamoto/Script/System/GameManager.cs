using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager
{
    public static int ScoreNum => score.Value;
    public static IObservable<int> Score => score;
    [Tooltip("�X�R�A��ۑ����Ă����ϐ�")]
    private static ReactiveProperty<int> score = new ReactiveProperty<int>();

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

    public static void ResetScore() 
    {
        score.Value = 0;
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

    public static void MainGameEnd() 
    {
        GameStart = null;
        GameEnd = null; 
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("ResultScene");
    }

}