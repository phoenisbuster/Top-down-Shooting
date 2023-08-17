using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class GameOverBuffCount : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        int buffCount = player.GetComponent<PlayerController>().buffNo;
        DOVirtual.Int(0, buffCount, 1.5f, v => 
        {
            GetComponent<TMP_Text>().text = "Buff Count: " + v;
        }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>{
            //Time.timeScale = 0;
        }).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
