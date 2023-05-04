using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BgMusicVolumeController : MonoBehaviour
{
    public Slider BgMusic_Slider;
    public AudioSource BgMusic_AudioSource;

    void Start()
    {
        if(PlayerPrefs.GetInt("settingsSaved", 0) == 0)
        {
            PlayerPrefs.SetFloat("BgMusic_Slider", 1.0f);
        }
        BgMusic_Slider.value = PlayerPrefs.GetFloat("BgMusic_Slider", 1.0f);
        BgMusic_AudioSource.volume = BgMusic_Slider.value;
        AudioListener.volume = PlayerPrefs.GetFloat("BgMusic_Slider");
    }
    public void setBgMusicVolume()
    {
        PlayerPrefs.SetFloat("BgMusic_Slider", BgMusic_Slider.value);
        PlayerPrefs.Save();
        AudioListener.volume = BgMusic_Slider.value;
    }
}