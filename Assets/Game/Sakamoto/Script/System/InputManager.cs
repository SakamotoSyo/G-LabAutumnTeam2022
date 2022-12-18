//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using static System.Collections.Specialized.BitVector32;
//using UnityEngine.InputSystem;

//public class InputManager : SingletonBehaviour<InputManager>
//{
//    public Vector3 MoveDir => _moveDir;

//    [Tooltip("InputSystem�Ő�������Scrpt")]
//    TestInput _inputManager;
//    [Tooltip("�ړ��������")]
//    Vector3 _moveDir;
//    [Tooltip("���͒���")]
//    Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
//    [Tooltip("���͉���")]
//    Dictionary<InputType, Action> _onExitInputDic = new Dictionary<InputType, Action>();
//    [Tooltip("���͒�")]
//    Dictionary<InputType, bool> _isStayInputDic = new Dictionary<InputType, bool>();

//    bool _isInstanced = false;

//    protected override void OnAwake()
//    {
//        Initialize();
//    }

//    /// <summary>
//    /// ����������
//    /// </summary>
//    void Initialize()
//    {
//        _inputManager = new TestInput();
//        _inputManager.Enable();
//        InirializeInput();
//        _inputManager.Player.Move.performed += context => { _moveDir = context.ReadValue<Vector3>(); };
//        _inputManager.Player.Move.canceled += context => { _moveDir = Vector3.zero; };
//        _inputManager.Player.Aim.started += context => { ExecuteInput(InputType.ADS, InputMode.Enter); };
//        _inputManager.Player.Aim.canceled += context => { ExecuteInput(InputType.ADS, InputMode.Exit); };
//        _inputManager.Player.Fire.started += context => { ExecuteInput(InputType.Fire, InputMode.Enter); };
//        _inputManager.Player.Fire.canceled += context => { ExecuteInput(InputType.Fire, InputMode.Exit); };
//        _inputManager.Player.Interact.started += context => { ExecuteInput(InputType.Interact, InputMode.Enter); };
//        _inputManager.Player.Interact.canceled += context => { ExecuteInput(InputType.Interact, InputMode.Exit); };
//        _inputManager.Player.Sprint.started += context => { ExecuteInput(InputType.Sprint, InputMode.Enter); };
//        _inputManager.Player.Sprint.canceled += context => { ExecuteInput(InputType.Sprint, InputMode.Exit); };
//        _inputManager.Player.Crouch.started += context => { ExecuteInput(InputType.Crouch, InputMode.Enter); };
//        _inputManager.Player.Crouch.canceled += context => { ExecuteInput(InputType.Crouch, InputMode.Exit); };
//        _inputManager.Player.Drop.started += context => { ExecuteInput(InputType.Drop, InputMode.Enter); };
//        _inputManager.Player.Drop.canceled += context => { ExecuteInput(InputType.Drop, InputMode.Exit); };
//        _inputManager.Player.Jump.started += context => { ExecuteInput(InputType.Jump, InputMode.Enter); };
//        _inputManager.Player.Jump.canceled += context => { ExecuteInput(InputType.Jump, InputMode.Exit); };
//        _inputManager.Player.EquipGun.started += context => { ExecuteInput(InputType.EquipWepon, InputMode.Enter); };
//        _inputManager.Player.EquipGun.canceled += context => { ExecuteInput(InputType.EquipWepon, InputMode.Exit); };
//        _inputManager.Player.EquipMelee.started += context => { ExecuteInput(InputType.EquipMelee, InputMode.Enter); };
//        _inputManager.Player.EquipMelee.canceled += context => { ExecuteInput(InputType.EquipMelee, InputMode.Exit); };
//        _inputManager.Player.EquipItem.started += context => { ExecuteInput(InputType.EquipItem, InputMode.Enter); };
//        _inputManager.Player.EquipItem.canceled += context => { ExecuteInput(InputType.EquipItem, InputMode.Exit); };

//        _isInstanced = true;
//    }

//    /// <summary>
//    /// ���͏����̏��������s��
//    /// </summary>
//    void InirializeInput()
//    {
//        if (_isInstanced)
//        {
//            for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
//            {
//                _onEnterInputDic[(InputType)i] = null;
//                _onExitInputDic[(InputType)i] = null;
//                _isStayInputDic[(InputType)i] = false;
//            }
//            return;
//        }
//        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
//        {
//            _onEnterInputDic.Add((InputType)i, null);
//            _onExitInputDic.Add((InputType)i, null);
//            _isStayInputDic.Add((InputType)i, false);
//        }
//    }

//    /// <summary>
//    /// ���͊J�n���͉��������Ƃ��ɌĂ΂��֐�
//    /// </summary>
//    /// <param name="input"></param>
//    void ExecuteInput(InputType input, InputMode type)
//    {

//        switch (type)
//        {
//            case InputMode.Enter:
//                //Debug.Log($"�����ꂽ{input}");
//                //���͊J�n���������s����
//                _onEnterInputDic[input]?.Invoke();
//                break;
//            case InputMode.Exit:
//                //Debug.Log($"�����ꂽ{input}");
//                // ���͉������������s����
//                _onExitInputDic[input]?.Invoke();
//                break;
//            default:
//                break;
//        }
//    }

//    /// <summary>
//    /// ����InputType�����͒����ǂ����t���O��Ԃ�
//    /// </summary>
//    /// <param name="type"></param>
//    /// <returns></returns>
//    public bool GetStayInput(InputType type)
//    {
//        return _isStayInputDic[type];
//    }

//    /// <summary>
//    ///����̓��͂ŌĂяo��Action��o�^����
//    /// </summary>
//    public void SetEnterInput(InputType type, Action action)
//    {
//        _onEnterInputDic[type] += action;
//    }

//    /// <summary>
//    ///����̓��͏I��������ɌĂяo��Action��o�^����
//    /// </summary>
//    public void SetExitInput(InputType type, Action action)
//    {
//        _onExitInputDic[type] += action;
//    }

//    /// <summary>
//    /// ����̓��͂ŌĂяo�����o�^����Action���폜����
//    /// </summary>
//    public void LiftEnterInput(InputType type, Action action)
//    {
//        _onEnterInputDic[type] -= action;
//    }

//    /// <summary>
//    ///����̓��͏I��������ɌĂяo�����o�^����Action���폜����
//    /// </summary>
//    public void LiftExitInput(InputType type, Action action)
//    {
//        _onExitInputDic[type] -= action;
//    }


//    /// <summary>
//    /// ���͂̃^�C�~���O
//    /// </summary>
//    public enum InputMode
//    {
//        /// <summary>���͎�</summary>
//        Enter,
//        /// <summary>���͏I����</summary>
//        Exit,
//    }

//    /// <summary>
//    /// ���͂̎��
//    /// </summary>
//    public enum InputType
//    {
//        /// <summary>�L�����Z���̏���</summary>
//        Cancel,
//        /// <summary>ADS</summary>
//        ADS,
//        /// <summary>�U������</summary>
//        Fire,
//        /// <summary>�X�L���P</summary>
//        Skill1,
//        /// <summary>�X�L���Q</summary>
//        Skill2,
//        /// <summary>�C���^���N�g</summary>
//        Interact,
//        /// <summary>���Ⴊ��</summary>
//        Crouch,
//        /// <summary>�_�b�V��</summary>
//        Sprint,
//        /// <summary>���𗎂Ƃ�</summary>
//        Drop,
//        /// <summary>�W�����v������</summary>
//        Jump,
//        /// <summary>����𑕔����� </summary>
//        EquipWepon,
//        /// <summary> </summary>
//        EquipMelee,
//        /// <summary>�A�C�e���𑕔�����</summary>
//        EquipItem,

//    }
//}
