using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    public SpriteRenderer sr = null;
    public Rigidbody2D rb = null;

    public float speed;
    public float gravity;
    public float movetime = 0.0f;

    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //カメラ内に入ってきた場合
        if(sr.isVisible)
        {
            movetime += Time.deltaTime;
            int Xvector = 0;
            int Yvector = 0;

            //時間経過で動きを変える
            if(movetime<=2)
            {
                Xvector = -1;
                Yvector = -1;

                //向きを変える
                anim.SetBool("left", true);
            }
            else if(movetime>2&&movetime<=4)
            {
                Xvector = -1;
                Yvector = 1;         
            }
            else if(movetime>4&&movetime<=6)
            {
                Xvector = 1;
                Yvector = 1;
                anim.SetBool("right", true);
            }
            else if(movetime>4&&movetime<=8)
            {
                Xvector = 1;
                Yvector = -1;
            }
            else if(movetime>8)
            {
                movetime = 0.0f;
            }

            //移動実行
            rb.velocity = new Vector2(Xvector * speed, Yvector*speed);
        }
        else
        {
            //カメラ外のときは動きを止める
            rb.Sleep();
        }
    }
}
