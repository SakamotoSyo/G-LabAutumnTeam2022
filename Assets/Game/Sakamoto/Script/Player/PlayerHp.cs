using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerHp : MonoBehaviour, IDamage
{
    public float MaxHp => _playerHp;
    public float CurrentHp => _playerHp;

    [Header("PlayerのMaxHp")]
    [SerializeField] float _playerMaxHp;
    [Tooltip("プレイヤーのHp")]
    float _playerHp;

    public Action<float> OnHealth;
    /// <summary>HPが０になった時通知するAction</summary>
    public Action OnDead;

    /// <summary>
    /// ダメージを受けるときに呼ばれる関数
    /// </summary>
    /// <param name="amount">ダメージ数</param>
    public void ApplyDamage(float amount)
    {
        _playerHp -= amount;
        OnHealth(_playerHp);
        //体力が０以下になった時通知する
        if (_playerHp <= 0)
        {
            OnDead?.Invoke();
        }
    }

    /// <summary>
    /// 回復するときに呼ぶ関数
    /// </summary>
    /// <param name="amount">回復する量</param>
    public void ApplyHeal(float amount)
    {
        _playerHp += amount;
    }
}
