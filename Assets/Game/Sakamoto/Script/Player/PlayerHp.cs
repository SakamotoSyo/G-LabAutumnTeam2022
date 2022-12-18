using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
public class PlayerHp : MonoBehaviour, IDamage
{
    public float MaxHp => _playerMaxHp;
    public float CurrentHp => _playerHp;

    [Header("PlayerのMaxHp")]
    [SerializeField] float _playerMaxHp;
    [Header("１秒ごとに何ダメージを受けるか")]
    [SerializeField] float _damage;
    [Tooltip("プレイヤーのHpがなくなったかどうか")]
    bool _isDead = false;
    [Tooltip("プレイヤーのHp")]
    float _playerHp;

    public Action<float> OnHealth;
    /// <summary>HPが０になった時通知するAction</summary>
    public Action OnDead;

    private async void Start()
    {
        _playerHp = _playerMaxHp;
        await RegularlyDamage();
    }

    /// <summary>
    /// ダメージを受けるときに呼ばれる関数
    /// </summary>
    /// <param name="amount">ダメージ数</param>
    public void ApplyDamage(float amount)
    {
        if (_isDead)
        {
            _playerHp = 0;
        }
        else 
        {
            _playerHp -= amount;
            OnHealth(_playerHp);
        }

        //体力が０以下になった時通知する
        if (_playerHp <= 0 && !_isDead)
        {
            OnDead?.Invoke();
            _isDead = true;
        }
    }

    /// <summary>
    /// 回復するときに呼ぶ関数
    /// </summary>
    /// <param name="amount">回復する量</param>
    public void ApplyHeal(float amount)
    {   
        _playerHp += amount;
        if (_playerMaxHp < _playerHp) 
        {
            _playerHp = _playerMaxHp;
        }
        _isDead = false;
    }

    private async UniTask RegularlyDamage()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            ApplyDamage(_damage);
        }
    }
}
