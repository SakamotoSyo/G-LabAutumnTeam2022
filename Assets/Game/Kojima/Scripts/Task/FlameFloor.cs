using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFloor : MonoBehaviour
{
    #region 定義
    private GameObject _player;
    [SerializeField]private GameObject _flameFloor;
    [SerializeField]private GameObject _floor;
    [Header("燃えるの床のダメージ量"),SerializeField] private float _flameDmg = 10;
    [Header("燃えるの床の燃焼時間"), SerializeField] private float _flameTime = 10;
    #endregion
    void Start()
    {
        _player = GameObject.Find("Player");
        //床の初期設定
        AudioManager.Instance.PlaySound(SoundPlayType.SE_fire);
        _floor.SetActive(false);
        _flameFloor.SetActive(true);

    }

    
    void Update()
    {
        BurningTime();
    }
    #region プレイヤーとFlameFloorとの接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        if(collision.gameObject.CompareTag("Player") && _flameFloor.activeSelf == true)
        {   
            //プレイヤーのHPを減らす
            playerCon.playerHp -= _flameDmg;
            //床の切り替え
            AudioManager.Instance.PlaySound(SoundPlayType.SE_fire_finish);
            _flameFloor.SetActive(false);
            _floor.SetActive(true);
        }
    }
    #endregion
    #region 燃え尽きるまで
    private void BurningTime()
    {
        //燃焼時間
        _flameTime -= Time.deltaTime;
        //時間制限で床の切り替え
        if (_flameTime < 0)
        {  
            AudioManager.Instance.PlaySound(SoundPlayType.SE_fire_finish);
            _flameFloor.SetActive(false);
            _floor.SetActive(true);
        }
    }
    #endregion
}
