using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPage : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject SettingBtn;
    public GameObject QuitBtn;
    public GameObject SettingPanel;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }

    public void Setting()
    {
        PlayButton.SetActive(false);
        SettingBtn.SetActive(false);
        QuitBtn.SetActive(false);
        SettingPanel.SetActive(true);
    }

    public void Quit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void BacktoMenu()
    {
        PlayButton.SetActive(true);
        SettingBtn.SetActive(true);
        QuitBtn.SetActive(true);
        SettingPanel.SetActive(false);
    }
}
