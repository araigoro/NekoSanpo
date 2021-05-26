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
    Car car;
    GameObject RoseR;
    GameObject RoseL;

    public bool right;
    public bool left;
    public bool jump;
    public bool isjump;
    public bool field = false;
    public int stageNo;

    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        gmanager = Manager.GetComponent<GManager>();
        anim = GetComponent<Animator>();
        stageNo = PlayerPrefs.GetInt("Stage", 1);
        if(stageNo==2)
        {
            RoseR = transform.Find("RoseRight").gameObject;
            RoseL = transform.Find("RoseLeft").gameObject;
        }    
    }

    //ここに入力する値で調節
    public float Speed;
    public float Gravity;
    public float JumpingPower;
    public float JumpPos = 0.0f;
    public float JumpHeight;
    public float JumpTime;
    public float JumpLimitTime;
    public float finishTime;

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
        if (right)
        {
            xSpeed = Speed;
            anim.SetBool("right", true);
            if (gmanager.rose == true)
            {
                RoseR.SetActive(true);
                RoseL.SetActive(false);
            }
        }
        else if (left)
        {
            xSpeed = -Speed;
            anim.SetBool("left", true);
            if (gmanager.rose == true)
            {
                RoseL.SetActive(true);
                RoseR.SetActive(false);
            }
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
        switch (coll.gameObject.tag)
        {
            case "Enemy":
                gmanager.GameOver();
                break;
            case "Field":
                field = true;
                break;
            case "Clear":
                if (stageNo == 1)
                {
                    gmanager.Cleartext();
                    gmanager.ClearRecord();
                }
                else if (stageNo == 2)
                {
                    gmanager.ClearHeart();
                }
                break;
            case "Gameover":
                gmanager.GameOver();
                break;
            case "Car":
                field = true;
                transform.SetParent(coll.transform);
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Fish":
                gmanager.fishcount++;
                gmanager.FishSE();
                break;
            case "Rose":
                gmanager.Rose();
                break;
            case "Enemy":
                gmanager.GameOver();
                break;
        }
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Rose":
                gmanager.RoseExit();
                break;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Car":
                transform.SetParent(null);
                break;
        }
    }
}
