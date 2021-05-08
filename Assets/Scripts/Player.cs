using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    
    Rigidbody2D RB;
    [SerializeField] GameObject Manager;
    GManager gmanager;

    public bool right;
    public bool left;
    public bool jump;
    public bool isjump;
    public bool field = false;

    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        gmanager = Manager.GetComponent<GManager>();
        anim = GetComponent<Animator>();
    }

    //ここに入力する値で調節
    public float Speed;
    public float Gravity;
    public float JumpingPower;
    public float JumpPos = 0.0f;
    public float JumpHeight;
    public float JumpTime;
    public float JumpLimitTime;
  
    public AnimationCurve jumpCurve;
    
    //キーボード操作や衝突など毎回チェックがいるものはUpdate
    // Update is called once per frame
    void Update()
    {
        right = gmanager.right;
        left = gmanager.left;
        jump = gmanager.jump;
        isjump = gmanager.isjump;

        float xSpeed = 0.0f;
        float ySpeed = -Gravity;
        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = Speed;
            anim.SetBool("right", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xSpeed = -Speed;
            anim.SetBool("left", true);
        }
        else
        {
            xSpeed = 0.0f;
            anim.SetBool("right", false);
            anim.SetBool("left", false);
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
    }
   
    public void OnCollisionEnter2D(Collision2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Enemy":
                gmanager.GameOver();
                break;
            case "Field":
                field = true;
                break;
            case "Clear":
                gmanager.Cleartext();
                gmanager.ClearRecord();
                break;
            case "Gameover":
                gmanager.GameOver();
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Fish")
        {
            gmanager.fishcount++;   
        }
    }
}
