using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDistance : MonoBehaviour
{
    public float distanceCount;
    //public float disCount;

    public GameObject player;
    public TMP_Text display;
    //public GameObject displayDis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //distanceCount = player.GetComponent<PlayerMovement>().disTravel;
        //Round to 2 decimal place
        double mult = Math.Pow(10.0, 2);
        double result = Math.Truncate( mult * distanceCount ) / mult;
        distanceCount = (float) result;
        
        display.GetComponent<TMP_Text>().text = "" + distanceCount;
        //displayDis.GetComponent<Text>().text = "" + disCount;
    }
}
