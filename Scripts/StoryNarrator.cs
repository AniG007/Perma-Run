using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryNarrator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] Image dialogBox;
    [SerializeField] LevelLoader LevelLoader;
    [SerializeField] GameObject LevelLoaderUI;
    [SerializeField] GameObject Next;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Skip;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI StoryGraphicNameHolder;

    [SerializeField] AudioSource Story_BG_Music;

    [TextArea(3, 10)]
    [SerializeField] string[] StorySentences1;
    
    [TextArea(3, 10)]
    [SerializeField] string[] StorySentences2;

    [TextArea(3, 10)]
    [SerializeField] string[] StorySentences3;

    [TextArea(3, 10)]
    [SerializeField] string[] StoryEndSentences;

    private Queue<string> sentences;

    private string Level;

    //[SerializeField] Canvas canvas;


    void Start()
    {
        LevelLoaderUI.SetActive(false);
        sentences = new Queue<string>(); //Initialization
        Level = SceneManager.GetActiveScene().name;

        if (PlayerPrefs.GetInt("StorySkip1", 1) == 1 && Level == "Story")
            Skip.SetActive(false);
        else if (PlayerPrefs.GetInt("StorySkip2", 1) == 1 && Level == "Story2")
            Skip.SetActive(false);
        else if (PlayerPrefs.GetInt("StorySkip3", 1) == 1 && Level == "Story3")
            Skip.SetActive(false);
        else if (PlayerPrefs.GetInt("StorySkipEnd", 1) == 1 && Level == "StoryEnd")
            Skip.SetActive(false);
        else
            Skip.SetActive(true);

        if (Level == "Story" || Level == "Story2" || Level == "Story3")
            Menu.SetActive(true);
        else
            Menu.SetActive(false);

        NarrateStory(Level);

        Story_BG_Music.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
        Story_BG_Music.Play();
    }

    public void startDialog(string[] StorySentences1)
    {
        sentences.Clear();
        
        foreach(string sentence in StorySentences1) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string s = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(AnimateDialog(s));
    }

    IEnumerator AnimateDialog(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {   
        dialogBox.enabled = false;
        dialogText.enabled = false;
        Next.SetActive(false);
        Menu.SetActive(false);
        StoryGraphicNameHolder.enabled = false;
        LevelLoaderUI.SetActive(true);
        Skip.SetActive(false);

        //LevelLoader.LoadNextLevel("Instructions");

        if (Level == "Story")
        {
            PlayerPrefs.SetInt("StorySkip1", 0);
            LevelLoader.LoadNextLevel("Permissions");
        }
        else if (Level == "Story2")
        {
            PlayerPrefs.SetInt("StorySkip2", 0);
            LevelLoader.LoadNextLevel("Permissions2");
        }
        else if (Level == "Story3")
        {
            PlayerPrefs.SetInt("StorySkip3", 0);
            LevelLoader.LoadNextLevel("Permissions3");
        }
        else if (Level == "StoryEnd")
        {
            PlayerPrefs.SetInt("StorySkipEnd", 0);
            LevelLoader.LoadNextLevel("MainMenu");
        }
    }

    public void NarrateStory(string Level)
    {
        switch (Level)
        {
            case "Story":
                startDialog(StorySentences1);
                break;
            case "Story2": startDialog(StorySentences2);
                break;
            case "Story3":
                startDialog(StorySentences3);
                break;
            case "StoryEnd":
                startDialog(StoryEndSentences);
                break;
            default: Debug.Log("None");
                break;
        }
        //startDialog(StorySentences1);
    }

    public void MainMenu()
    {
        dialogBox.enabled = false;
        dialogText.enabled = false;
        Next.SetActive(false);
        Menu.SetActive(false);
        StoryGraphicNameHolder.enabled = false;
        Skip.SetActive(false);
        LevelLoaderUI.SetActive(true);
        LevelLoader.LoadNextLevel("MainMenu");
    }

    public void SkipStory()
    {
        dialogBox.enabled = false;
        dialogText.enabled = false;
        Next.SetActive(false);
        Menu.SetActive(false);
        StoryGraphicNameHolder.enabled = false;
        LevelLoaderUI.SetActive(true);
        Skip.SetActive(false);

        //LevelLoader.LoadNextLevel("Instructions");

        if (Level == "Story")
            LevelLoader.LoadNextLevel("Permissions");

        else if (Level == "Story2")
            LevelLoader.LoadNextLevel("Permissions2");

        else if (Level == "Story3")
            LevelLoader.LoadNextLevel("Permissions3");

        else if (Level == "StoryEnd")
            LevelLoader.LoadNextLevel("MainMenu");

    }
}
