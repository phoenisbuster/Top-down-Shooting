using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LogIn : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool isInputName = false;
    //private bool isInputPass = false;
    
    public GameObject warningNoName;
    public GameObject warningNoPass;

    public TMP_InputField userName;
    public TMP_InputField userPass;
    public void LoadScene()
    {
        string tempName = userName.text;
        string tempPass = userPass.text;
        if(!string.IsNullOrWhiteSpace(tempName) && !string.IsNullOrWhiteSpace(tempPass))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            //if(userName.Inpu)
            if(string.IsNullOrWhiteSpace(tempName))
            {
                warningNoName.SetActive(true);
            }
            if(string.IsNullOrWhiteSpace(tempPass))
            {
                warningNoPass.SetActive(true);
            }
            return;
        }
        
    }

}
