using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour   

{   // Static instance of GameManager for easy access from other scripts
    public static GameManager instance;
    [SerializeField] private AudioSource GameOver;
    [SerializeField] private AudioSource Weeen;
    // ilagay mo dito ang GameObject na gusto mong magpakita kapag nanalo ka
    public  GameObject LevelCompleteUI;
    //GameObject pag natalo ka     
    public  GameObject GameOverUI;
    //GameObject of SettingsUI
    public GameObject SettingsUI;
    //Pause
    public static bool GameIsPaused = false;
    public  GameObject PauseMenuUI;   
    //Buttons Inside of Main Canvas
    public  GameObject PauseButton;
    public  GameObject SettingsButton;
    //Timer
    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;
    private bool stopTimer = false;
    private float timeLeft;
    private bool isTimerRunning;
     //Images 
    [SerializeField]
    private Sprite bgImage;    
    public  Sprite[] puzzles;   //Images
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public  List<Button> btns = new List<Button>(); 
    //PlayerPrefsLevelsUnlock
    public int levelToUnlock;
    int numberOfUnlockedLevels;
    //Logic of the Game (Pair/Match)
    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;


    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Images");
    }
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;

        timeLeft = gameTime;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        isTimerRunning = true;
        PauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = timeLeft;
            timerText.text = FormatTime(timeLeft);  

            if (timeLeft <= 0)
            {
                stopTimer = true;
                LevelCompleteUI.SetActive(false);
                GameOver.Play();
                GameOverUI.SetActive(true);
            }   
        }
    }

    void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("stopTimer value: " + stopTimer);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
    private void LevelCompleted()
    {
        int numberOfUnlockedLevels = PlayerPrefs.GetInt("levelsUnlocked");

        if(numberOfUnlockedLevels <= levelToUnlock)
        {
            PlayerPrefs.SetInt("levelsUnlocked", numberOfUnlockedLevels + 1);
        }

    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleCards");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if(index == looper/2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    public void PickPuzzle()
    {
        int buttonIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        // Check if the button is already matched or is the same as the previous guess
        if(!btns[buttonIndex].IsInteractable() || (firstGuess && firstGuessIndex == buttonIndex))
        {
            return;
        }
        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = buttonIndex;
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = buttonIndex;
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            if(firstGuessPuzzle == secondGuessPuzzle)
            {
                print("Cards Match");
            }
            else
            {
                print("Puzzle don't Match");  
            }
            StartCoroutine(checkThePuzzleMatch());
        }
    }

    IEnumerator checkThePuzzleMatch()
    {
        yield return new WaitForSeconds(0.3f);

        if(firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.3f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            CheckTheGameFinished();                  
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;            
        }
        yield return new WaitForSeconds(0.3f);
        firstGuess = secondGuess = false;
    }
    
    void CheckTheGameFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            print("Level Complete");
            print(" it took you "+ countGuesses + " ");
            Weeen.Play();
            LevelCompleteUI.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
            StopTimer();

        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            Sprite temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void Pause()
    {
        Debug.Log("Pausing Game.....");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void InviSettingsButton()
    {
        GameObject SettingsButton = GameObject.FindWithTag("SettingsButton");
        SettingsUI.SetActive(true);
        SettingsButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void HideAndShowPauseButton()
    {
       GameObject PauseButton = GameObject.FindWithTag("pauseButton");

        PauseButton.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Debug.Log("Resuming Game.....");
        PauseMenuUI.SetActive(false);
        SettingsUI.SetActive(false); 
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseButton.SetActive(true);
        SettingsButton.SetActive(true);
             
    }

}