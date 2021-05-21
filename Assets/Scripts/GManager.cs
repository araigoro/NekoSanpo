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
    public float texttime = 0.0f;
    public float finishtime = 0.0f;
    public bool right = false;
    public bool left = false;
    public bool jump = false;
    public bool isjump = false;
    public bool rose = false;
    public bool clear = false;

    void Start()
    {
        clear = false;
    }

    // Update is called once per frame
    void Update()
    {
        FishCount.text = "×" + fishcount.ToString();
        if(Player.transform.position.x>0)
        {
            FirstText.SetActive(false);
        }
        if (clear == true)
        {
            finishtime += Time.deltaTime;
            if (finishtime > 2)
            {
                ReturnTitle();
            }
        }
    }
    public void ClearButton()
    {
        SceneManager.LoadScene("Stage2");
        PlayerPrefs.SetInt("Stage", 2);
    }
    public void Cleartext()
    {
        if(fishcount==10)
        {
            ClearText.SetActive(true);
        }     
    }
    public void ClearHeart()
    {
        if (rose == true)
        {
            ClearText.SetActive(true);
            clear = true;
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
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
        jump = true;
        isjump = true;
    }
    public void JPushUp()
    {
        jump = false;
        isjump = false;
    }
    public void ClearRecord()
    {
        PlayerPrefs.SetInt("CLEAR", 1);
    }
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
    public void ButtonYes()
    {
        fishcount = 0;
        RoseOb.SetActive(false);
        rose = true;
    }
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

}
