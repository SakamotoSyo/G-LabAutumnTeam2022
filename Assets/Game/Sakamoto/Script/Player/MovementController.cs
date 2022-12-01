using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("PlayerAction")]
    [SerializeField] ActionController _action;
    [Header("PlayerAnimation")]
    [SerializeField] PlayerAnimation _playerAnimation;
    [Header("PlayerMove")]
    [SerializeField] PlayerMove _playerMove;

    PlayerInput _playerInput;

    void Start()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerAnimation.Init();
    }

    void Update()
    {
        if (_playerInput.Action) 
        {
            _action.Interact(gameObject.transform);
        }
        _playerAnimation.Update();

    }

  Å@ void FixedUpdate()
    {
       _playerMove.FixedUpdate();
    }
}
