using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using UnityEngine.Networking;

public class Menu : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button settings;
    [SerializeField] Button leaderboard;
    [SerializeField] Button quit;
    [SerializeField] Button credits;
    [SerializeField] Button Profile;

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject CreditsScreen;
    [SerializeField] GameObject LeaderBoard;
    [SerializeField] GameObject PlayerName;
    [SerializeField] GameObject LevelSelection;
    [SerializeField] GameObject UserProfile;
    [SerializeField] GameObject InternetDialog;

    [SerializeField] Toggle JoyToggle;
    [SerializeField] Toggle ButtonToggle;

    [SerializeField] LevelLoader levelLoader;

    [SerializeField] TextMeshProUGUI Title;

    [SerializeField] AudioSource MenuMusic;
    [SerializeField] AudioSource ClickSound;

    int L1P;
    int L2P;
    int L3P;
    int PP;
    int SP;
    int LP;
    int CP;

    /*void OnGUI()
    {
        //Delete all of the PlayerPrefs settings by pressing this Button
        if (GUI.Button(new Rect(100, 200, 200, 60), "Delete"))
        {
            PlayerPrefs.DeleteAll();
        }
    }*/

    private void Awake()
    {
        PlayerPrefs.SetInt("PrevLevel", 0); //for checking if player goes to next level instead of loading from menu... this is used for carrying the score to the next level
        //PlayerPrefs.SetString("PlayerScore", "0");
    }
    private void Start()
    {
        SettingsMenu.SetActive(false);
        LoadingScreen.SetActive(false);
        LeaderBoard.SetActive(false);
        PlayerName.SetActive(false);

        MainMenu.SetActive(true);

        MenuMusic.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
        ClickSound.volume = PlayerPrefs.GetFloat("masterVolume", 5f);

        L1P = int.Parse(PlayerPrefs.GetString("L1P", "0"));
        L2P = int.Parse(PlayerPrefs.GetString("L2P", "0"));
        L3P = int.Parse(PlayerPrefs.GetString("L3P", "0"));
        PP = int.Parse(PlayerPrefs.GetString("PP", "0"));
        SP = int.Parse(PlayerPrefs.GetString("SP", "0"));
        LP = int.Parse(PlayerPrefs.GetString("LP", "0"));
        CP = int.Parse(PlayerPrefs.GetString("CP", "0"));

        //Button actions at start performs better than update //ref:https://stackoverflow.com/questions/39571316/take-long-second-loading-new-simple-scene-in-unity
        play.onClick.AddListener(Play);
        //quit.onClick.AddListener(Quit);
        settings.onClick.AddListener(Settings);
        credits.onClick.AddListener(Credits);
        leaderboard.onClick.AddListener(OpenLeaderBoard);
        Profile.onClick.AddListener(ShowProfile);

        if (PlayerPrefs.GetString("controller") == "") 
            PlayerPrefs.SetString("controller", "buttons");
        
        if(PlayerPrefs.GetInt("FirstPlay", 1) == 1)
        {
            Profile.gameObject.SetActive(false);
        }

        else
        {
            Profile.gameObject.SetActive(true);
        }

        UpdateBadgePrefs();

        //CheckForInternet();
    }

    void Play()
    {           
        if (PlayerPrefs.GetInt("FirstPlay",1) == 1)
        {
            MainMenu.SetActive(false);
            LoadingScreen.SetActive(true);
            //levelLoader.LoadNextLevel("Story");
            PlayerName.SetActive(true);
            Title.color = Color.white;
        }

        else
        {
            MainMenu.SetActive(false);
            LevelSelection.SetActive(true);
            //LoadingScreen.SetActive(true);
            //levelLoader.LoadNextLevel("Level3");
            //levelLoader.LoadNextLevel("Level2");
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    void Settings()
    {
        SP++;
        PlayerPrefs.SetString("SP", SP.ToString());
        SettingsMenu.SetActive(true);
        Title.enabled = false;

        string Controller = PlayerPrefs.GetString("controller");

        if (Controller == "buttons")
            ButtonToggle.isOn = true;
        else
            JoyToggle.isOn = true;

        MainMenu.SetActive(false);
    }

    void Credits()
    {
        CP++;
        PlayerPrefs.SetString("CP", CP.ToString());
        CreditsScreen.SetActive(true);
        Title.enabled = false;
        MainMenu.SetActive(false);
    }

    void OpenLeaderBoard()
    {
        LP++;
        PlayerPrefs.SetString("LP", LP.ToString());
        LeaderBoard.SetActive(true);
        Title.enabled = false;
        MainMenu.SetActive(false);
    }

    void ShowProfile()
    {
        PP++;
        PlayerPrefs.SetString("PP", PP.ToString());
        UserProfile.SetActive(true);
    }

    IEnumerator PlayClickSound()
    {
        yield return new WaitForSeconds(0.5f);
        CreditsScreen.SetActive(true);
        Title.enabled = false;
        MainMenu.SetActive(false);
    }

    public void Load_Level(string Level)
    {
        string Level_Toast = "Complete the Previous Level";

        if (Level == "1")
        {
            L1P++;
            PlayerPrefs.SetString("L1P", L1P.ToString());
            Score.instance.UploadBadges();

            Title.color = Color.white;
            LoadingScreen.SetActive(true);
            levelLoader.LoadNextLevel("Story");
            //levelLoader.LoadNextLevel("PostLevelQuiz");
            LevelSelection.SetActive(false);
        }

        else if (Level == "2")
        {
            /*if (PlayerPrefs.GetInt("LoadLevel2") == 1)
            {*/
                L2P++;
                PlayerPrefs.SetString("L2P", L2P.ToString());
                Score.instance.UploadBadges();

                Title.color = Color.white;
                LoadingScreen.SetActive(true);
                levelLoader.LoadNextLevel("Story2");
                //levelLoader.LoadNextLevel("PostLevelQuiz2");
                LevelSelection.SetActive(false);
            //}

            /*else 
                ShowToast(Level_Toast);*/
        }

        else if (Level == "3")
        {
            /*if (PlayerPrefs.GetInt("LoadLevel3") == 1)
            {*/
                L3P++;
                PlayerPrefs.SetString("L3P", L3P.ToString());
                Score.instance.UploadBadges();

                Title.color = Color.white;
                LoadingScreen.SetActive(true);
                levelLoader.LoadNextLevel("Story3");
                //levelLoader.LoadNextLevel("PostLevelQuiz3");
                LevelSelection.SetActive(false);
            //}

            /*else
                ShowToast(Level_Toast);*/
        }
    }

    public void Back_To_MainMenu()
    {
        LevelSelection.SetActive(false);
        MainMenu.SetActive(true);
    }

    void ShowToast(string Level_Toast)
    {
        //https://agrawalsuneet.github.io/blogs/native-android-in-unity/ for ref

        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
        object[] toastParams = new object[3];

        AndroidJavaClass unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        
        toastParams[0] = unityActivity.GetStatic<AndroidJavaObject> ("currentActivity");
        toastParams[1] = Level_Toast;
        toastParams[2] = toastClass.GetStatic<int> ("LENGTH_LONG");

        AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject> ("makeText", toastParams);

        toastObject.Call("show");
    }

    void UpdateBadgePrefs()
    {
        int hiddenAreas = int.Parse(PlayerPrefs.GetString("HiddenAreaCount", "0"));
        int quiz = int.Parse(PlayerPrefs.GetString("igQuiz", "0"));
        int destroyer = int.Parse(PlayerPrefs.GetString("destroyer", "0"));
        int postquiz = int.Parse(PlayerPrefs.GetString("postQuiz", "0"));

        if (hiddenAreas >= 5)
            PlayerPrefs.SetInt("HA", 1);

        if (quiz >= 8)
            PlayerPrefs.SetInt("IGQ",1);

        if (destroyer >= 25)
            PlayerPrefs.SetInt("D", 1);

        if (postquiz == 3)
            PlayerPrefs.SetInt("PQ", 1);
    }

    IEnumerator checkInternetConnection(Action<bool> action) //https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
    {
        WWW www = new WWW("https://www.google.com/");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    public void CheckForInternet()
    {
        StartCoroutine(checkInternetConnection((isConnected) =>  //https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
        {
            if (isConnected)
            {
                InternetDialog.SetActive(false);
                Debug.Log("Connected");
            }
            else
            {
                InternetDialog.SetActive(true);
                MainMenu.SetActive(false);
                Debug.Log("Not Connected");
            }
        }));
    }
}