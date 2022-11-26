using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Player‚ÌAnimation")]
    [SerializeField] Animator _anim;
    [Header("PlayerInput")]
    [SerializeField] PlayerInput _playerInput;
    [Header("Player‚ÌHp‚ªMax‚ÌŽž‚ÌAnimation")]
    [SerializeField] RuntimeAnimatorController _maxController;
    [Header("Player‚ÌHp‚ª2/3‚ÌŽž‚ÌAnimation")]
    [SerializeField] RuntimeAnimatorController _23Controller;
    [Header("Player‚ÌHp‚ª1/3‚ÌŽž‚ÌAnimation")]
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
    /// Player‚ÌHp‚ª•Ï‚í‚Á‚½Žž‚É’Ê’m‚³‚ê‚é
    /// </summary>
    /// <param name="amount"></param>
    void OnHealthChanged(float amount)
    {
        //Player‚ÌHp‚ª3/2‚ÌŽž
        if (_playerHp.MaxHp / 3 * 2 > _playerHp.CurrentHp)
        {
            _anim.runtimeAnimatorController = _23Controller;
        }
        //Player‚ÌHp‚ª3/1‚ÌŽž
        else if (_playerHp.MaxHp / 3 * 1 > _playerHp.CurrentHp)
        {
            _anim.runtimeAnimatorController = _13Controller;
        }
    }


}
