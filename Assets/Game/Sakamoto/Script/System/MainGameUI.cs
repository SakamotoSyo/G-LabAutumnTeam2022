using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
using UniRx;
public class MainGameUI : MonoBehaviour
{
    [Header("�J�E���g�Ɏg��Image")]
    [SerializeField] Image _timeImage;
    [Header("�J�E���g�_�E����Sprite")]
    [SerializeField] Sprite[] _sprites = new Sprite[2];
    [Header("Game�̐�������")]
    [SerializeField] int _gameTime;
    [Header("Timer��Text")]
    [SerializeField] Text _timerText;
    [Header("Score��\������Text")]
    [SerializeField] Text _scoreText;
    [Header("MainGameUI��Presenter")]
    [SerializeField] GameUIPresenter _gameUIPresenter;

    [Tooltip("�n�܂�܂ł̕b��")]
    readonly float _startTime = 3;

    async void Start() 
    {
        _gameUIPresenter.Start();
        await StartDeley();
    }

    /// <summary>
    /// �J�E���g�_�E�������ăQ�[�����X�^�[�g������
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
