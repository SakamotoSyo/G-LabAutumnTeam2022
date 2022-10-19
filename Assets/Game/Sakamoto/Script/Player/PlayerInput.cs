using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [Header("�������g�̃v���C���[�ԍ�")]
    [SerializeField] int _playerNum;
    [Tooltip("��������")]
    Vector2 _movement;
    [Tooltip("����Action����")]
    bool _isAction = false;
    [Tooltip("Input���u���b�N���邩�ǂ���")]
    bool _inputBlock = false;

    WaitForSeconds _actionWaitForSeconds;
    Coroutine _actionCoroutine;

    readonly float _actionWait = 0.03f;

    /// <summary>���݃A�N�V���������ǂ����Ԃ�</summary>
    public bool Action
    {
        get { return _isAction && !_inputBlock; }
    }
    /// <summary>���݂̕������͂�Ԃ�</summary>
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

        yield return _actionWaitForSeconds;

        _isAction = false;
    }

    /// <summary>Input�Ɋւ�����͂��󂯕t���邩�ǂ����ύX����</summary>
    public void InputBlock() 
    {
        _inputBlock = !_inputBlock;
    }
}