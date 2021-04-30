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
    [SerializeField] TextMeshProUGUI FishCount;
    [SerializeField] GameObject ClearText;
    [SerializeField] GameObject FirstText;
    

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
        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = Speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xSpeed = -Speed;
        }
        else
        {
            xSpeed = 0.0f;
        }
        if (jump)
        {
            if(field)
            {
                ySpeed = JumpingPower;
                JumpPos = transform.position.y;
                JumpTime = 0.0f;
            }
            if (isjump)
            {
                field = false;
                ySpeed *= jumpCurve.Evaluate(JumpTime);
                bool canTime = JumpLimitTime > JumpTime;
                if (JumpPos + JumpHeight > transform.position.y&&canTime)
                {
                    ySpeed = JumpingPower;
                    JumpTime += Time.deltaTime;
                }                    
            }
            else
            {
                isjump = false;
            }
        }
        RB.velocity = new Vector2(xSpeed, ySpeed);
        if(transform.position.x>0)
        {
            FirstText.SetActive(false);
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
        jump = true;
        isjump = true;
    }
    public void JPushUp()
    {
        jump = false;
        isjump = false;
    }
    public void ClearButton()
    {
        SceneManager.LoadScene("Stage2");
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
            case "Clear":
                if(fishcount==10)
                {
                    ClearText.SetActive(true);
                }
                break;
            case "Gameover":
                SceneManager.LoadScene("Gameover");
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Fish")
        {
            fishcount++;
            FishCount.text = "×" + fishcount.ToString();           
        }
    }
}
