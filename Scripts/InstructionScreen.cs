using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionScreen : MonoBehaviour
{
    [SerializeField] Image Screen1;
    /*[SerializeField] Image Screen2;
    [SerializeField] Image Screen3;
    [SerializeField] Image Screen4;
    [SerializeField] Image Screen5;
    [SerializeField] Image Screen6;
    [SerializeField] Image Screen7;
    [SerializeField] Image Screen8;*/
    [SerializeField] Image Screen9;
    [SerializeField] Image Black;

    [SerializeField] Sprite[] Instructions;

    [SerializeField] GameObject Back;
    [SerializeField] GameObject Play;
    [SerializeField] LevelLoader LevelLoader;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI GoodLuckText;

    int incrementor = 0;

    //An Image array would be a better approach

    private void Start()
    {
        Screen1.enabled = true;
        /*Screen2.enabled = false;
        Screen3.enabled = false;
        Screen4.enabled = false;
        Screen5.enabled = false;
        Screen6.enabled = false;
        Screen7.enabled = false;
        Screen8.enabled = false;*/
        Screen9.enabled = false;

        Black.enabled = false;
        LevelLoader.enabled = false;
        Title.enabled = false;
        GoodLuckText.enabled = false;

        Back.SetActive(false);
        Play.SetActive(false);

        Screen1.GetComponent<Image>().sprite = Instructions[0];
    }

    public void Next()
    {
        if(incrementor <13)
            incrementor++;

        if (incrementor == 13)
        {
            //Screen1.enabled = false;
            Black.enabled = true;
            Play.SetActive(true);
            Back.SetActive(true);
            GoodLuckText.enabled = true;
        }
        else
            Screen1.GetComponent<Image>().sprite = Instructions[incrementor];        

        
        /*if (Screen1.enabled == true)
        {
            Screen1.enabled = false;
            Screen2.enabled = true;
        }*/

        /*else if (Screen2.enabled == true)
        {
            Screen2.enabled = false;
            Screen3.enabled = true;
        }

        else if (Screen3.enabled == true)
        {
            Screen3.enabled = false;
            Screen4.enabled = true;
        }

        else if (Screen4.enabled == true)
        {
            Screen4.enabled = false;
            Screen5.enabled = true;
        }

        else if (Screen5.enabled == true)
        {
            Screen5.enabled = false;
            Screen6.enabled = true;
        }

        else if (Screen6.enabled == true)
        {
            Screen6.enabled = false;
            Screen7.enabled = true;
        }

        else if (Screen7.enabled == true)
        {
            Screen7.enabled = false;
            Screen8.enabled = true;
        }

        else if (Screen8.enabled == true)
        {
            Screen8.enabled = false;
            Screen9.enabled = true;
        }

        else if (Screen9.enabled == true)
        {
            Black.enabled = true;
            Play.SetActive(true);
            Back.SetActive(true);
        }*/

    }

    public void PlayGame()
    {
        Screen9.enabled = false;
        Black.enabled = false;
        Screen1.enabled = false;
        GoodLuckText.enabled = false;

        Back.SetActive(false);
        Play.SetActive(false);
        Title.enabled = true;
        PlayerPrefs.SetInt("FirstPlay", 0);
        LevelLoader.enabled = true;
        LevelLoader.LoadNextLevel("Traps");
    }

    public void BackToInstructions()
    {
        Screen1.enabled = true;
        /*Screen2.enabled = false;
        Screen3.enabled = false;
        Screen4.enabled = false;
        Screen5.enabled = false;
        Screen6.enabled = false;
        Screen7.enabled = false;
        Screen8.enabled = false;
        Screen9.enabled = false;*/

        Black.enabled = false;
        Title.enabled = false;
        Back.SetActive(false);
        Play.SetActive(false);
        GoodLuckText.enabled = false;

        Screen1.GetComponent<Image>().sprite = Instructions[0];
        incrementor = 0;
    }
}
