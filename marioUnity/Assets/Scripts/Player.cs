using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*キャラを移動させたり、衝突のチェックをしたり、重力を加えたりするもの
     * キャラクター操作：http://game-sakusei.com/unity/616.html
     * https://akiblog10.com/unity-moving/
     * ボタン操作：https://qiita.com/netty/items/66284cbb2bb1cd42a486
     * 移動を作ろう：https://dkrevel.com/makegame-beginner/make-2d-aciton-move/
     * 衝突判定・ジャンプ：http://game-sakusei.com/unity/628.html
     * 接地判定：https://dkrevel.com/makegame-beginner/make-2d-action-ground/
     */

    public Rigidbody2D RB;
   

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    //ここに入力する値で調節
    public float Speed;
    public float JampingPower;

    [SerializeField] GameObject BUttonRight;
    [SerializeField] GameObject BUttonLeft;
    [SerializeField] GameObject BUttonJamp;

    bool right = false;
    bool left = false;
    bool jamp = false;

    //キーボード操作や衝突など毎回チェックがいるものはUpdate
    // Update is called once per frame
    void Update()
    {
        var Hori = Input.GetAxis("Horizontal");

        if (right)
        {
            RB.velocity = new Vector2(Speed, RB.velocity.y);
        }
        else if (left)
        {
            RB.velocity = new Vector2(-Speed, RB.velocity.y);
        }
        else if(jamp)
        {
            RB.AddForce(Vector2.up * JampingPower);
        }
        else
        {
            RB.velocity = Vector2.zero;
        }
    }

    public void RPushDown()
    {
        right = true;
    }
    public void RPushUp()
    {
        right = false;
    }
    public void LPushDown()
    {
        left = true;
    }
    public void LPushUp()
    {
        left = false;
    }
    public void JPushDown()
    {
        jamp = true;
    }
    public void JPushUp()
    {
        jamp = false;
    }
}
