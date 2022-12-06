using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//リスト内のItemをコンベアに沿って移動させるクラス
public class ItemMoveCon : MonoBehaviour
{
   
    #region 定義
    [Header("移動速度A"), SerializeField] private float _moveSpeedA = 0.01f;
    [Header("移動速度B"), SerializeField] private float _moveSpeedB = 0.01f;
    [Header("移動方向：X"), SerializeField] private float _posX = 0.0f;
    [Header("移動方向：Y"), SerializeField] private float _posY = 1.0f;

    private float _sum;

    static public bool _ItemMoveConFlag = true;//FreezeMgrで停止させるようのFlag
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
    #region アイテムを動かす
    private void ItemMove()
    {
        _rb2d.velocity = new Vector2(_posX, -_posY);//velocityはあくまで、速度を加えているだけ
        _sum = _moveSpeedA * _moveSpeedB;
        _posY -= _sum;
    }
    #endregion
    #region アイテムを止める
    private void StopItemMove()
    {
        _rb2d.velocity = Vector2.zero;
    }
    #endregion
}
