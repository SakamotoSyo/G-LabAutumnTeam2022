using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���X�g����Item���R���x�A�ɉ����Ĉړ�������N���X
public class ItemMoveCon : MonoBehaviour
{
   
    #region ��`
    [Header("�ړ����xA"), SerializeField] private float _moveSpeedA = 0.01f;
    [Header("�ړ����xB"), SerializeField] private float _moveSpeedB = 0.01f;
    [Header("�ړ������FX"), SerializeField] private float _posX = 0.0f;
    [Header("�ړ������FY"), SerializeField] private float _posY = 1.0f;

    private float _sum;

    static public bool _ItemMoveConFlag = true;//FreezeMgr�Œ�~������悤��Flag
    Rigidbody2D _rb2d;

    #endregion
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (_ItemMoveConFlag == true)
        {
            ItemMove();
        }
        else if (_ItemMoveConFlag == false)
        {
            StopItemMove();
        } 
    }
    #region �A�C�e���𓮂���
    private void ItemMove()
    {
        _rb2d.velocity = new Vector2(_posX, -_posY);//velocity�͂����܂ŁA���x�������Ă��邾��
        _sum = _moveSpeedA * _moveSpeedB;
        _posY -= _sum;
    }
    #endregion
    #region �A�C�e�����~�߂�
    private void StopItemMove()
    {
        _rb2d.velocity = Vector2.zero;
    }
    #endregion
}
