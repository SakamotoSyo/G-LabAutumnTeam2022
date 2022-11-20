using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("PlayerのAnimation")]
    [SerializeField] Animator _anim;
    [Header("PlayerInput")]
    [SerializeField] PlayerInput _playerInput;
    [Header("PlayerのHpがMaxの時のAnimation")]
    [SerializeField] RuntimeAnimatorController _maxController;
    [Header("PlayerのHpが2/3の時のAnimation")]
    [SerializeField] RuntimeAnimatorController _23Controller;
    [Header("PlayerのHpが1/3の時のAnimation")]
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
    /// PlayerのHpが変わった時に通知される
    /// </summary>
    /// <param name="amount"></param>
    void OnHealthChanged(float amount)
    {
        //PlayerのHpが3/2の時
        if (_playerHp.MaxHp / 3 * 2 > _playerHp.CurrentHp)
        {
            _anim.runtimeAnimatorController = _23Controller;
        }
        //PlayerのHpが3/1の時
        else if (_playerHp.MaxHp / 3 * 1 > _playerHp.CurrentHp) 
        {
            _anim.runtimeAnimatorController = _13Controller;
        }
    }


}
