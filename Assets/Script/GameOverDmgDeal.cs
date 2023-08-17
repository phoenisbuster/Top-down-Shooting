using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class GameOverDmgDeal : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        float dmgDeal = player.GetComponent<PlayerController>().dmgDeal;
        DOVirtual.Float(0, dmgDeal, 1.5f, v => 
        {
            double mult = Math.Pow(10.0, 2);
            double result = Math.Truncate( mult * v ) / mult;
            v = (float) result;
            GetComponent<TMP_Text>().text = "Damage Deal: " + v;
        }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>{
            //Time.timeScale = 0;
        }).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
