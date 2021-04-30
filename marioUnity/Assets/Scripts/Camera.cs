using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, 0, -10);
        if(transform.position.x<0)
        {
            transform.position = new Vector3(0, 0, -10);
        }
        else if(transform.position.x>99)
        {
            transform.position = new Vector3(99, 0, -10);
        }
           
           
    }
}
