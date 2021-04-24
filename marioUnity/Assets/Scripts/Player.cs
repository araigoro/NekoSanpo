using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Field")
        {
            GameObject gamemanager = GameObject.Find("GameManager");
            gamemanager.GetComponent<GameSceneManager>().FieldCheck();
        }
        if(collision.gameObject.tag=="Enemy")
        {
            GameObject gamemanager = GameObject.Find("GameManager");
            gamemanager.GetComponent<GameSceneManager>().GameOver();
        }
    }
}
