using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonStart()
    {
        SceneManager.LoadScene("Title");
    }
    public void ButtonRetry()
    {
        SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("Stage", 1));
    }
}
