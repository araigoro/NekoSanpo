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

    public int fishcount;
    public bool right = false;
    public bool left = false;
    public bool jump = false;
    public bool isjump = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FishCount.text = "×" + fishcount.ToString();
        if(Player.transform.position.x>0)
        {
            FirstText.SetActive(false);
        }
    }
    public void ClearButton()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void Cleartext()
    {
        if(fishcount==10)
        {
            ClearText.SetActive(true);
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

}
