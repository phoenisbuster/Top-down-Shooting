using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverScore : MonoBehaviour
{
    public GameObject player;
    public int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Score = player.GetComponent<PlayerController>().Score;
        DOVirtual.Int(0, Score, 1.5f, v => 
        {
            GetComponent<TMP_Text>().text = "Score: " + v;
        }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>{
            //Time.timeScale = 0;
        }).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
