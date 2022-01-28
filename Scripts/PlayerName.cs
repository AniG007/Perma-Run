using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class PlayerName : MonoBehaviour
{
    [SerializeField] Button ButtonNext;
    [SerializeField] Button ButtonNext2;

    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject PlayerNameScreen;
    [SerializeField] GameObject EmailIdParticipantIdScreen;
    [SerializeField] TMP_InputField PlayerNameText;
    [SerializeField] TMP_InputField EmailId;
    [SerializeField] TMP_InputField ParticipantId;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] TextMeshProUGUI Title;
    int numberCheck;

    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$"; //https://gist.github.com/randhirraj93/42854e56892e672e849e51f726352433 for reference
    
    List<string> EmailIdList = new List<string>();
    List<string> ParticipantIdList = new List<string>();

    private void Start()
    {

        AddEmailsToList();
        AddParticipantIdsToList();
    }
    public void LoadStory()
    {

        /*if (!EmailIdList.Contains(EmailId.text))  // || !ParticipantIdList.Contains(ParticipantId.text))
            ShowToast("Please enter the email-id that you used in the survey");*/
        //ShowToast("Please check the EmailID/ParticipantID");

        /*else if (validateEmail(EmailId.text) == false)
            ShowToast("Please enter a valid email-id");*/
        
        if (EmailId.text == "")
            ShowToast("Please enter a valid email-id");

        /*else if (ParticipantId.text.Length < 6 || ParticipantId.text.Length > 6)
            ShowToast("Enter your 6 digit Participant Id");
        else if (int.TryParse(ParticipantId.text, out numberCheck) == false)
            ShowToast("Enter your 6 digit Participant Id");
        else if (ParticipantId.text.Contains(" "))
            ShowToast("Name should not contain space");
        else if (ParticipantId.text.Contains("*"))
            ShowToast("Name should not contain *");
        else if (ParticipantId.text == "")
            ShowToast("Parcipant Id should no be empty");*/

        else
        {
            //PlayerPrefs.SetString("ParticipantID", ParticipantId.text);
            PlayerPrefs.SetString("ParticipantID", "000000");
            PlayerPrefs.SetString("EmailID", EmailId.text);
            EmailIdParticipantIdScreen.SetActive(false);

            Title.enabled = true;
            LoadingScreen.SetActive(true);
            levelLoader.LoadNextLevel("Story");
        }
    }

    public void NavigateToEmailParticipantId()
    {
        if (PlayerNameText.text.Length > 12)
            ShowToast("Nickname should not be longer than 12 characters");
        else if (PlayerNameText.text.Contains("*"))
            ShowToast("Nickname should not contain * character");
        else if (PlayerNameText.text.Contains(" "))
            ShowToast("Nickname should not contain Space");
        else if (PlayerNameText.text == "")
            ShowToast("Please enter your Nickname to Continue");
        else
        {
            PlayerPrefs.SetString("PlayerName", PlayerNameText.text);
            PlayerNameScreen.SetActive(false);
            EmailIdParticipantIdScreen.SetActive(true);
            /*Title.enabled = true;
            LoadingScreen.SetActive(true);
            levelLoader.LoadNextLevel("Story");*/
        }
    }

    void ShowToast(string Level_Toast)
    {
        //https://agrawalsuneet.github.io/blogs/native-android-in-unity/ for ref

        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
        object[] toastParams = new object[3];

        AndroidJavaClass unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        toastParams[0] = unityActivity.GetStatic<AndroidJavaObject>("currentActivity");
        toastParams[1] = Level_Toast;
        toastParams[2] = toastClass.GetStatic<int>("LENGTH_LONG");

        AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", toastParams);

        toastObject.Call("show");
    }

    public static bool validateEmail(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }

    void AddEmailsToList()
    {
        EmailIdList.Add("devtesting40@gmail.com");
        EmailIdList.Add("40@gmail.com");
        EmailIdList.Add("meow@mailinator.as");
    }

    void AddParticipantIdsToList()
    {
        ParticipantIdList.Add("123456");
        ParticipantIdList.Add("654321");
        ParticipantIdList.Add("654322");
        ParticipantIdList.Add("438906");
    }
}
