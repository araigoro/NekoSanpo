using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI FishCount;
    
    [SerializeField] GameObject ClearText;
    [SerializeField] GameObject FirstText;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject RoseText1;
    [SerializeField] GameObject RoseText2;
    [SerializeField] GameObject RoseOb;

    public int fishcount;
    public int StageNo;
    public float texttime = 0.0f;
    public float finishtime = 0.0f;
    public bool right = false;
    public bool left = false;
    public bool jump = false;
    public bool isjump = false;
    public bool rose = false;
    public bool clear = false;

    public AudioClip[] SE;
    private AudioSource audioSource;  

    void Start()
    {
        clear = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        rose = false;
    }

    // Update is called once per frame
    void Update()
    {
        //取得した魚の数を表示
        FishCount.text = "×" + fishcount.ToString();

        //プレイヤーが少し動いたら最初のテキストを消す
        if(Player.transform.position.x>0)
        {
            FirstText.SetActive(false);
        }
        //クリアした際の処理
        if (clear == true)
        {
            //2秒後にタイトルに戻る
            finishtime += Time.deltaTime;
            if (finishtime > 2)
            {
                ReturnTitle();
            }
        }
    }

    //クリアボタン処理
    public void ClearButton()
    {
        //ステージ２をロードかつステージ数を記録
        SceneManager.LoadScene("Stage2");
        PlayerPrefs.SetInt("Stage", 2);
    }

    //クリアテキストの表示
    public void Cleartext()
    {
        if(fishcount==10)
        {
            ClearText.SetActive(true);
        }     
    }

    //ステージ２のクリア演出
    public void ClearHeart()
    {
        if (rose == true)
        {
            ClearText.SetActive(true);
            clear = true;
        }
    }
    //ゲームオーバー処理
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    //右ボタン処理
    public void RPushDown()
    {
        right = true;
    }
    public void RPushUp()
    {
        right = false;
    }

    //左ボタン処理
    public void LPushDown()
    {
        left = true;
    }
    public void LPushUp()
    {
        left = false;
    }
    //ジャンプボタン処理
    public void JPushDown()
    {
        jump = true;
        isjump = true;
    }
    public void JPushUp()
    {
        jump = false;
        isjump = false;
    }

    //クリアしたステージを記録
    public void ClearRecord()
    {
        PlayerPrefs.SetInt("CLEAR", 1);
    }

    //バラの表示
    public void Rose()
    {
        if(fishcount==10)
        {
            RoseText1.SetActive(true);
        }
        else
        {
            RoseText2.SetActive(true);
        }
    }

    //YESボタン処理
    public void ButtonYes()
    {
        fishcount = 0;
        RoseOb.SetActive(false);
        rose = true;
    }
    //NOボタン処理
    public void ButtonNo()
    {
        RoseText1.SetActive(false);
    }
    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void RoseExit()
    {
        RoseText1.SetActive(false);
        RoseText2.SetActive(false);
    }

    //魚を取得した際に音を鳴らす
    public void FishSE()
    {
        audioSource.PlayOneShot(SE[0]);
    }

    //クリア時の音を鳴らす
    public void ClearSE()
    {
        //クリアしたステージによって音を変える
        StageNo = PlayerPrefs.GetInt("Stage,1");
        switch(StageNo)
        {
            case 1:
                audioSource.PlayOneShot(SE[1]);
                break;
            case 2:
                audioSource.PlayOneShot(SE[2]);
                break;
        }
    }

}
