using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class EnterPauseMenu : MonoBehaviour
{
    public GameObject PlayerHub;
    public GameObject GameManager;
    public GameObject AccessPlayer;
    // Start is called before the first frame update
    public bool isPaused = false;
    private bool canClick = false;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(1).GetComponent<TMP_Text>().text = "Health: " + AccessPlayer.GetComponent<PlayerController>().Hp;
        transform.GetChild(2).GetComponent<TMP_Text>().text = "Mana: " + AccessPlayer.GetComponent<PlayerController>().Mana;
        transform.GetChild(3).GetComponent<TMP_Text>().text = "Stamina: " + AccessPlayer.GetComponent<PlayerController>().Stamina;
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!GameManager.GetComponent<GameManagerScript>().ReturnGameOver())
            {
                if(!isPaused) PopUpPauseMenu();
                else ClosePauseMenu();
            }                
            //Time.timeScale = 0;
        }
    }

    public void PopUpPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 1;
        //Debug.Log(Time.timeScale);
        DOTween.Kill(transform);
        transform.DOMoveZ(AccessPlayer.transform.position.z - 1, 0f).SetUpdate(true).SetLink(gameObject).OnComplete(() => 
        {
            canClick = true;
            Time.timeScale = 0;
        });
        PlayerHub.SetActive(false); 
         
    }

    public void ClosePauseMenu()
    {        
        isPaused = false;
        canClick = false;
        Time.timeScale = 1;
        DOTween.Kill(transform);
        PlayerHub.SetActive(true);
        transform.DOMoveZ(AccessPlayer.transform.position.z + 3, 0f).SetUpdate(true).OnComplete(() => 
        {
            Time.timeScale = 1;
        }).SetLink(gameObject);
        //Debug.Log(Time.timeScale); 
                
    }

    public void BackToMenu()
    {
        if(canClick)
        {
            KillTween();
            PlayerPrefs.DeleteAll();
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
        DOTween.KillAll();
    } 
}
