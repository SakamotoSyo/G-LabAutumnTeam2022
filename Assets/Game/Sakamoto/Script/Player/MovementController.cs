using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Rigidbodyコンポーネント")]
    [SerializeField]Rigidbody2D _rb;
    [Header("Playerのスピード")]
    [SerializeField] float _speed;

    PlayerInput _playerInput;

    void Start()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();   
    }

    void Update()
    {
        
    }

  　 void FixedUpdate()
    {
        _rb.velocity = _playerInput.MoveInput.normalized *_speed;
    }
}
