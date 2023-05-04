using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown graphicsDrop;
    public Slider MasterVolumeSlider;
    public AudioSource BgMusic_AudioSource;
    public AudioSource LevelComplete_AudioSource;
    public AudioSource GameOver_AudioSource;      

    [SerializeField] AudioSource BgMusic;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("settingsSaved", 0) == 0)
        {
            PlayerPrefs.SetInt("graphics", 0);
            PlayerPrefs.SetFloat("MasterVolumeSlider", 1.0f);
        }
        //Graphics
        if(PlayerPrefs.GetInt("graphics", 2) == 2)
        {
            graphicsDrop.value = 0;
            QualitySettings.SetQualityLevel(0);
        }
        if (PlayerPrefs.GetInt("graphics", 1) == 1)
        {
            graphicsDrop.value = 1;
            QualitySettings.SetQualityLevel(1);
        }
        if (PlayerPrefs.GetInt("graphics", 0) == 0)
        {
            graphicsDrop.value = 2;
            QualitySettings.SetQualityLevel(2);
        }
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolumeSlider", 1.0f);
        BgMusic_AudioSource.volume = MasterVolumeSlider.value;
        LevelComplete_AudioSource.volume = MasterVolumeSlider.value;
        GameOver_AudioSource.volume = MasterVolumeSlider.value;
        AudioListener.volume = PlayerPrefs.GetFloat("MasterVolumeSlider");
        
    }

    public void setGraphics()
    {
        if (graphicsDrop.value == 0)
        {
            PlayerPrefs.SetInt("graphics", 2);
            PlayerPrefs.Save();
            QualitySettings.SetQualityLevel(0);
        }
        if (graphicsDrop.value == 1)
        {
            PlayerPrefs.SetInt("graphics", 1);
            PlayerPrefs.Save();
            QualitySettings.SetQualityLevel(1);
        }
        if (graphicsDrop.value == 2)
        {
            PlayerPrefs.SetInt("graphics", 0);
            PlayerPrefs.Save();
            QualitySettings.SetQualityLevel(2);
        }
    } 
    
    public void setMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolumeSlider", MasterVolumeSlider.value);
        PlayerPrefs.Save();
        //SLIDERS VOLUME.
        AudioListener.volume = MasterVolumeSlider.value;
        BgMusic_AudioSource.volume = MasterVolumeSlider.value;
        LevelComplete_AudioSource.volume = MasterVolumeSlider.value;
        GameOver_AudioSource.volume = MasterVolumeSlider.value;
    }

    public void OnBgMusic()
    {
        BgMusic.mute = !BgMusic.mute;
    }

    public void saveSettings()
    {
        PlayerPrefs.SetInt("settingsSaved", 1);
        PlayerPrefs.Save();
    }
}
