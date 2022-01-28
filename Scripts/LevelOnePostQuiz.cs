using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelOnePostQuiz : MonoBehaviour
{
    [SerializeField] GameObject LevelLoaderUI;
    [SerializeField] LevelLoader LevelLoader;
    [SerializeField] GameObject Screen1;
    [SerializeField] GameObject Screen2;
    [SerializeField] GameObject Screen3;
    [SerializeField] GameObject AppFeaturesScreen;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject NoSelectionPanel;

    [SerializeField] Toggle ActivityToggle;
    [SerializeField] Toggle CalendarToggle;
    [SerializeField] Toggle CameraToggle;
    [SerializeField] Toggle ContactsToggle;
    [SerializeField] Toggle LocationToggle;
    [SerializeField] Toggle MicToggle;
    [SerializeField] Toggle PhoneToggle;
    [SerializeField] Toggle SensorsToggle;
    [SerializeField] Toggle SMSToggle;
    [SerializeField] Toggle StorageToggle;

    [SerializeField] TextMeshProUGUI PostResult;
    [SerializeField] TextMeshProUGUI PermissionDescription;

    [SerializeField] Image PermissionImage;
    [SerializeField] Image PermissionImage2;
    [SerializeField] GameObject[] PermissionSprites = new GameObject[10];

    int postquiz;

    /*private string BaseURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd3RaFW4FUiGPNrIljNBFyZRuM8MUVQlyilln7LSOUqKqyddQ/formResponse";*/

    private string BaseURL = "https://docs.google.com/forms/d/e/1FAIpQLSfswUU60z9pAJgleFKOuCNd53Zfpm74946NUiFwgO6Cjc-e1w/formResponse";

    int PostLevelOneCorrectAnswerCount = 0;
    int PostLevelOneWrongAnswerCount = 0;

    string CurrentScene;

    string wrongPermsForLog;
    string PreQuizWrongPerms;

    int L1P;
    int L2P;
    int L3P;

    private void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        postquiz = int.Parse(PlayerPrefs.GetString("postQuiz","0"));
        wrongPermsForLog = "";
        PreQuizWrongPerms = PlayerPrefs.GetString("PreQuizWrongPerms", "");

        L1P = int.Parse(PlayerPrefs.GetString("L1P", "0"));
        L2P = int.Parse(PlayerPrefs.GetString("L2P", "0"));
        L3P = int.Parse(PlayerPrefs.GetString("L3P", "0"));
    }

    public void NavigateToResults()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);

        if (CurrentScene == "PostLevelQuiz")
        {
            if (PlayerPrefs.GetInt("Level1PostCorrect") == 7)
            {
                if (postquiz < 3)
                {
                    postquiz += 1;
                    PlayerPrefs.SetString("postQuiz", postquiz.ToString());
                }

                else
                {
                    if (PlayerPrefs.GetInt("PQ") == 0)
                    {
                        PlayerPrefs.SetInt("PQ", 1);
                        Score.instance.UploadBadges();
                    }
                }

                PostResult.text = "AWESOME!! YOU SCORED " + PlayerPrefs.GetInt("Level1PostCorrect").ToString() + "/7";
            }

            else
            {
                PostResult.text = "YOU SCORED " + PlayerPrefs.GetInt("Level1PostCorrect").ToString() + "/7";
            }
        }
        else if (CurrentScene == "PostLevelQuiz2")
        {
            if (PlayerPrefs.GetInt("Level1PostCorrect") == 5)
            {
                if (postquiz < 3)
                {
                    postquiz += 1;
                    PlayerPrefs.SetString("postQuiz", postquiz.ToString());
                }

                else
                {
                    if (PlayerPrefs.GetInt("PQ") == 0)
                    {
                        PlayerPrefs.SetInt("PQ", 1);
                        Score.instance.UploadBadges();
                    }
                }

                PostResult.text = "AWESOME!! YOU SCORED " + PlayerPrefs.GetInt("Level1PostCorrect").ToString() + "/5";
            }

            else
            {
                PostResult.text = "YOU SCORED " + PlayerPrefs.GetInt("Level1PostCorrect").ToString() + "/5";
            }
        }

        else if (CurrentScene == "PostLevelQuiz3")
        {
            if (PlayerPrefs.GetInt("Level1PostCorrect") == 3)
            {
                if (postquiz < 3)
                {
                    postquiz += 1;
                    PlayerPrefs.SetString("postQuiz", postquiz.ToString());
                }

                else
                {
                    if(PlayerPrefs.GetInt("PQ") == 0)
                    {
                        PlayerPrefs.SetInt("PQ", 1);
                        Score.instance.UploadBadges();
                    }
                }

                PostResult.text = "AWESOME!! YOU SCORED " + PlayerPrefs.GetInt("Level1PostCorrect").ToString() + "/3";
            }

            else
            {
                PostResult.text = "YOU SCORED " + PlayerPrefs.GetInt("Level1PostCorrect").ToString() + "/3";
            }
        }
    }

    public void OnSubmitClicked()
    {
        if (MicToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz")
                PostLevelOneCorrectAnswerCount++;
            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " MIC ";
            }

        }

        if (LocationToggle.isOn)
        {
            //if (CurrentScene == "PostLevelQuiz" || CurrentScene == "PostLevelQuiz2")
            PostLevelOneCorrectAnswerCount++;
            /*else
                PostLevelOneWrongAnswerCount++;*/
        }

        if (StorageToggle.isOn)
        {
            //if (CurrentScene == "PostLevelQuiz" || CurrentScene == "PostLevelQuiz2")
                PostLevelOneCorrectAnswerCount++;
            /*else
                PostLevelOneWrongAnswerCount++;*/
        }

        if (ActivityToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz2")
                PostLevelOneCorrectAnswerCount++;

            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " AR ";
            }
        }

        if (CalendarToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz3")
                PostLevelOneCorrectAnswerCount++;
            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " CAL ";
            }
        }

        if (CameraToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz" || CurrentScene == "PostLevelQuiz2")
                PostLevelOneCorrectAnswerCount++;
            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " CAM ";
            }
        }

        if (ContactsToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz")
                PostLevelOneCorrectAnswerCount++;
            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " CON ";
            }
        }

        if (PhoneToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz")
                PostLevelOneCorrectAnswerCount++;
            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " TP ";
            }
        }

        if (SensorsToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz2")
                PostLevelOneCorrectAnswerCount++;

            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " SEN ";
            }
        }

        if (SMSToggle.isOn)
        {
            if (CurrentScene == "PostLevelQuiz")
                PostLevelOneCorrectAnswerCount++;
            else
            {
                PostLevelOneWrongAnswerCount++;
                wrongPermsForLog += " SMS ";
            }
        }

        if (!SMSToggle.isOn && !SensorsToggle.isOn && !PhoneToggle.isOn && !ContactsToggle.isOn && !CameraToggle.isOn && !CalendarToggle.isOn && !ActivityToggle.isOn && !StorageToggle.isOn && !LocationToggle.isOn && !MicToggle.isOn)
            NoSelectionPanel.SetActive(true);
        else if (SMSToggle.isOn || SensorsToggle.isOn || PhoneToggle.isOn || ContactsToggle.isOn || CameraToggle.isOn || CalendarToggle.isOn || ActivityToggle.isOn || StorageToggle.isOn || LocationToggle.isOn || MicToggle.isOn)
        {
            PlayerPrefs.SetInt("Level1PostCorrect", PostLevelOneCorrectAnswerCount);
            PlayerPrefs.SetInt("Level1PostWrong", PostLevelOneWrongAnswerCount);

            NavigateToResults();
        }
    }

    public void MainMenu()
    {
        LevelLoaderUI.SetActive(true);
        LevelLoader.LoadNextLevel("MainMenu");
    }

    public void NavigateToEnd()
    {
        StartCoroutine(UploadData());
        Screen2.SetActive(false);
        //Screen3.SetActive(true);

        LevelLoaderUI.SetActive(true);

        if (CurrentScene == "PostLevelQuiz")
        {
            L2P++;
            PlayerPrefs.SetString("L2P", L2P.ToString());
            Score.instance.UploadBadges();
            LevelLoader.LoadNextLevel("Story2");
        }
        else if (CurrentScene == "PostLevelQuiz2")
        {
            L3P++;
            PlayerPrefs.SetString("L3P", L3P.ToString());
            Score.instance.UploadBadges();
            LevelLoader.LoadNextLevel("Story3");
        }
        else if (CurrentScene == "PostLevelQuiz3")
        {
            LevelLoader.LoadNextLevel("StoryEnd");
        }
    }

    public void Back()
    {
        PostLevelOneCorrectAnswerCount = 0;
        PostLevelOneWrongAnswerCount = 0;
        Screen2.SetActive(false);
        Screen1.SetActive(true);
    }

    public void PermissionInfo(string PermissionName)
    {
        if (PermissionName == "ActivityRecognition")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[0].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to recognize Physical Activity (Eg: Walking, Running, Cycling etc.). This is not required for a Communication App's functionality.";

            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "This Permission is required for an Activity Tracker to recognize Physical Activity (Eg: Walking, Running, Cycling etc.)";

            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to recognize Physical Activity (Eg: Walking, Running, Cycling etc.). This is not required for an Event Booking app.";

        }

        if (PermissionName == "Calendar")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[1].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to access the user's Calendar Data (For Setting and Retrieving Reminders). This is not required for a Communication App's functionality.";

            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to access the user's Calendar Data (For Setting and Retrieving Reminders). This is not required for an Activity Tracker.";

            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to access the user's Calendar Data (For Setting and Retrieving Reminders). This is required for an Event Booking app";

        }

        if (PermissionName == "Camera")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[2].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to access the device's Camera. This is required for a Communication app to make video calls.";

            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to access the device's Camera. This is required for clicking pictures for the blog using the app.";

            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to access the device's Camera. This is not required for an Event Booking app.";
        }

        if (PermissionName == "Contacts")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[3].GetComponent<Image>().sprite;

            if(CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to access the Contacts stored in the device. This is required for a Communication app's Funtionality.";

            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to access the Contacts stored in the device. This is not required for an Activity Tracker.";

            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to access the Contacts stored in the device. This is not required for an Event Booking App.";
        }

        if (PermissionName == "Location")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = true;
            PermissionImage.enabled = false;

            PermissionImage2.GetComponent<Image>().sprite = PermissionSprites[4].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to access the device's Location. This is required for sending your location to your contacts.";
            
            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to access the device's Location. This is required for calculating distance and for tracking your walking routes.";
            
            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to access the device's Location. This is required for finding events happening around you.";
        }

        if (PermissionName == "Mic")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = true;
            PermissionImage.enabled = false;

            PermissionImage2.GetComponent<Image>().sprite = PermissionSprites[5].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to access the device's Microphone (For Audio Recording and Transmission). This is required for video calls and sending audio snippets.";

            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to access the device's Microphone (For Audio Recording and Transmission). This is not required for an Activity Tracker.";

            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to access the device's Microphone (For Audio Recording and Transmission). This is not required for an Event Booking app.";

        }

        if (PermissionName == "Phone")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[6].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to make Phone Calls and retrieve Caller Id. This is required for a Communication app's Funtionality";
            
            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to make Phone Calls and retrieve Caller Id. This is not required for an Activity Tracker.";
            
            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to make Phone Calls and retrieve Caller Id. This is not required for an Event Booking app";
            
            PermissionImage2.enabled = false;
        }

        if (PermissionName == "Sensors")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[7].GetComponent<Image>().sprite;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Sensor permission is needed for accessing sensors like heart rate sensor to track the user's vitals. This is not needed for a Communication app's functionality.";

            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Sensor permission is needed for accessing sensors like heart rate sensor to track the user's vitals.";
            
            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Sensor permission is needed for accessing sensors like heart rate sensor to track the user's vitals. This is not needed for an Event Booking app.";

            PermissionImage2.enabled = false;
        }

        if (PermissionName == "SMS")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to read and write SMS. This is required for a Communication app's Funtionality";
            
            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to read and write SMS. This is not required for an Activity Tracker.";
            
            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to read and write SMS. This is not required for an Event Booking app.";
            
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[8].GetComponent<Image>().sprite;
            PermissionImage2.enabled = false;
        }

        if (PermissionName == "Storage")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;
            if (CurrentScene == "PostLevelQuiz")
                PermissionDescription.text = "Allows an app to read and write on the device's Storage. This is required to download files and send files to your contacts.";
            
            else if (CurrentScene == "PostLevelQuiz2")
                PermissionDescription.text = "Allows an app to read and write on the device's Storage. This is required to upload files to the fitness blog.";
            
            else if (CurrentScene == "PostLevelQuiz3")
                PermissionDescription.text = "Allows an app to read and write on the device's Storage. This is required to save Event Tickets on your phone.";
            
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[9].GetComponent<Image>().sprite;
            PermissionImage2.enabled = false;
        }
    }

    public void HidePanel()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
        }
    }

    IEnumerator UploadData() //https://ninest.vercel.app/html/google-forms-embed
    {
        WWWForm form = new WWWForm();
        WWWForm form2 = new WWWForm();
        WWWForm form3 = new WWWForm();

        form.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
        form.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
        form.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
        form.AddField("entry.1555203978", PlayerPrefs.GetInt("Level1PreWrong") + PreQuizWrongPerms);
        form.AddField("entry.1901484427", PlayerPrefs.GetInt("Level1PreCorrect"));
        form.AddField("entry.196385247", PlayerPrefs.GetInt("Level1PostWrong") + wrongPermsForLog);
        form.AddField("entry.285188838", PlayerPrefs.GetInt("Level1PostCorrect"));

        form2.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
        form2.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
        form2.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
        form2.AddField("entry.1388626478", PlayerPrefs.GetInt("Level1PreWrong") + PreQuizWrongPerms);
        form2.AddField("entry.888520948", PlayerPrefs.GetInt("Level1PreCorrect"));
        form2.AddField("entry.1833786694", PlayerPrefs.GetInt("Level1PostWrong") + wrongPermsForLog);
        form2.AddField("entry.1934431195", PlayerPrefs.GetInt("Level1PostCorrect"));

        form3.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
        form3.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
        form3.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
        form3.AddField("entry.808424731", PlayerPrefs.GetInt("Level1PreWrong") + PreQuizWrongPerms);
        form3.AddField("entry.131604988", PlayerPrefs.GetInt("Level1PreCorrect"));
        form3.AddField("entry.1983288352", PlayerPrefs.GetInt("Level1PostWrong") + wrongPermsForLog);
        form3.AddField("entry.2026619903", PlayerPrefs.GetInt("Level1PostCorrect"));

        if (CurrentScene == "PostLevelQuiz")
        {
            UnityWebRequest www = UnityWebRequest.Post(BaseURL, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StopCoroutine(UploadData());
            }
        }
        else if (CurrentScene == "PostLevelQuiz2")
        {
            UnityWebRequest www = UnityWebRequest.Post(BaseURL, form2);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StopCoroutine(UploadData());
            }
        }

        else if (CurrentScene == "PostLevelQuiz3")
        {
            UnityWebRequest www = UnityWebRequest.Post(BaseURL, form3);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StopCoroutine(UploadData());
            }
        }

        /*if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            StopCoroutine(UploadData());
        }*/
    }

    public void ShowAppFeatures()
    {
        AppFeaturesScreen.SetActive(true);
    }

    public void HideAppFeatures()
    {
        AppFeaturesScreen.SetActive(false);
    }

    public void HideNoSelectionPanel()
    {
        if (NoSelectionPanel.activeSelf)
            NoSelectionPanel.SetActive(false);
    }
}
