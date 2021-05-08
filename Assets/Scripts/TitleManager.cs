using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject Stage1Button;
    [SerializeField] GameObject Stage2Button;
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject Stage2ButtonText;
    [SerializeField] GameObject ImageLock;

    public bool b;
    public int clearstage;

    // Start is called before the first frame update
    void Start()
    {
        Stage1Button.SetActive(false);
        Stage2Button.SetActive(false);
        StartButton.SetActive(true);

        clearstage = PlayerPrefs.GetInt("CLEAR", 0);      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Startbutton()
    {
        Stage1Button.SetActive(true);
        Stage2Button.SetActive(true);
        StartButton.SetActive(false);

        switch (clearstage)
        {
            case 0:
                b = false;
                Stage2ButtonText.SetActive(false);
                ImageLock.SetActive(true);
                break;
            case 1:
                b = true;
                Stage2ButtonText.SetActive(true);
                ImageLock.SetActive(false);
                break;
        }
        //Buttonを呼び出すためにはusing UIが必要！
        Stage2Button.GetComponent<Button>().interactable = b;
    }
    public void StageButton(int num)
    {
        SceneManager.LoadScene("Stage" + num);
    }
}
