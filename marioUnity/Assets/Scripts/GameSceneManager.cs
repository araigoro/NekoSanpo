using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject Player;

    Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
    }

    //ここに入力する値で調節
    public float Speed;
    public float JampingPower;

    public bool right = false;
    public bool left = false;
    public bool jamp = false;
    public bool field = false;

    //キーボード操作や衝突など毎回チェックがいるものはUpdate
    // Update is called once per frame
    void Update()
    {
        RB = Player.GetComponent<Rigidbody2D>();
        if (right)
        {
            //Vector2(x,y)orVector3(x,y,z)の順で入れる
            RB.velocity = new Vector2(Speed, RB.velocity.y);
        }
        else if (left)
        {
            RB.velocity = new Vector2(-Speed, RB.velocity.y);
        }
        else if(jamp&&field)
        {
            RB.velocity = new Vector2(RB.velocity.x,JampingPower);
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
    public void FieldCheck()
    {
        field = true;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Gameover");
    }
}
