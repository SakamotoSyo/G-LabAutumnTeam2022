using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Player��Animation")]
    [SerializeField] Animator _anim;
    [Header("PlayerInput")]
    [SerializeField] PlayerInput _playerInput;
    [Header("Player��Hp��Max�̎���Animation")]
    [SerializeField] RuntimeAnimatorController _maxController;
    [Header("Player��Hp��2/3�̎���Animation")]
    [SerializeField] RuntimeAnimatorController _23Controller;
    [Header("Player��Hp��1/3�̎���Animation")]
    [SerializeField] RuntimeAnimatorController _13Controller;
    [Header("PlayerHp")]
    [SerializeField] PlayerHp _playerHp;
    void Start()
    {
        _playerHp.OnHealth += OnHealthChanged;
    }

    private void Update()
    {
        _anim.SetFloat("X", _playerInput.PlayerDir.x);
        _anim.SetFloat("Y", _playerInput.PlayerDir.y);
        // _anim.SetBool("Walk", _playerInput.PlayerDir.x != 0 || _playerInput.PlayerDir.y != 0 ? true : false);
        //_anim.SetFloat("Walk", _playerInput.x)
        if (Input.GetKeyDown(KeyCode.K))
        {
            _anim.runtimeAnimatorController = _23Controller;
        }
    }

    /// <summary>
    /// Player��Hp���ς�������ɒʒm�����
    /// </summary>
    /// <param name="amount"></param>
    void OnHealthChanged(float amount)
    {
        //Player��Hp��3/2�̎�
        if (_playerHp.MaxHp / 3 * 2 > _playerHp.CurrentHp)
        {
            _anim.runtimeAnimatorController = _23Controller;
        }
        //Player��Hp��3/1�̎�
        else if (_playerHp.MaxHp / 3 * 1 > _playerHp.CurrentHp)
        {
            _anim.runtimeAnimatorController = _13Controller;
        }
    }


}
