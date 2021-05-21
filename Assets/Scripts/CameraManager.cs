using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    int StageNo;

    // Start is called before the first frame update
    void Start()
    {
        StageNo = PlayerPrefs.GetInt("Stage", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, 0, -10);
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            switch (StageNo)
            {
                case 1:
                    if (transform.position.x > 99)
                    {
                        transform.position = new Vector3(99, 0, -10);
                    }
                    break;
                case 2:
                    if (transform.position.x > 153)
                    {
                        transform.position = new Vector3(153, 0, -10);
                    }
                    break;
            }
        }
    }
}
