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
        //ステージ１か２を判別
        StageNo = PlayerPrefs.GetInt("Stage", 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Xの位置をキャラクターと合わせて追従するようにする
        transform.position = new Vector3(Player.transform.position.x, 0, -10);

        //0以下の位置（ステージ外・左端）にはいかないようにする
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            //ステージ外・右端にいかないようにする
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
