using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject[] TurretList;
    public GameObject[] EnemyList;
    public int playerWin = 100;
    public AudioSource FirstPharse;
    public AudioSource SeconPharse;
    public static event Action changePhrase;
 
    // Start is called before the first frame update
    void Start()
    {
        TurretList = GameObject.FindGameObjectsWithTag("Turret");
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        FirstPharse.Play();
        SeconPharse.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(TurretList != GameObject.FindGameObjectsWithTag("Turret"))
            TurretList = GameObject.FindGameObjectsWithTag("Turret");
        if(EnemyList != GameObject.FindGameObjectsWithTag("Enemy"))
            EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(TurretList.Length <= 3)
        {
            FirstPharse.Stop();
            SeconPharse.UnPause();
            changePhrase?.Invoke();
        }
        else
            SeconPharse.Pause();

        if(player.GetComponent<PlayerController>().isDead && TurretList.Length == 0 && EnemyList.Length == 0)
        {
            playerWin = 0;
        }
        else
        {
            if(TurretList.Length == 0 && EnemyList.Length == 0)
            {
                playerWin = 1;
            }
            if(player.GetComponent<PlayerController>().isDead)
            {
                playerWin = -1;
            }
        }
        
    }

    public bool ReturnGameOver()
    {
        if(playerWin == 100)
            return false;
        else
            return true;
    }
}
