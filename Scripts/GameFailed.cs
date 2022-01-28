using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFailed : MonoBehaviour
{
    [SerializeField] Button Play;
    [SerializeField] Button RestartLevel;

    [SerializeField] GameObject GameFailedUI;
    [SerializeField] GameObject UICanvas;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject Joystick;

    public static bool IsGamePaused = false;

    private void Awake()
    {
        gameObject.SetActive(false);

        RestartLevel.onClick.AddListener(Restart);
        Play.onClick.AddListener(PlayGame);
    }

    public void Restart()
    {
        IsGamePaused = false;
        GameFailedUI.SetActive(false);
        UICanvas.SetActive(true);

        Time.timeScale = 1f;

        LoadingScreen.SetActive(true);
        levelLoader.LoadNextLevel(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        IsGamePaused = false;
        GameFailedUI.SetActive(false);
        UICanvas.SetActive(true);
        Joystick.SetActive(true);

        Time.timeScale = 1f;
    }

}
