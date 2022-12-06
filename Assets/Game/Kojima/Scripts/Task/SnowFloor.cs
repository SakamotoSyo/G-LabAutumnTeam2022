using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    #region ��`
    GameObject _player1;
    GameObject _player2;
    Vector2 _player1_curPos;//���݂̍��W
    Vector2 _player2_curPos;
    Vector2 _player1_prevPos;//�ߋ��̍��W
    Vector2 _player2_prevPos;
    Vector2 _player1_dis;//���݂Ɖߋ��̍��W�̃x�N�g���̒����̍�7
    Vector2 _player2_dis;
    int hpBase = 5;

    #endregion

    void Start()
    {
        _player1 = GameObject.Find("Player1");
        _player2 = GameObject.Find("Player2");
    }

    
    void Update()
    {
       
    }


    # region �v���C���[�̈ړ������̌v�Z���s��
    //�v���C���[��HP�񕜂��s��
    private void FixedUpdate()
    {
        /*----�v���C���[�̈ړ����Ǘ����Ă���N���X����ړ�������̍��W���擾----*/
        _player1_curPos = new Vector2(_player1.transform.position.x,0);
        _player2_curPos = new Vector2(_player2.transform.position.x,0);
        //�ړ������̌v�Z
        _player1_dis = _player1_prevPos + _player1_curPos;
        _player2_dis = _player2_prevPos + _player2_curPos;
        //�ړ������ɉ�����HP����
        playerCon.playerHp += hpBase * _player1_dis.magnitude;
        playerCon.playerHp += hpBase * _player2_dis.magnitude;

        _player1_prevPos = _player1_curPos;
        _player2_prevPos= _player2_curPos;

    }
    #endregion


    #region �����蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //tag�Ńv���C���[��SnowFloor�̐ڐG�𔻒�
        if (collision.gameObject.tag == "Player1")
        {
            //�v���C���[��SnowFloor�ɓ������u�Ԃ̍��W���擾
            /*----�v���C���[�̈ړ����Ǘ����Ă���N���X����擾----*/
            _player1_prevPos = new Vector2(_player1.transform.position.x, 0);
        }
        if(collision.gameObject.tag == "Player2")
        {
            _player2_prevPos = new Vector2(_player2.transform.position.x, 0);
        }
    }
    #endregion
}
