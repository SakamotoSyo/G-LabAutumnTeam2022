using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMgr : MonoBehaviour
{
    #region ��`
    [Header("�h�A�J������"),SerializeField]float _doorRereaseTimer = 5;
    [Header("�Ďn������"), SerializeField] float _startTimer;
    [SerializeField] DoorHit [] _doorHits;
    #endregion
    void Start()
    {
        _doorHits[0].DoorChange += DoorCheck;
        _doorHits[1].DoorChange += DoorCheck;
        
    }
    void Update()
    {
        //Debug.Log($"Door1 = {_doorHits[0].IsDoorClose()}");
        //Debug.Log($"Door2 = {_doorHits[1].IsDoorClose()}");
          DoorCheck();
    }
    #region �Q�����̃h�A�̐^�U�̃`�F�b�N�Ƃ��̌�̏���
    private void DoorCheck()
    {
        
        //�ǂ��炩�̃h�A��false�Ȃ�J�E���g�_�E�����J�n
        if (_doorHits[0].IsDoorClose() == false || _doorHits[1].IsDoorClose() == false)
        {
           // Debug.Log("aaaaaaaaa");
            _doorRereaseTimer -= Time.deltaTime;
            _startTimer = 5;
        }

        //�h�A��true�Ȃ�^�C�}�[�����Z�b�g
        if (_doorHits[0].IsDoorClose() == true && _doorHits[1].IsDoorClose() == true)
        {
           // Debug.Log("reset");
            _doorRereaseTimer = 5;
        }
        StopGen();
    }
    #endregion
    
    #region ��莞�Ԍo��ItemGenerator��ItemMoveCon��Stop�֐����Ă�
  private void StopGen()
    {
        if(_doorRereaseTimer < 0)
        {
            ItemGenerator._stopFrag = false;
            ItemMoveCon._ItemMoveConFlag = false;
           
        }
        DoorReCheck();
    }
    #endregion

    #region �h�A��߂����̏���
    private void DoorReCheck()
    {
        //�����̃h�A��true�Ȃ�^�C�}�[�����Z�b�g
        if (_doorHits[0].IsDoorClose() == true && _doorHits[1].IsDoorClose() == true)
        {
            _doorRereaseTimer = 5;//���Z�b�g
            _startTimer -= Time.deltaTime;//�Ďn���̃J�E���g�_�E��
            if (_startTimer < 0)
            {
                ItemGenerator._stopFrag = true;
                ItemMoveCon._ItemMoveConFlag = true;
            }
        }
    }
    #endregion
}
