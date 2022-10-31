using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCon : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    bool _keyChangeFlag = false;
    [Header("ˆÚ“®‘¬“x"), SerializeField]
    public static float _speed = 1f;
    [SerializeField]public static float playerHp = 100;
    float prevPosX;
    float nowPosX;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        Vector2 prevPos_ = new Vector2(prevPosX, 0);
        var moveVec = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            moveVec.x = _keyChangeFlag ? _speed : -_speed;
            Vector2 nowPos_ = new Vector2(nowPosX, 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveVec.x = _keyChangeFlag ? -_speed : _speed;

        }
        
        _rigidbody.AddForce(moveVec);
    }
}
