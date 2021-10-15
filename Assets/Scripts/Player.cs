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
        //コンポーネント取得
        RB = GetComponent<Rigidbody2D>();
        gmanager = Manager.GetComponent<GManager>();
        anim = GetComponent<Animator>();

        //ステージ判別
        stageNo = PlayerPrefs.GetInt("Stage", 1);

        //ステージ２だった場合取得
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
        //GManagerスクリプトの変数を代入
        right = gmanager.right;
        left = gmanager.left;
        jump = gmanager.jump;
        isjump = gmanager.isjump;

        float xSpeed = 0.0f;
        float ySpeed = -Gravity;

        //右移動
        if (right||Input.GetKey(KeyCode.D))
        {
            xSpeed = Speed;

            //アニメーション設定
            anim.SetBool("right", true);
            anim.SetBool("left", false);

            //バラを取得していた場合
            if (gmanager.rose == true)
            {
                RoseR.SetActive(true);
                RoseL.SetActive(false);
            }
        }
        //左移動
        else if (left||Input.GetKey(KeyCode.A))
        {
            xSpeed = -Speed;

            //アニメーション設定
            anim.SetBool("right", false);
            anim.SetBool("left", true);

            //バラを取得していた場合
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
        //ジャンプ処理
        if (jump)
        {
            //地面に接地していた場合
            if(field)
            {
                ySpeed = JumpingPower;

                //上方向に力を加える
                JumpPos = transform.position.y;
                JumpTime = 0.0f;
            }
            //ジャンプしている間
            if (isjump)
            {
                field = false;

                //時間経過でジャンプ力を操作
                ySpeed *= jumpCurve.Evaluate(JumpTime);
                bool canTime = JumpLimitTime > JumpTime;

                //ジャンプの高さに上限設定
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
        //移動実行
        RB.velocity = new Vector2(xSpeed, ySpeed);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        //衝突したオブジェクトのタグで処理分岐
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
                    //指定のメソッドを実行
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
                //プレイヤーを車の子オブジェクトにする
                transform.SetParent(coll.transform);
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Fish":
                //取得した魚の数をプラス
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
                //子オブジェクトを解除する
                transform.SetParent(null);
                break;
        }
    }
}
