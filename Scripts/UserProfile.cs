using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameHolder;
    [SerializeField] TextMeshProUGUI ScoreHolder;
    [SerializeField] TextMeshProUGUI HiddenAreaHolder;

    [SerializeField] Sprite[] badges;

    [SerializeField] GameObject Panel;
    [SerializeField] GameObject PanelImage;

    [SerializeField] GameObject[] BadgeHolder;


    void Start()
    {
        NameHolder.text = PlayerPrefs.GetString("PlayerName");
        HiddenAreaHolder.text = PlayerPrefs.GetString("HiddenAreaCount", "0") + " / 7";
        ScoreHolder.text = PlayerPrefs.GetString("PlayerScore", "0");
        DisplayBadges();
    }

    public void BackToMenu()
    {
        if (Panel.activeSelf)
            Panel.SetActive(false);

        gameObject.SetActive(false);
    }

    public void DisplayBadgeDetails(string BadgeName)
    {
        Panel.SetActive(true);
        if (BadgeName == "Explorer")
        {
            PanelImage.GetComponent<Image>().sprite = badges[0];
            Panel.GetComponentInChildren<TextMeshProUGUI>().text = "Earn the Explorer badge by discovering at least 5 hidden areas in-game";
        }

        else if(BadgeName == "Scholar")
        {
            PanelImage.GetComponent<Image>().sprite = badges[1];
            Panel.GetComponentInChildren<TextMeshProUGUI>().text = "Earn the Scholar Badge by answering 8 in-game quizzes correctly in a single attempt!";
        }

        else if (BadgeName == "Destroyer")
        {
            PanelImage.GetComponent<Image>().sprite = badges[2];
            Panel.GetComponentInChildren<TextMeshProUGUI>().text = "Destroy atleast 25 enemies in-game to earn this badge.";
        }

        else if (BadgeName == "Geek")
        {
            PanelImage.GetComponent<Image>().sprite = badges[3];
            Panel.GetComponentInChildren<TextMeshProUGUI>().text = "Earn the Geek badge by answering all the quizzes correctly after each level";
        }
    }

    public void HideBadgeDetails()
    {
        Panel.SetActive(false);
    }

    void DisplayBadges()
    {
        int hiddenAreas = int.Parse(PlayerPrefs.GetString("HiddenAreaCount", "0"));
        int quiz = int.Parse(PlayerPrefs.GetString("igQuiz", "0"));
        int destroyer = int.Parse(PlayerPrefs.GetString("destroyer", "0"));
        int postquiz = int.Parse(PlayerPrefs.GetString("postQuiz", "0"));

        /*Debug.Log(hiddenAreas);
        Debug.Log(quiz);
        Debug.Log(destroyer);
        Debug.Log(postquiz);*/

        if (hiddenAreas >= 5)
            BadgeHolder[0].GetComponent<Image>().sprite = badges[0];

        if (quiz >= 8)
            BadgeHolder[1].GetComponent<Image>().sprite = badges[1];

        if (destroyer >= 25)
            BadgeHolder[2].GetComponent<Image>().sprite = badges[2];

        if (postquiz == 3)
            BadgeHolder[3].GetComponent<Image>().sprite = badges[3];
    }
}
