using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    #region ��`
    GameObject _player;
    Vector2 _curPos;//���݂̍��W
    Vector2 _prevPos;//�ߋ��̍��W
    Vector2 _dis;//���݂Ɖߋ��̍��W�̃x�N�g���̒����̍�
    int hpBase = 5;

    #endregion

    void Start()
    {
        _player = GameObject.Find("Player");
    }

    
    void Update()
    {
       
    }


    # region �v���C���[�̈ړ������̌v�Z���s��
    //�v���C���[��HP�񕜂��s��
    private void FixedUpdate()
    {
        /*----�v���C���[�̈ړ����Ǘ����Ă���N���X����ړ�������̍��W���擾----*/
         _curPos = new Vector2(_player.transform.position.x,0);
        //�ړ������̌v�Z
        _dis = _prevPos + _curPos;
        //�ړ������ɉ�����HP����
        playerCon.playerHp += hpBase * _dis.magnitude;
       
        _prevPos = _curPos;

    }
    #endregion


    #region �����蔻��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //tag�Ńv���C���[��SnowFloor�̐ڐG�𔻒�
        if(collision.gameObject.tag == "Player")
        {
            //�v���C���[��SnowFloor�ɓ������u�Ԃ̍��W���擾
            /*----�v���C���[�̈ړ����Ǘ����Ă���N���X����擾----*/
             _prevPos = new Vector2(_player.transform.position.x, 0);
        }
        
    }
    #endregion
}
