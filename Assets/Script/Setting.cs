using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider Sound;
    public Slider Brightness;
    // Start is called before the first frame update
    void Start()
    {
        Sound.value = PlayerPrefs.GetFloat("Sound", 1);
        Brightness.value = PlayerPrefs.GetFloat("Bright", 1);
    }
    public void AdjustAmbientLight()
    {
        float rbgValue = Brightness.value;
        RenderSettings.ambientLight = new Color (rbgValue, rbgValue, rbgValue, 1);
        PlayerPrefs.SetFloat("Bright", Brightness.value);
    }
 
    public void AdjustVolume()
    {
        //Sound.value = PlayerPrefs.GetFloat("Sound", 1);
        float volume = Sound.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Sound", Sound.value);
    }

    public void SaveSetting()
    {
        Sound.value = PlayerPrefs.GetFloat("Sound", 1);
        Brightness.value = PlayerPrefs.GetFloat("Bright", 1);
    }

}
