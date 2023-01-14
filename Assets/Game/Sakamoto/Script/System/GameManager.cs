using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager
{
    public static int ScoreNum => score.Value;
    public static IObservable<int> Score => score;
    [Tooltip("スコアを保存しておく変数")]
    private static ReactiveProperty<int> score = new ReactiveProperty<int>();

    /// <summary>Gameがスタートするときに呼ぶAction</summary>
    public static event Action GameStart;
    /// <summary>GameをPauseさせるときに呼ぶAction</summary>
    public static event Action GamePause;
    /// <summary>Gameを終了するときに呼ぶAction</summary>
    public static event Action GameEnd;

    public static void AddScore(int scoreNum)
    {
        score.Value += scoreNum;
    }

    public static void ResetScore() 
    {
        score.Value = 0;
    }

    /// <summary>ゲームを開始するときに呼ぶ関数</summary>
    public static void OnBiginTurn()
    {
        GameStart?.Invoke();
    }

    /// <summary>ゲームを終了するときに呼ぶ関数</summary>
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