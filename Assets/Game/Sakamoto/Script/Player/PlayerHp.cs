using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
public class PlayerHp : MonoBehaviour, IDamage
{
    public float MaxHp => _playerMaxHp;
    public float CurrentHp => _playerHp;

    [Header("Player��MaxHp")]
    [SerializeField] float _playerMaxHp;
    [Header("�P�b���Ƃɉ��_���[�W���󂯂邩")]
    [SerializeField] float _damage;
    [Tooltip("�v���C���[��Hp���Ȃ��Ȃ������ǂ���")]
    bool _isDead = false;
    [Tooltip("�v���C���[��Hp")]
    float _playerHp;

    public Action<float> OnHealth;
    /// <summary>HP���O�ɂȂ������ʒm����Action</summary>
    public Action OnDead;

    private async void Start()
    {
        _playerHp = _playerMaxHp;
        await RegularlyDamage();
    }

    /// <summary>
    /// �_���[�W���󂯂�Ƃ��ɌĂ΂��֐�
    /// </summary>
    /// <param name="amount">�_���[�W��</param>
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

        //�̗͂��O�ȉ��ɂȂ������ʒm����
        if (_playerHp <= 0 && !_isDead)
        {
            OnDead?.Invoke();
            _isDead = true;
        }
    }

    /// <summary>
    /// �񕜂���Ƃ��ɌĂԊ֐�
    /// </summary>
    /// <param name="amount">�񕜂����</param>
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
