using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] GameObject Manager;
    [SerializeField] GameObject car;
    GManager gmanager;
    

    public float speed;
   
    public bool player;
    
   
    // Start is called before the first frame update
    void Start()
    {
        gmanager = Manager.GetComponent<GManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == true)
        {
            if (car.transform.position.x < 142)
            {
                car.transform.Translate(new Vector2(speed, 0));
            }
        }
    }
    
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&gmanager.rose==true)
        {
            player = true;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            player = false;
        }
    }
}
