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
        //コンポーネント取得
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //スタート画面に戻る処理
    public void ButtonStart()
    {
        SceneManager.LoadScene("Title");
    }

    //同じステージを再ロード
    public void ButtonRetry()
    {
        SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("Stage", 1));
    }
}
