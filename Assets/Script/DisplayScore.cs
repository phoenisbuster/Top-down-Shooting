using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public int moneyCount;
    //public float disCount;

    public GameObject player;
    public GameObject display;
    //public GameObject displayDis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moneyCount = player.GetComponent<PlayerMovement>().MoneyCount;
        display.GetComponent<Text>().text = "" + moneyCount;
        //displayDis.GetComponent<Text>().text = "" + disCount;
    }
}
