using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PermissionNavigation : MonoBehaviour
{
    [SerializeField] GameObject Next;
    [SerializeField] GameObject LevelLoaderUI;
    [SerializeField] LevelLoader LevelLoader;
    [SerializeField] GameObject Screen1;
    [SerializeField] GameObject Screen2;
    [SerializeField] GameObject AppFeatures;
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

    [SerializeField] TextMeshProUGUI PermissionDescription;
    [SerializeField] GameObject[] PermissionSprites = new GameObject[10];
    [SerializeField] Image PermissionImage;
    [SerializeField] Image PermissionImage2;

    string CurrentScene;
    string wrongPermsForLog;

    int LevelOneCorrectAnswerCount = 0;
    int LevelOneWrongAnswerCount = 0;

    /*int LevelTwoCorrectAnswerCount = 0;
    int LevelTwoWrongAnswerCount = 0;

    int LevelThreeCorrectAnswerCount = 0;
    int LevelThreeWrongAnswerCount = 0;*/

    public void NavigateToInstructions()
    {
        Screen2.SetActive(false);
        LevelLoaderUI.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Permissions")
        {
            if (PlayerPrefs.GetInt("FirstPlay", 1) == 1)
                LevelLoader.LoadNextLevel("Instructions");
            
            else
                LevelLoader.LoadNextLevel("Traps");
        }

        else if (SceneManager.GetActiveScene().name == "Permissions2")
            LevelLoader.LoadNextLevel("Level2");

        else if (SceneManager.GetActiveScene().name == "Permissions3")
            LevelLoader.LoadNextLevel("Level3");
    }

    private void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        wrongPermsForLog = "";
        PlayerPrefs.SetString("PreQuizWrongPerms", "");
    }

    public void OnSubmitClicked()
    {
        if (MicToggle.isOn) {
            if (CurrentScene == "Permissions")
                LevelOneCorrectAnswerCount++;
            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " MIC ";
            }
        }
        
        if (LocationToggle.isOn) {
            //if (CurrentScene == "Permissions" || CurrentScene == "Permissions2")
                LevelOneCorrectAnswerCount++;
                //LevelOneWrongAnswerCount++;
        }

        if (StorageToggle.isOn) {
            //if (CurrentScene == "Permissions" || CurrentScene == "Permissions2")
                LevelOneCorrectAnswerCount++;
            
                //LevelOneWrongAnswerCount++;
        }

        if (ActivityToggle.isOn) {
            
            if(CurrentScene == "Permissions2")
                LevelOneCorrectAnswerCount++;

            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " AR ";
            }
        }

        if (CalendarToggle.isOn)
        {
            if (CurrentScene == "Permissions3")
                LevelOneCorrectAnswerCount++;
            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " CAL ";
            }
        }

        if (CameraToggle.isOn)
        {
            if (CurrentScene == "Permissions" || CurrentScene == "Permissions2")
                LevelOneCorrectAnswerCount++;

            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " CAM ";
            }
        }

        if (ContactsToggle.isOn)
        {
            if (CurrentScene == "Permissions")
                LevelOneCorrectAnswerCount++;
            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " CON ";
            }
        }

        if (PhoneToggle.isOn)
        {
            if (CurrentScene == "Permissions")
                LevelOneCorrectAnswerCount++;
            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " TP ";
            }
        }

        if (SensorsToggle.isOn)
        {
            if (CurrentScene == "Permissions2")
                LevelOneCorrectAnswerCount++;

            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " SEN ";
            }
        }

        if (SMSToggle.isOn)
        {
            if (CurrentScene == "Permissions")
                LevelOneCorrectAnswerCount++;
            else
            {
                LevelOneWrongAnswerCount++;
                wrongPermsForLog += " SMS ";
            }
        }

        if (!SMSToggle.isOn && !SensorsToggle.isOn && !PhoneToggle.isOn && !ContactsToggle.isOn && !CameraToggle.isOn && !CalendarToggle.isOn && !ActivityToggle.isOn && !StorageToggle.isOn && !LocationToggle.isOn && !MicToggle.isOn)
            NoSelectionPanel.SetActive(true);

        else if (SMSToggle.isOn || SensorsToggle.isOn || PhoneToggle.isOn || ContactsToggle.isOn || CameraToggle.isOn || CalendarToggle.isOn || ActivityToggle.isOn || StorageToggle.isOn || LocationToggle.isOn || MicToggle.isOn)
        {
            PlayerPrefs.SetInt("Level1PreCorrect", LevelOneCorrectAnswerCount);
            PlayerPrefs.SetInt("Level1PreWrong", LevelOneWrongAnswerCount);
            PlayerPrefs.SetString("PreQuizWrongPerms", wrongPermsForLog);
            NavigateToInstructions();
        }
    }

    public void NavigateToInitialQuestions()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);
        //LevelLoaderUI.SetActive(true);
    }

    public void Back()
    {
        Screen1.SetActive(true);
        Screen2.SetActive(false);
    }

    public void PermissionInfo(string PermissionName)
    {
        if (PermissionName == "ActivityRecognition")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to recognize Physical Activity (Eg: Walking, Running, Cycling etc.)";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[0].GetComponent<Image>().sprite;
        }

        if (PermissionName == "Calendar")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to access the user's Calendar Data (For setting and retrieving Reminders)";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[1].GetComponent<Image>().sprite;
        }

        if (PermissionName == "Camera")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to access the device's Camera (For taking pics and videos)";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[2].GetComponent<Image>().sprite;
        }

        if (PermissionName == "Contacts")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to access the Contacts on the device";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[3].GetComponent<Image>().sprite;
        }

        if (PermissionName == "Location")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = true;
            PermissionImage.enabled = false;

            PermissionDescription.text = "Allows an app to access the device's precise Location";
            PermissionImage2.GetComponent<Image>().sprite = PermissionSprites[4].GetComponent<Image>().sprite;
        }

        if (PermissionName == "Mic")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = true;
            PermissionImage.enabled = false;

            PermissionDescription.text = "Allows an app to access the device's Microphone (For Recording and Transmitting Audio)";
            PermissionImage2.GetComponent<Image>().sprite = PermissionSprites[5].GetComponent<Image>().sprite;
        }

        if (PermissionName == "Phone")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to make Phone Calls and retrieve Caller Id.";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[6].GetComponent<Image>().sprite;
            PermissionImage2.enabled = false;
        }

        if (PermissionName == "Sensors")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to access sensors like heart rate sensor to track the user's vitals.";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[7].GetComponent<Image>().sprite;
            PermissionImage2.enabled = false;
        }

        if (PermissionName == "SMS")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to read and write SMS";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[8].GetComponent<Image>().sprite;
            PermissionImage2.enabled = false;
        }

        if (PermissionName == "Storage")
        {
            Panel.SetActive(true);

            PermissionImage2.enabled = false;
            PermissionImage.enabled = true;

            PermissionDescription.text = "Allows an app to read and write on the device's Storage (For Downloading and Uploading data)";
            PermissionImage.GetComponent<Image>().sprite = PermissionSprites[9].GetComponent<Image>().sprite;
            PermissionImage2.enabled = false;
        }
    }

    public void ShowAppFeatures()
    {
        AppFeatures.SetActive(true);
    }

    public void HideAppFeatures()
    {
        AppFeatures.SetActive(false);
    }

    public void HidePanel()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
        }
    }

    public void HideNoSelectionPanel()
    {
        if (NoSelectionPanel.activeSelf)
            NoSelectionPanel.SetActive(false);
    }
}