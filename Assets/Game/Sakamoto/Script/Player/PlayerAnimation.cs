using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Header("PlayerのAnimation")]
    [SerializeField] Animator _anim;
    [Header("EffectのAnimation")]
    [SerializeField] Animator _effectAnim;
    [Header("PlayerInput")]
    [SerializeField] PlayerInput _playerInput;
    [Header("PlayerのHpがMaxの時のAnimation")]
    [SerializeField] RuntimeAnimatorController _maxController;
    [Header("PlayerのHpが2/3の時のAnimation")]
    [SerializeField] RuntimeAnimatorController _23Controller;
    [Header("PlayerのHpが1/3の時のAnimation")]
    [SerializeField] RuntimeAnimatorController _13Controller;
    [Header("PlayerのHpが0の時")]
    [SerializeField] RuntimeAnimatorController _nonController;
    [Header("PlayerHp")]
    [SerializeField] PlayerHp _playerHp;
    [Tooltip("過去のHpを保存")]
    float _hp;
    public void Init()
    {
        _playerHp.OnHealth += OnHealthChanged;
    }

    public void Update()
    {
        _anim.SetFloat("X", _playerInput.PlayerDir.x);
        _anim.SetFloat("Y", _playerInput.PlayerDir.y);
    }

    /// <summary>
    /// PlayerのHpが変わった時に通知される
    /// </summary>
    /// <param name="amount"></param>
    void OnHealthChanged(float amount)
    {
        if (_anim == null) return;
        //PlayerのHpが3/2の時
        if (_playerHp.MaxHp / 3 * 2 < amount && _anim.runtimeAnimatorController != _maxController) 
        {
            _anim.runtimeAnimatorController = _maxController;
            AudioManager.Instance.PlaySound(SoundPlayType.SE_snowman_recover);
            _effectAnim.SetTrigger("Recovery");
        }
        else if (_playerHp.MaxHp / 3 * 2 > amount && _playerHp.MaxHp / 3 * 1 < amount && _anim.runtimeAnimatorController != _23Controller)
        {
            _anim.runtimeAnimatorController = _23Controller;
            if (_playerHp.CurrentHp - _hp < 0)
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_snowman_small);
                _effectAnim.SetTrigger("Melt");
            }
            else
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_snowman_recover);
                _effectAnim.SetTrigger("Recovery");
            }

        }
        //PlayerのHpが3/1の時
        else if (0 < _playerHp.MaxHp / 3 * 1 && _playerHp.MaxHp / 3 * 1 > amount && _anim.runtimeAnimatorController != _13Controller)
        {
            _anim.runtimeAnimatorController = _13Controller;
            if (_playerHp.CurrentHp - _hp < 0)
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_snowman_small);
                _effectAnim.SetTrigger("Melt");
            }
            else
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_snowman_recover);
                _effectAnim.SetTrigger("Recovery");
            }
        }
        else if (0 >= amount && _anim.runtimeAnimatorController != _nonController) 
        {
            _anim.runtimeAnimatorController = _nonController;
            AudioManager.Instance.PlaySound(SoundPlayType.SE_snowman_small);
            _effectAnim.SetTrigger("Melt");
        }

        _hp = amount;
    }


}
