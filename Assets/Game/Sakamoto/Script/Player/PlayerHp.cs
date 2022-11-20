using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerHp : MonoBehaviour, IDamage
{
    public float MaxHp => _playerHp;
    public float CurrentHp => _playerHp;

    [Header("Player��MaxHp")]
    [SerializeField] float _playerMaxHp;
    [Tooltip("�v���C���[��Hp")]
    float _playerHp;

    public Action<float> OnHealth;
    /// <summary>HP���O�ɂȂ������ʒm����Action</summary>
    public Action OnDead;

    /// <summary>
    /// �_���[�W���󂯂�Ƃ��ɌĂ΂��֐�
    /// </summary>
    /// <param name="amount">�_���[�W��</param>
    public void ApplyDamage(float amount)
    {
        _playerHp -= amount;
        OnHealth(_playerHp);
        //�̗͂��O�ȉ��ɂȂ������ʒm����
        if (_playerHp <= 0)
        {
            OnDead?.Invoke();
        }
    }

    /// <summary>
    /// �񕜂���Ƃ��ɌĂԊ֐�
    /// </summary>
    /// <param name="amount">�񕜂����</param>
    public void ApplyHeal(float amount)
    {
        _playerHp += amount;
    }
}
