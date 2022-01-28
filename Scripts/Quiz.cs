using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class Quiz : MonoBehaviour
{
    public PlayerMovement player_instance;

    [SerializeField] Button btn1;
    [SerializeField] Button btn2;
    [SerializeField] Button btn3;
    [SerializeField] Button btnContinue;
    [SerializeField] Button btnClose;
    [SerializeField] Button btnTryAgain;
    [SerializeField] Button btnAnswer;

    [SerializeField] TextMeshProUGUI btn1TMP;
    [SerializeField] TextMeshProUGUI btn2TMP;
    [SerializeField] TextMeshProUGUI btn3TMP;
    [SerializeField] TextMeshProUGUI ans1TMP;
    [SerializeField] TextMeshProUGUI ans2TMP;
    [SerializeField] TextMeshProUGUI ans3TMP;
    [SerializeField] TextMeshProUGUI btnAnswerTMP;
    [SerializeField] TextMeshProUGUI Question;

    [SerializeField] GameObject CanvasForQuiz;
    [SerializeField] GameObject CanvasForPlayerControl;
    [SerializeField] GameObject CanvasForLevelEnd;
    [SerializeField] GameObject CanvasForPauseMenu;
    [SerializeField] GameObject CanvasForExitMenu;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] LevelLoader levelLoader;

    [SerializeField] HealthBar healthBar;

    [SerializeField] GameObject Joystick;

    [SerializeField] GameObject player;

    [SerializeField] GameObject []Character;
    List<Quiz_Data_Class> quiz = new List<Quiz_Data_Class>(3);

    //private string BaseURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd3RaFW4FUiGPNrIljNBFyZRuM8MUVQlyilln7LSOUqKqyddQ/formResponse";
    private string BaseURL = "https://docs.google.com/forms/d/e/1FAIpQLSfswUU60z9pAJgleFKOuCNd53Zfpm74946NUiFwgO6Cjc-e1w/formResponse";

    string []arr = new string[3];

    //These strings are assigned at various functions instead of awake or start because the script is used throught the game on multiple
    // game objects and these need to be changed everytime when those game objects are encountered. Start did not work as expected, hence this.
    string CharacterName = "";
    string Level = "";
    string ControllerType;
    string ans1 = "";
    string ans2 = "";
    string ans3 = "";

    int igquiz;
    int FirstAttempt = 1;
    int index;

    int attempt = 0; //for logging quiz attempt

    Quiz_QA instance = new Quiz_QA();

    public void DisplayQuizCanvas()
    {
        CharacterName = FindObjectOfType<PlayerMovement>().QuizCharacter;
        Level = SceneManager.GetActiveScene().name;
        ControllerType = PlayerPrefs.GetString("controller");

        quiz = instance.ReturnOptions(CharacterName, Level);

        index = Random.Range(0, quiz.Count);
        btn1TMP.text = quiz[index].option;
        ans1TMP.text = quiz[index].answer;
        quiz.RemoveAt(index);

        index = Random.Range(0, quiz.Count);
        btn2TMP.text = quiz[index].option;
        ans2TMP.text = quiz[index].answer;
        quiz.RemoveAt(index);

        index = Random.Range(0, quiz.Count);
        btn3TMP.text = quiz[index].option;
        ans3TMP.text = quiz[index].answer;
        quiz.RemoveAt(index);

        Question.text = instance.ReturnQuestion(CharacterName, Level);

        CanvasForQuiz.SetActive(true);
        CanvasForPlayerControl.SetActive(false);
        CanvasForLevelEnd.SetActive(false);
        CanvasForPauseMenu.SetActive(false);

        btnTryAgain.gameObject.SetActive(false);
        btnAnswer.gameObject.SetActive(false);
        btnClose.gameObject.SetActive(false);
        btnContinue.gameObject.SetActive(false);

        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(true);

        if (ControllerType == "joystick")
        {
            Joystick.GetComponentInChildren<Image>().enabled = false;
            Joystick.SetActive(false);
        }

        healthBar.setHealth(player.GetComponent<PlayerMovement>().CheckHealth());
    }

    public void onButton1Click()
    {
        attempt++;

        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        btn3.gameObject.SetActive(false);

        btnAnswer.gameObject.SetActive(true);
        btnAnswer.enabled = false;
        ans1 = ans1TMP.text;
        
        CharacterName = FindObjectOfType<PlayerMovement>().QuizCharacter;
        Level = SceneManager.GetActiveScene().name;

        Question.text = instance.ReturnReply(CharacterName, ans1);

        igquiz = int.Parse(PlayerPrefs.GetString("igQuiz", "0"));

        if(ans1 == "c")
        {
            if (FirstAttempt == 1)
            {
                if (igquiz < 8)
                {
                    if (CharacterName == "Bee" && PlayerPrefs.GetInt("l1q1", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l1q1", 1);
                    }

                    else if (CharacterName == "Fairy" && PlayerPrefs.GetInt("l1q4", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l1q4", 1);
                    }

                    else if (CharacterName == "Pink" && PlayerPrefs.GetInt("l3q1", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l3q1", 1);
                    }

                    else if (CharacterName == "Totem" && PlayerPrefs.GetInt("l3q2", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l3q2", 1);
                    }

                    if (igquiz == 8)
                        PlayerPrefs.SetInt("IGQ", 1);
                }
            }

            btnAnswerTMP.color = new Color32(5, 145, 0, 255);
            btnAnswerTMP.text = btn1TMP.text;
            btnContinue.gameObject.SetActive(true);
            FirstAttempt = 1;

            StartCoroutine(UploadData(CharacterName));
        }

        else
        {
            player.GetComponent<PlayerMovement>().TakeDamage(15);
            healthBar.setHealth(player.GetComponent<PlayerMovement>().CheckHealth());

            FirstAttempt = 0;
            btnAnswerTMP.color = Color.red;
            btnAnswerTMP.text = btn1TMP.text;
            btnTryAgain.gameObject.SetActive(true);
        }
    }

    public void onButton2Click()
    {
        attempt++;

        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        btn3.gameObject.SetActive(false);
        
        btnAnswer.gameObject.SetActive(true);
        btnAnswer.enabled = false;
        
        CharacterName = FindObjectOfType<PlayerMovement>().QuizCharacter;
        Level = SceneManager.GetActiveScene().name;

        ans2 = ans2TMP.text;
        Question.text = instance.ReturnReply(CharacterName, ans2);

        igquiz = int.Parse(PlayerPrefs.GetString("igQuiz", "0"));

        if (ans2 == "c")
        {
            if (FirstAttempt == 1)
            {
                if (igquiz < 8)
                {
                    if (CharacterName == "Rabbit" && PlayerPrefs.GetInt("l1q2", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l1q2", 1);
                    }
                    else if (CharacterName == "Fox" && PlayerPrefs.GetInt("l2q2", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l2q2", 1);
                    }

                    else if (CharacterName == "Squirrel" && PlayerPrefs.GetInt("l2q3", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l2q3", 1);
                    }

                    else if (CharacterName == "Frog" && PlayerPrefs.GetInt("l3q4", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l3q4", 1);
                    }

                    if (igquiz == 8)
                        PlayerPrefs.SetInt("IGQ", 1);
                }
            }

            btnAnswerTMP.color = new Color32(5, 145, 0, 255);
            btnAnswerTMP.text = btn2TMP.text;
            btnContinue.gameObject.SetActive(true);
            FirstAttempt = 1;

            StartCoroutine(UploadData(CharacterName));
        }

        else
        {
            player.GetComponent<PlayerMovement>().TakeDamage(15);
            healthBar.setHealth(player.GetComponent<PlayerMovement>().CheckHealth());

            FirstAttempt = 0;
            btnAnswerTMP.color = Color.red;
            btnAnswerTMP.text = btn2TMP.text;
            btnTryAgain.gameObject.SetActive(true);
        }
    }

    public void onButton3Click()
    {
        attempt++;

        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        btn3.gameObject.SetActive(false);
        
        btnAnswer.gameObject.SetActive(true);
        btnAnswer.enabled = false;

        CharacterName = FindObjectOfType<PlayerMovement>().QuizCharacter;
        Level = SceneManager.GetActiveScene().name;

        
        ans3 = ans3TMP.text;
        Question.text = instance.ReturnReply(CharacterName, ans3);

        igquiz = int.Parse(PlayerPrefs.GetString("igQuiz", "0"));

        
        if (ans3 == "c")
        {
            if (FirstAttempt == 1)
            {
                if (igquiz < 8)
                {
                    if (CharacterName == "GrassHopper" && PlayerPrefs.GetInt("l1q3", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l1q3", 1);
                    }

                    else if (CharacterName == "Hunter" && PlayerPrefs.GetInt("l2q1", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l2q1", 1);
                    }

                    else if (CharacterName == "Ant" && PlayerPrefs.GetInt("l2q4", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l2q4", 1);
                    }

                    else if (CharacterName == "Astronaut" && PlayerPrefs.GetInt("l3q3", 0) == 0)
                    {
                        igquiz += 1;
                        PlayerPrefs.SetString("igQuiz", igquiz.ToString());
                        PlayerPrefs.SetInt("l3q3", 1);
                    }

                    if (igquiz == 8)
                        PlayerPrefs.SetInt("IGQ", 1);
                }
            }

            btnAnswerTMP.color = new Color32(5, 145, 0, 255);
            btnAnswerTMP.text = btn3TMP.text;
            btnContinue.gameObject.SetActive(true);
            FirstAttempt = 1;

            StartCoroutine(UploadData(CharacterName));
        }

        else
        {
            player.GetComponent<PlayerMovement>().TakeDamage(15);
            healthBar.setHealth(player.GetComponent<PlayerMovement>().CheckHealth());
            
            FirstAttempt = 0;
            btnAnswerTMP.color = Color.red;
            btnAnswerTMP.text = btn3TMP.text;
            btnTryAgain.gameObject.SetActive(true);
        }
    }

    public void CloseQuizDialog()
    {
        ControllerType = PlayerPrefs.GetString("controller");

        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(true);

        btnAnswer.gameObject.SetActive(false);
        btnClose.gameObject.SetActive(false);

        btnAnswerTMP.color = new Color32(50, 50, 50, 255);
        Question.color = Color.white;

        CanvasForQuiz.SetActive(false);
        CanvasForPlayerControl.SetActive(true);
        CanvasForLevelEnd.SetActive(true);
        CanvasForPauseMenu.SetActive(true);

        if (ControllerType == "joystick")
        {
            Joystick.GetComponentInChildren<Image>().enabled = true;
            Joystick.SetActive(true);
        }

        attempt = 0;

        Time.timeScale = 1f;
        RemoveCollider(CharacterName);
    }

    public void TryAgain()
    {
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(true);

        btnAnswer.gameObject.SetActive(false);
        
        btnTryAgain.gameObject.SetActive(false);

        CharacterName = FindObjectOfType<PlayerMovement>().QuizCharacter;
        Level = SceneManager.GetActiveScene().name;

        btn2TMP.color = new Color32(50,50,50,255);

        Question.text = instance.ReturnQuestion(CharacterName, Level);
        /*btn1TMP.text = instance.ReturnOption1(CharacterName, Level);
        btn2TMP.text = instance.ReturnOption2(CharacterName, Level);
        btn3TMP.text = instance.ReturnOption3(CharacterName, Level);*/
    }

    public void ShowSecurityTip()
    {
        btnContinue.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        btnAnswer.gameObject.SetActive(false);
        btnClose.gameObject.SetActive(true);

        CharacterName = FindObjectOfType<PlayerMovement>().QuizCharacter;
        Level = SceneManager.GetActiveScene().name;

        //btn2TMP.text = "<-- Tip Unlocked!";
        Question.color = Color.green;
        Question.text = instance.ReturnPromotionFocus(CharacterName, Level);
    }

    private void RemoveCollider(string CharacterName)
    {
        if(CharacterName == "Bee" || CharacterName == "Hunter" || CharacterName == "Pink")
        {
            Character[0].SetActive(false);
            Score.instance.ChangeScore(10);
        }

        else if (CharacterName == "Rabbit" || CharacterName == "Fox" || CharacterName == "Totem")
        {
            Character[1].SetActive(false);
            Score.instance.ChangeScore(10);
        }

        else if (CharacterName == "GrassHopper" || CharacterName == "Squirrel" || CharacterName == "Astronaut")
        {
            Character[2].SetActive(false);
            Score.instance.ChangeScore(10);
        }

        else if (CharacterName == "Fairy" || CharacterName == "Ant" || CharacterName == "Frog")
        {
            Character[3].SetActive(false);
            Score.instance.ChangeScore(10);
        }
    }

    IEnumerator UploadData(string Character) //https://ninest.vercel.app/html/google-forms-embed
    {
        WWWForm form = new WWWForm();

        string LevelPlayNumber = "";
        string QuizData = "";
        string health = player.GetComponent<PlayerMovement>().CheckHealth().ToString();

        if (SceneManager.GetActiveScene().name == "Traps")
            LevelPlayNumber = PlayerPrefs.GetString("L1P", "1");
        

        else if (SceneManager.GetActiveScene().name == "Level2")
            LevelPlayNumber = PlayerPrefs.GetString("L2P", "1");

        else if (SceneManager.GetActiveScene().name == "Level3")
            LevelPlayNumber = PlayerPrefs.GetString("L3P", "1");

        QuizData = Character + " " + LevelPlayNumber + " " + attempt.ToString() + " " + health;

        form.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
        form.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
        form.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
        form.AddField("entry.1133351998", QuizData);

        UnityWebRequest www = UnityWebRequest.Post(BaseURL, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    public void DisplayQuitGameDialog()
    {
        CanvasForExitMenu.SetActive(true);
    }

    public void QuitGame(string option)
    {
        if (option == "YES")
        {
            CanvasForExitMenu.SetActive(false);
            LoadingScreen.SetActive(true);
            Time.timeScale = 1f;
            //SceneManager.LoadScene("MainMenu");
            levelLoader.LoadNextLevel("MainMenu");
        }
        else
        {
            CanvasForExitMenu.SetActive(false);
            PauseMenuUI.SetActive(true);
        }
    }
}