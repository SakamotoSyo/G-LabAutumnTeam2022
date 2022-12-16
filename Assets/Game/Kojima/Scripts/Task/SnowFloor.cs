using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    #region ��`
    [SerializeField] PlayerHp _player1;
    [SerializeField] PlayerHp _player2;
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
     
    }

    
    void Update()
    {
       
    }


    # region �v���C���[�̈ړ������̌v�Z���s��
    //�v���C���[��HP�񕜂��s��
    private void FixedUpdate()
    {
      
        /*----�v���C���[�̈ړ����Ǘ����Ă���N���X����ړ�������̍��W���擾----*/
       

    }
    #endregion


    #region �����蔻��
    private void OnTriggerStay2D(Collider2D collision)
    {
        //tag�Ńv���C���[��SnowFloor�̐ڐG�𔻒�
        if (collision.gameObject.tag == "Player1")
        {
            //�v���C���[��SnowFloor�ɓ������u�Ԃ̍��W���擾
            /*----�v���C���[�̈ړ����Ǘ����Ă���N���X����擾----*/
            _player1_curPos = new Vector2(_player1.transform.position.x, 0);
            //�ړ������̌v�Z
            _player1_dis = _player1_prevPos - _player1_curPos;
            //�ړ������ɉ�����HP����
            _player1.ApplyHeal(hpBase * _player1_dis.magnitude);
           

            _player1_prevPos = _player1_curPos;
           
        }
        if(collision.gameObject.tag == "Player2")
        {
            _player2_curPos = new Vector2(_player2.transform.position.x, 0);
            _player2_dis = _player2_prevPos + _player2_curPos;
            _player2.ApplyHeal(hpBase * _player2_dis.magnitude);
            _player2_prevPos = _player2_curPos;
        }
    }
    #endregion
}
