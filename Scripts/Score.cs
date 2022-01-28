using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score instance;
    public Text points;
    public int score = 0;

    const string privateCode = "private code";
    const string publicCode = "public code";

    const string webURL = "http://dreamlo.com/lb/";

    string badges = "";
    string hidden = "";
    string dataUpload = "";
    string Button_Press_Count = "";
    [SerializeField] RectTransform score_rect_transform;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }


        int prevLevel = PlayerPrefs.GetInt("PrevLevel");

        string ParticipantId = PlayerPrefs.GetString("ParticipantID", "000000");
        string EmailId = PlayerPrefs.GetString("EmailID", "@localhost");

        //Checking prevlevel to see if player is playing level by level to carry forward the score. - Check Gamecomplete script for more details
        if (prevLevel == 1)
        {
            score = int.Parse(PlayerPrefs.GetString("LevelScore"));
        }
    }

    public void ChangeScore(int coinValue)
    {
        string ParticipantId = PlayerPrefs.GetString("ParticipantID", "000000");
        string EmailId = PlayerPrefs.GetString("EmailID", "@localhost");

        score += coinValue;
        StartCoroutine(PlayScoreAnim());

        badges = FormatVariablesForBadges();
        hidden = FormatVariablesForHiddenArea();
        Button_Press_Count = FormatButtonPress();
        dataUpload = badges + "|" + hidden + "|" + Button_Press_Count + "|" + ParticipantId + "|" + EmailId;

        if (score >= 0)
        {
            points.text = score.ToString();
        }
        else
        {
            score = 0;
            points.text = score.ToString();
        }
        
        StopCoroutine(PlayScoreAnim());
        
        if(PlayerPrefs.GetString("PlayerScore","0") == "0")
        {
            PlayerPrefs.SetString("PlayerScore", score.ToString());
        }
        
        else if (int.Parse(PlayerPrefs.GetString("PlayerScore")) < score)
        {
            PlayerPrefs.SetString("PlayerScore", score.ToString());
            AddNewHighscore(PlayerPrefs.GetString("PlayerName", ""), score);
        }
    }

    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        //WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score + "/" + "1" + "/" + "0|Logs");
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score + "/" + "1" + "/" + dataUpload);
        yield return www;

        /*if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(badges);
        }
        else
        {
            print("Error uploading: " + www.error);
        }*/
    }

    IEnumerator PlayScoreAnim() //https://www.youtube.com/watch?v=z5CdXvbTQ2Q&t=49s
    {
        for (float i = 1f; i<=2f; i += 0.05f)
        {
            score_rect_transform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        score_rect_transform.localScale = new Vector3(2f, 2f, 2f);
        for (float i = 2f; i >= 1f; i -= 0.05f)
        {
            score_rect_transform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
    }

    string FormatVariablesForBadges()
    {
        string explorer = PlayerPrefs.GetInt("HA", 0).ToString();
        string scholar = PlayerPrefs.GetInt("IGQ", 0).ToString();
        string destroyer = PlayerPrefs.GetInt("D", 0).ToString();
        string geek = PlayerPrefs.GetInt("PQ", 0).ToString();

        return explorer + "|" + scholar+ "|" + destroyer+ "|" + geek;
    }

    string FormatVariablesForHiddenArea()
    {
        string h1 = PlayerPrefs.GetInt("l1h1", 0).ToString();
        string h2 = PlayerPrefs.GetInt("l1h2", 0).ToString();
        string h3 = PlayerPrefs.GetInt("l2h1", 0).ToString();
        string h4 = PlayerPrefs.GetInt("l2h2", 0).ToString();
        string h5 = PlayerPrefs.GetInt("l2h3", 0).ToString();
        string h6 = PlayerPrefs.GetInt("l3h1", 0).ToString();
        string h7 = PlayerPrefs.GetInt("l3h2",0).ToString();

        return h1 + "|" + h2 + "|" + h3 + "|" + h4 + "|" + h5 + "|" + h6 + "|" + h7;
    }

    public void UploadBadges() //for formatting and uploading data
    {
        int HighScore = int.Parse(PlayerPrefs.GetString("PlayerScore","0"));
        string PlayerName = PlayerPrefs.GetString("PlayerName", "");
        string ParticipantId = PlayerPrefs.GetString("ParticipantID", "000000");
        string EmailId = PlayerPrefs.GetString("EmailID", "@localhost");

        HighScore++;
        badges = FormatVariablesForBadges();
        hidden = FormatVariablesForHiddenArea();
        Button_Press_Count = FormatButtonPress();
        dataUpload = badges + "|" + hidden + "|" + Button_Press_Count + "|" + ParticipantId + "|" + EmailId;

        PlayerPrefs.SetString("PlayerScore", HighScore.ToString());
        AddNewHighscore(PlayerName, HighScore);
    }

    string FormatButtonPress()
    {
        string L1P = PlayerPrefs.GetString("L1P", "0");
        string L2P = PlayerPrefs.GetString("L2P", "0");
        string L3P = PlayerPrefs.GetString("L3P", "0");
        string PP = PlayerPrefs.GetString("PP", "0");
        string SP = PlayerPrefs.GetString("SP", "0");
        string LP = PlayerPrefs.GetString("LP", "0");
        string CP = PlayerPrefs.GetString("CP", "0");
        string AF = PlayerPrefs.GetString("AF", "0");
        string PI = PlayerPrefs.GetString("PI", "0");
        

        return L1P + "|" + L2P + "|" + L3P + "|" + PP + "|" + SP + "|" + LP + "|" + CP + "|" + AF + "|" + PI;
    }
}