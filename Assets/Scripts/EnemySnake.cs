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
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sr.isVisible)
        {
            movetime += Time.deltaTime;
            int xVector = -1;
            
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
           
            rb.velocity = new Vector2(xVector * speed, -gravity);          
        }
        else
        {
            rb.Sleep();
        }
    }
}
