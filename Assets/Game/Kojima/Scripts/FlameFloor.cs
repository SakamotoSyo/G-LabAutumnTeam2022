using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFloor : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]private GameObject _flameFloor;
    [SerializeField]private GameObject _floor;
    [Header("�R����̏��̃_���[�W��"),SerializeField] private float _flameDmg = 10;
    [Header("�R����̏��̔R�Ď���"), SerializeField] private float _flameTime = 10;
    void Start()
    {
        _player = GameObject.Find("Player");
        //���̏����ݒ�
        _floor.SetActive(false);
        _flameFloor.SetActive(true);

    }

    
    void Update()
    {
        BurningTime();
    }
    //�v���C���[��FlameFloor�Ƃ̐ڐG����
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        if(collision.gameObject.CompareTag("Player") && _flameFloor.activeSelf == true)
        {   
                //�v���C���[��HP�����炷
                playerCon.playerHp -= _flameDmg;
                //���̐؂�ւ�
                _flameFloor.SetActive(false);
                _floor.SetActive(true);
        }
    }

    private void BurningTime()
    {
        //�R�Ď���
        _flameTime -= Time.deltaTime;
        //���Ԑ����ŏ��̐؂�ւ�
        if (_flameTime < 0)
        {  
            _flameFloor.SetActive(false);
            _floor.SetActive(true);
        }
    }
}
