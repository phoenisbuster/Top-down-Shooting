using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //public GameObject DisplayHp;
    //public GameObject DisplayMoney;
    //public GameObject DisplayDistance;
    //public GameObject DisplayPanel;
    public GameObject PlayerHub;
    public GameObject GameManager;
    public GameObject Canvas;
    // Start is called before the first frame update
    //private bool isPaused = false;
    private bool canClick = false;
    private int isWin;
    void Start()
    {
        //Display1Pos = DisplayHp.transform.position;
        //Display2Pos = DisplayMoney.transform.position;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetComponent<GameManagerScript>().ReturnGameOver())
        {
            isWin = GameManager.GetComponent<GameManagerScript>().playerWin;
            PlayerHub.SetActive(false); 
            PopUpGameOverMenu();
            //Time.timeScale = 0;          
        }
    }
    public void PopUpGameOverMenu()
    {
        //DOTween.Kill(transform);
        transform.DOMoveZ(Canvas.transform.position.z - 1, 1f).SetUpdate(true).OnComplete(() => 
        {
            canClick = true;
            transform.GetChild(0).GetComponent<TMP_Text>().text = isWin > 0? "You Win!" : "You Lose!";
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(7).gameObject.SetActive(true);
        }).SetLink(gameObject);
        //DisplayPanel.SetActive(false);
        //DisplayHp.SetActive(false);
        //Time.timeScale = 0;     
    }

    public void BackToMenu()
    {
        if(canClick)
        {
            KillTween();
            SceneManager.LoadScene("Menu");
        }
            
    }

    public void RestartGame()
    {
        if(canClick)
        {
            KillTween();
            SceneManager.LoadScene(2);
        }
            
    }

    public void QuitGame()
    {
        if(canClick)
        {
            KillTween();
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }            
    }  

    public void KillTween()
    {
        //DOTween.Kill(transform);
        //Time.timeScale = 1;
        //AccessPlayer.GetComponent<PlayerMovement>().isReset = true;
        DOTween.KillAll();
    } 
}
