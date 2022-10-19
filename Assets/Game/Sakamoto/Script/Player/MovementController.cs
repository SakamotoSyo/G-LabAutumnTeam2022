using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Rigidbody�R���|�[�l���g")]
    [SerializeField]Rigidbody2D _rb;
    [Header("Player�̃X�s�[�h")]
    [SerializeField] float _speed;

    PlayerInput _playerInput;

    void Start()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();   
    }

    void Update()
    {
        
    }

  �@ void FixedUpdate()
    {
        _rb.velocity = _playerInput.MoveInput.normalized * new Vector2(_speed, _speed);
    }
}