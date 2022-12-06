using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    #region 定義
    GameObject _player;
    Vector2 _curPos;//現在の座標
    Vector2 _prevPos;//過去の座標
    Vector2 _dis;//現在と過去の座標のベクトルの長さの差
    int hpBase = 5;

    #endregion

    void Start()
    {
        _player = GameObject.Find("Player");
    }

    
    void Update()
    {
       
    }


    # region プレイヤーの移動距離の計算を行う
    //プレイヤーのHP回復を行う
    private void FixedUpdate()
    {
        /*----プレイヤーの移動を管理しているクラスから移動した後の座標を取得----*/
         _curPos = new Vector2(_player.transform.position.x,0);
        //移動距離の計算
        _dis = _prevPos + _curPos;
        //移動距離に応じてHPを回復
        playerCon.playerHp += hpBase * _dis.magnitude;
       
        _prevPos = _curPos;

    }
    #endregion


    #region 当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //tagでプレイヤーとSnowFloorの接触を判定
        if (collision.gameObject.tag == "Player")
        {
            //プレイヤーがSnowFloorに入った瞬間の座標を取得
            /*----プレイヤーの移動を管理しているクラスから取得----*/
            _prevPos = new Vector2(_player.transform.position.x, 0);
        }
    }
    #endregion
}
