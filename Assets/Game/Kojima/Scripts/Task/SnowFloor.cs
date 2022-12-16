using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    #region 定義
    [SerializeField] PlayerHp _player1;
    [SerializeField] PlayerHp _player2;
    Vector2 _player1_curPos;//現在の座標
    Vector2 _player2_curPos;
    Vector2 _player1_prevPos;//過去の座標
    Vector2 _player2_prevPos;
    Vector2 _player1_dis;//現在と過去の座標のベクトルの長さの差7
    Vector2 _player2_dis;
    int hpBase = 5;

    #endregion

    void Start()
    {
     
    }

    
    void Update()
    {
       
    }


    # region プレイヤーの移動距離の計算を行う
    //プレイヤーのHP回復を行う
    private void FixedUpdate()
    {
      
        /*----プレイヤーの移動を管理しているクラスから移動した後の座標を取得----*/
       

    }
    #endregion


    #region 当たり判定
    private void OnTriggerStay2D(Collider2D collision)
    {
        //tagでプレイヤーとSnowFloorの接触を判定
        if (collision.gameObject.tag == "Player1")
        {
            //プレイヤーがSnowFloorに入った瞬間の座標を取得
            /*----プレイヤーの移動を管理しているクラスから取得----*/
            _player1_curPos = new Vector2(_player1.transform.position.x, 0);
            //移動距離の計算
            _player1_dis = _player1_prevPos - _player1_curPos;
            //移動距離に応じてHPを回復
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
