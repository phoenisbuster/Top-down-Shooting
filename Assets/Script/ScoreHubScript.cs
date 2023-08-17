using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHubScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + player.GetComponent<PlayerController>().Score;
    }
}
