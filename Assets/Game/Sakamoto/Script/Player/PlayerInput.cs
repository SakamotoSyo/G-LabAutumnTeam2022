using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Vector2 PlayerDir => _playerDir;
    public Vector2 LastMoveDir => _playerDir;
    [Header("自分自身のプレイヤー番号")]
    [SerializeField] int _playerNum;

    [Tooltip("動く方向")]
    Vector2 _movement;
    [Tooltip("最後に動いた方向")]
    Vector2 _playerDir;
    [Tooltip("現在Action中か")]
    bool _isAction = false;
    [Tooltip("現在ジャンプ中かどうか")]
    bool _isJump = false;
    [Tooltip("Inputをブロックするかどうか")]
    bool _inputBlock = true;

    /// <summary>現在アクション中かどうか返す</summary>
    public bool Action
    {
        get { return _isAction && !_inputBlock; }
    }

    public bool Jump 
    {
        get { return _isJump && !_inputBlock; }
    }
    /// <summary>現在の方向入力を返す</summary>
    public Vector2 MoveInput 
    {
        get 
        {
            if (_inputBlock) 
            {
                return Vector2.zero;
            }
            return _movement;
        }
    } 
    void Awake()
    {
        GameManager.GameStart += InputBlock;
    }

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw($"Horizontal{_playerNum}"), Input.GetAxisRaw($"Vertical{_playerNum}"));

        if (Vector2.zero != _movement)
        {
            _playerDir = _movement;
        }

        if (Input.GetButtonDown($"Action{_playerNum}"))
        {
            _isAction = true;
        }
        else 
        {
            _isAction = false;
        }
    }


    /// <summary>Inputに関する入力を受け付けるかどうか変更する</summary>
    public void InputBlock() 
    {
        _inputBlock = !_inputBlock;
    }
}
