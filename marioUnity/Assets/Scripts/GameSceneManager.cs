using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    /*キャラを移動させたり、衝突のチェックをしたり、重力を加えたりするもの
     * キャラクター操作：http://game-sakusei.com/unity/616.html
     * https://akiblog10.com/unity-moving/
     * ボタン操作：https://qiita.com/netty/items/66284cbb2bb1cd42a486
     * 移動を作ろう：https://dkrevel.com/makegame-beginner/make-2d-aciton-move/
     * 衝突判定・ジャンプ：http://game-sakusei.com/unity/628.html
     * 接地判定：https://dkrevel.com/makegame-beginner/make-2d-action-ground/
     */

    [SerializeField] GameObject ButtonRight;
    [SerializeField] GameObject ButtonLeft;
    [SerializeField] GameObject ButtonJamp;
    [SerializeField] GameObject Fish;
    [SerializeField] TextMeshProUGUI FishCount;

    Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    //ここに入力する値で調節
    public float Speed;
    public float Gravity;
    public float JumpingPower;
    public float JumpPos = 0.0f;
    public float JumpHeight;
    public float JumpTime;
    public float JumpLimitTime;

    public int fishcount;

    public bool right = false;
    public bool left = false;
    public bool jump = false;
    public bool field = false;
    public bool isjump = false;

    public AnimationCurve jumpCurve;
    
    //キーボード操作や衝突など毎回チェックがいるものはUpdate
    // Update is called once per frame
    void Update()
    {
        float xSpeed = 0.0f;
        float ySpeed = -Gravity;
        if (right)
        {
            xSpeed = Speed;
        }
        else if (left)
        {
            xSpeed = -Speed;
        }
        else if(jump)
        {
            if(field)
            {
                ySpeed = JumpingPower;
                JumpPos = transform.position.y;
                JumpTime = 0.0f;
            }
            if (isjump)
            {
                ySpeed *= jumpCurve.Evaluate(JumpTime);
                bool canTime = JumpLimitTime > JumpTime;
                if (JumpPos + JumpHeight > transform.position.y&&canTime)
                {
                    ySpeed = JumpingPower;
                    JumpTime += Time.deltaTime;
                }
                else
                {
                    isjump = false;
                    JumpTime = 0.0f;
                }
                
            }
        }
        else
        {
            xSpeed = 0.0f;
        }
        RB.velocity = new Vector2(xSpeed, ySpeed);
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
        jump = true;
        isjump = true;
    }
    public void JPushUp()
    {
        jump = false;
        isjump = false;
        JumpTime = 0.0f;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Enemy":
                SceneManager.LoadScene("GameOver");
                break;
            case "Field":
                field = true;
                break;
            case "Fish":
                Fish.SetActive(false);
                fishcount++;
                FishCount.text = "×" + fishcount.ToString();
                break;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Field")
        {
            field = false;
        }
    }

}
