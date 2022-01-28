using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    [SerializeField] Button RestartLevel;
    [SerializeField] Button Menu;
    [SerializeField] Button NextButton;

    [SerializeField] GameObject GameCompleteUI;
    [SerializeField] GameObject UICanvas;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] GameObject LoadingScreen;

    public static bool IsGamePaused = false;

    private void Awake()
    {
        gameObject.SetActive(false);

        RestartLevel.onClick.AddListener(Restart);
        Menu.onClick.AddListener(MainMenu);
        NextButton.onClick.AddListener(Next);
    }

    public void Restart()
    {
        IsGamePaused = false;
        GameCompleteUI.SetActive(false);
        UICanvas.SetActive(true);

        Time.timeScale = 1f;

        LoadingScreen.SetActive(true);
        //levelLoader.LoadNextLevel("Traps");

        levelLoader.LoadNextLevel(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        IsGamePaused = false;
        GameCompleteUI.SetActive(false);

        Time.timeScale = 1f;

        LoadingScreen.SetActive(true);
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Next()
    {
        PlayerPrefs.SetInt("PrevLevel", 1);
        PlayerPrefs.SetString("LevelScore", Score.instance.score.ToString());

        IsGamePaused = false;
        GameCompleteUI.SetActive(false);

        Time.timeScale = 1f;
        LoadingScreen.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Traps") {

            levelLoader.LoadNextLevel("PostLevelQuiz");
            //levelLoader.LoadNextLevel("Story2");
        }

        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            levelLoader.LoadNextLevel("PostLevelQuiz2");
            //levelLoader.LoadNextLevel("Story3");
        }

        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            levelLoader.LoadNextLevel("PostLevelQuiz3");
            //levelLoader.LoadNextLevel("GameComplete");
        }
    }
}