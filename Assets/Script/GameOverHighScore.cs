using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameOverHighScore : MonoBehaviour
{
    public GameObject Score;
    public int HighScore;
    // Start is called before the first frame update
    void Start()
    {
        int oldScore = PlayerPrefs.GetInt("HighScore", 0);
        GetComponent<TMP_Text>().text = "" + oldScore;
        int num = Score.GetComponent<GameOverScore>().Score;
        float time = 0;
        if(num > oldScore)
        {
            time = 1.5f;
        }            
        else
        {
            time = 0.01f;
            num = oldScore;            
        }  
        PlayerPrefs.SetInt("HighScore", num);  
        DOVirtual.Int(oldScore, num, time, v => 
        {
            GetComponent<TMP_Text>().text = "" + v;
        }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>
            {
                Time.timeScale = 0;
                Debug.Log("GameOver");
            }).SetLink(gameObject).SetDelay(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
