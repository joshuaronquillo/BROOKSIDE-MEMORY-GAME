using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public void Play(int SIMULATION)
    {
      Debug.Log("Loading Levels...."); 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SIMULATION); 
    }

    public void nextLevel(int SIMULATION)
    {
        Debug.Log("Loading Next Level....");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SIMULATION);
    }

    public void Menu(int SIMULATION)
    {
        Debug.Log("Loading Menu....");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SIMULATION);
    }

    public void LevelSelection(int SIMULATION)
    {
        Debug.Log("Loading SelectLevelScene....");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SIMULATION);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game..");
        Time.timeScale = 1.5f;
        Application.Quit(); 
    }

}