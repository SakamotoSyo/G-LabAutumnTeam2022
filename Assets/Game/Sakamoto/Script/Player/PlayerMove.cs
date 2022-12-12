using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Header("Rigidbody�R���|�[�l���g")]
    [SerializeField] Rigidbody2D _rb;
    [Header("Player�̃X�s�[�h")]
    [SerializeField] float _speed;
    [Header("PlayerInput")]
    [SerializeField] PlayerInput _playerInput;

    public void FixedUpdate()
    {
        _rb.velocity = _playerInput.MoveInput.normalized * _speed;
    }
}
