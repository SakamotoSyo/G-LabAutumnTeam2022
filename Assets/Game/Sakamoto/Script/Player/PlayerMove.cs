using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Header("Rigidbodyコンポーネント")]
    [SerializeField] Rigidbody2D _rb;
    [Header("Playerのスピード")]
    [SerializeField] float _speed;
    [Header("PlayerInput")]
    [SerializeField] PlayerInput _playerInput;

    public void FixedUpdate()
    {
        _rb.velocity = _playerInput.MoveInput.normalized * _speed;
    }
}
