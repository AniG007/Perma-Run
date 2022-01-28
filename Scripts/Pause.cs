using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Pause : MonoBehaviour
{

    public static bool IsGamePaused = false;

    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject UICanvas;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject InfoScreen;
    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject AppFeaturesScreen;
    [SerializeField] GameObject DialogBox;
    [SerializeField] GameObject PlayerControl;
    [SerializeField] GameObject ExitGameCanvas;


    [SerializeField] Button ResumeGame;
    [SerializeField] Button RestartLevel;
    [SerializeField] Button Menu;
    [SerializeField] Button ButtonInfo;
    [SerializeField] Button AppFeatures;
    [SerializeField] Button PermissionInfo;
    [SerializeField] Button BackButton;

    int PI;
    int AF;

    private void Awake()
    {
        gameObject.SetActive(false);
        LoadingScreen.SetActive(false);

        ResumeGame.onClick.AddListener(Resume);
        RestartLevel.onClick.AddListener(Restart);
        Menu.onClick.AddListener(MainMenu);
        ButtonInfo.onClick.AddListener(Info);
    }

    private void Start()
    {
        PI = int.Parse(PlayerPrefs.GetString("PI","0"));
        AF = int.Parse(PlayerPrefs.GetString("AF", "0"));
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        UICanvas.SetActive(true);
        IsGamePaused = false;

        if (PlayerPrefs.GetString("controller") != "buttons")
        {
            Joystick.GetComponentInChildren<Image>().enabled = true;
            Joystick.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    public void Restart ()
    {
        IsGamePaused = false;
        PauseMenuUI.SetActive(false);
        UICanvas.SetActive(true);

        Time.timeScale = 1f;

        LoadingScreen.SetActive(true);
        levelLoader.LoadNextLevel(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        IsGamePaused = false;
        PauseMenuUI.SetActive(false);

        //Time.timeScale = 1f;

        ExitGameCanvas.SetActive(true);

        /*LoadingScreen.SetActive(true);
        levelLoader.LoadNextLevel("MainMenu");*/
    }

    public void Info()
    {
        //InfoScreen.SetActive(true);
        AppFeatures.gameObject.SetActive(true);
        PermissionInfo.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);

        ResumeGame.gameObject.SetActive(false);
        RestartLevel.gameObject.SetActive(false);
        Menu.gameObject.SetActive(false);
        ButtonInfo.gameObject.SetActive(false);
    }

    IEnumerator Resumer()
    {
        yield return new WaitForSeconds(0.5f);
        PauseMenuUI.SetActive(false);
        UICanvas.SetActive(true);
        IsGamePaused = false;

        StopCoroutine(Resumer());
        Time.timeScale = 1f;   
    }

    public void Back()
    {
        AppFeatures.gameObject.SetActive(false);
        PermissionInfo.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);

        ResumeGame.gameObject.SetActive(true);
        RestartLevel.gameObject.SetActive(true);
        Menu.gameObject.SetActive(true);
        ButtonInfo.gameObject.SetActive(true);
    }
    
    public void ShowPermissionsInfo() 
    {
        PI++;
        PlayerPrefs.SetString("PI", PI.ToString());
        InfoScreen.SetActive(true);
    }

    public void ShowAppFeatures()
    {
        AF++;
        PlayerPrefs.SetString("AF", AF.ToString());
        AppFeaturesScreen.SetActive(true);
    }

    /*public void ClosePermissionInfoScreen()
    {
        InfoScreen.SetActive(false);

        if (DialogBox.activeSelf == false)
        {
            DialogBox.SetActive(true);
            PlayerControl.SetActive(true);
        }
    }*/
}
