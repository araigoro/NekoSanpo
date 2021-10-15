using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : MonoBehaviour
{
    public SpriteRenderer sr = null;
    public Rigidbody2D rb = null;

    public float speed;
    public float gravity;
    public float movetime=0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //カメラの範囲内に入ってきた場合
        if(sr.isVisible)
        {
            movetime += Time.deltaTime;
            int xVector = -1;
            
            //時間経過で動きを変える
            if(movetime<=Random.Range(2.5f,3.5f))
            {
                xVector = 1;
            }
            else if(movetime>3.5&&movetime<=Random.Range(6.0f,7.0f))
            {
                xVector = -1;
            }
            else if(movetime>7)
            {
                movetime = 0.0f;
            }
           
            //動き実行
            rb.velocity = new Vector2(xVector * speed, -gravity);          
        }
        else
        {
            //カメラ外の時は止めておく
            rb.Sleep();
        }
    }
}
