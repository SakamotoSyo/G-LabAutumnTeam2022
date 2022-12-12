using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
using UniRx;
public class MainGameUI : MonoBehaviour
{
    [Header("カウントに使うImage")]
    [SerializeField] Image _timeImage;
    [Header("カウントダウンのSprite")]
    [SerializeField] Sprite[] _sprites = new Sprite[2];
    [Header("Gameの制限時間")]
    [SerializeField] int _gameTime;
    [Header("TimerのText")]
    [SerializeField] Text _timerText;
    [Header("Scoreを表示するText")]
    [SerializeField] Text _scoreText;
    [Header("MainGameUIのPresenter")]
    [SerializeField] GameUIPresenter _gameUIPresenter;

    [Tooltip("始まるまでの秒数")]
    readonly float _startTime = 3;

    async void Start() 
    {
        _gameUIPresenter.Start();
        await StartDeley();
    }

    /// <summary>
    /// カウントダウンさせてゲームをスタートさせる
    /// </summary>
    /// <returns></returns>
    async UniTask StartDeley()
    {
        _timeImage.enabled = true;
        for (int i = 0; i < _sprites.Length; i++)
        {
            _timeImage.sprite = _sprites[i];
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        _timeImage.enabled = false;
        GameManager.OnBiginTurn();
        await TimerCount();
        GameManager.OnEndTurn();
    }
    public void SetScore(int score) 
    {
        _scoreText.text = score.ToString();
    }


    async UniTask TimerCount() 
    {
        var TimeCurrent = _gameTime;
        while (TimeCurrent > 0) 
        {
            TimeCurrent--;
            _timerText.text = TimeCurrent.ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }

}

[Serializable]
public class GameUIPresenter 
{
    [SerializeField] MainGameUI _gameUI;

    public void Start() 
    {
        GameManager.Score.Subscribe(Value => _gameUI.SetScore(Value));
    }
}
