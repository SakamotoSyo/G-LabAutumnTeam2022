using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [Header("自分自身のプレイヤー番号")]
    [SerializeField] int _playerNum;
    [Header("アクションした後に動けなくなる時間")]
    [SerializeField] float _actionWait = 0f;

    [Tooltip("動く方向")]
    Vector2 _movement;
    [Tooltip("現在Action中か")]
    bool _isAction = false;
    [Tooltip("Inputをブロックするかどうか")]
    bool _inputBlock = false;

    WaitForSeconds _actionWaitForSeconds;
    Coroutine _actionCoroutine;

    /// <summary>現在アクション中かどうか返す</summary>
    public bool Action
    {
        get { return _isAction && !_inputBlock; }
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
    void Start()
    {
        GameManager.GameStart += InputBlock;
    }

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw($"Horizontal{_playerNum}"), Input.GetAxisRaw($"Vertical{_playerNum}"));

        if (Input.GetButtonDown($"Action{_playerNum}")) 
        {
            if (_actionCoroutine != null) 
            {
                StopCoroutine(_actionCoroutine);
            }

            _actionCoroutine = StartCoroutine(ActionWait());
        }
    }

    IEnumerator ActionWait() 
    {
        _isAction = true;
        //アニメーションにかかる時間を
        yield return _actionWaitForSeconds;

        _isAction = false;
    }

    /// <summary>Inputに関する入力を受け付けるかどうか変更する</summary>
    public void InputBlock() 
    {
        _inputBlock = !_inputBlock;
    }
}
