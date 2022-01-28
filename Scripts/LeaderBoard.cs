using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] Button back;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject LeaderBoardScreen;
    [SerializeField] TextMeshProUGUI Title;

    [SerializeField] TextMeshProUGUI[] PlayerNames;
    [SerializeField] TextMeshProUGUI[] PlayerScores;

    [SerializeField] Image[] Badges;
    [SerializeField] Sprite[] BadgeSprites;

    const string privateCode = "private code";
    const string publicCode = "public code";

    const string webURL = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;
    public static LeaderBoard instance;

    string badges = "";

    private void Awake()
    {
        //highscoreDisplay = GetComponent<DisplayHighscores>();
        instance = this;
    }

    private void Start()
    {
        string PlayerName = PlayerPrefs.GetString("PlayerName", "");
        int PlayerScore = int.Parse(PlayerPrefs.GetString("PlayerScore", "0"));
        badges = FormatVariablesForBadges();

        for (int i = 0; i < PlayerNames.Length; i++)
        {
            PlayerNames[i].text = "Fetching...";
        }

        //AddNewHighscore(PlayerName, PlayerScore);
        DownloadHighscores();
    }

    private void AddNewHighscore(string username, int score)
    {
        StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        //WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score + "/" + "1" + "/" + badges);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHighscores();
        }
        /*else
        {
            print("Error uploading: " + www.error);
        }*/
    }

    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            DisplayHighscores(www.text);
            
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }

    void DisplayHighscores(string data)
    {
        string[] entries = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        if (entries.Length != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                int explorer = int.Parse(entryInfo[3]);
                int scholar = int.Parse(entryInfo[4]);
                int destroyer = int.Parse(entryInfo[5]);
                int geek = int.Parse(entryInfo[6]);
                highscoresList[i] = new Highscore(username, score, explorer, scholar, destroyer, geek);
                PlayerNames[i].text = highscoresList[i].username;
                PlayerScores[i].text = highscoresList[i].score.ToString();

                if ((i + 1) == entries.Length && i < 4)
                {
                    i++;
                    while (i < 5)
                    {
                        PlayerNames[i].text = "";
                        PlayerScores[i].text = "";
                        i++;
                    }
                }
            }
            HighlightPlayer(highscoresList);
            ShowBadgesForPlayer(highscoresList);
        }
    }

    /*public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreList.Length; i++)
        {
            highscoreList[i].text = i + 1 + ". ";
            if (i < highscoreList.Length)
            {
                highscoresList[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }*/

    public void Back()
    {
        Title.enabled = true;
        LeaderBoardScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    public struct Highscore
    {
        public string username;
        public int score;
        public int explorer;
        public int scholar;
        public int destroyer;
        public int geek;

        public Highscore(string _username, int _score, int _explorer, int _scholar, int _destroyer, int _geek)
        {
            username = _username;
            score = _score;
            explorer = _explorer;
            scholar = _scholar;
            destroyer = _destroyer;
            geek = _geek;
        }
    }

    private void HighlightPlayer(Highscore[] scoreList)
    {
        string PlayerName = PlayerPrefs.GetString("PlayerName", "");

        for (int i = 0; i < scoreList.Length; i++)
        {
            if(PlayerName == scoreList[i].username)
            {
                PlayerNames[i].color = Color.green;
                PlayerScores[i].color = Color.green;
            }
        }
    }

    private void ShowBadgesForPlayer(Highscore[] scoreList) //There must be a better way to do this. Unity does not serialize 2-D arrays :') 
    {
        for(int i = 0; i< scoreList.Length; i++)
        {
            /*Debug.Log(scoreList[i].username);
            Debug.Log(scoreList[i].explorer);
            Debug.Log(scoreList[i].scholar);
            Debug.Log(scoreList[i].destroyer);
            Debug.Log(scoreList[i].geek);   */

            int explorer = scoreList[i].explorer;
            int scholar = scoreList[i].scholar;
            int destroyer = scoreList[i].destroyer;
            int geek = scoreList[i].geek;
                        
            if(i == 0)
            {
                if (explorer == 1)
                {
                    Badges[0].sprite = BadgeSprites[0];
                }
                if (scholar == 1)
                {
                    Badges[1].sprite = BadgeSprites[1];
                }
                if (destroyer == 1)
                {
                    Badges[2].sprite = BadgeSprites[2];
                }
                if (geek == 1)
                {
                    Badges[3].sprite = BadgeSprites[3];
                }
            }

            else if (i == 1)
            {
                if (explorer == 1)
                {
                    Badges[4].sprite = BadgeSprites[0];
                }
                if (scholar == 1)
                {
                    Badges[5].sprite = BadgeSprites[1];
                }
                if (destroyer == 1)
                {
                    Badges[6].sprite = BadgeSprites[2];
                }
                if (geek == 1)
                {
                    Badges[7].sprite = BadgeSprites[3];
                }
            }

            else if (i == 2)
            {
                if (explorer == 1)
                {
                    Badges[8].sprite = BadgeSprites[0];
                }
                if (scholar == 1)
                {
                    Badges[9].sprite = BadgeSprites[1];
                }
                if (destroyer == 1)
                {
                    Badges[10].sprite = BadgeSprites[2];
                }
                if (geek == 1)
                {
                    Badges[11].sprite = BadgeSprites[3];
                }
            }

            else if (i == 3)
            {
                if (explorer == 1)
                {
                    Badges[12].sprite = BadgeSprites[0];
                }
                if (scholar == 1)
                {
                    Badges[13].sprite = BadgeSprites[1];
                }
                if (destroyer == 1)
                {
                    Badges[14].sprite = BadgeSprites[2];
                }
                if (geek == 1)
                {
                    Badges[15].sprite = BadgeSprites[3];
                }
            }

            else if (i == 4)
            {
                if (explorer == 1)
                {
                    Badges[16].sprite = BadgeSprites[0];
                }
                if (scholar == 1)
                {
                    Badges[17].sprite = BadgeSprites[1];
                }
                if (destroyer == 1)
                {
                    Badges[18].sprite = BadgeSprites[2];
                }
                if (geek == 1)
                {
                    Badges[19].sprite = BadgeSprites[3];
                }
            }
        }
    }

    string FormatVariablesForBadges()
    {
        string explorer = PlayerPrefs.GetInt("HA", 0).ToString();
        string scholar = PlayerPrefs.GetInt("IGQ", 0).ToString();
        string destroyer = PlayerPrefs.GetInt("D", 0).ToString();
        string geek = PlayerPrefs.GetInt("PQ", 0).ToString();

        return explorer + "|" + scholar + "|" + destroyer + "|" + geek;
    }
}