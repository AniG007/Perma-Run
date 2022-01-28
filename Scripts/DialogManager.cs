using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] Image dialogBox;

    [SerializeField] TextMeshProUGUI dialogText;
    /*[SerializeField] TextMeshProUGUI CloseButton;
    [SerializeField] TextMeshProUGUI InfoButton;*/

    [SerializeField] GameObject CloseButton;
    [SerializeField] GameObject InfoButton;

    [SerializeField] GameObject InfoScreen;
    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject PlayerControl;
    [SerializeField] GameObject PauseMenu;
    private Queue<string> sentences;

    /*private bool wasSuggestionShown;

    private float suggestionTimer;*/
    string Controller;
    [SerializeField] Canvas canvas;

    int PI;

    void Start()
    {
        sentences = new Queue<string>(); //Initialization

        canvas.enabled = false;
        CloseButton.SetActive(false);
        InfoButton.SetActive(false);

        Controller = PlayerPrefs.GetString("controller");

        PI = int.Parse(PlayerPrefs.GetString("PI", "0"));
    }

    /*private void Update()
    {
        if (wasSuggestionShown)
        {
            suggestionTimer += Time.deltaTime;
            if (suggestionTimer >= 5)
            {
                EndDialog();
            }
        }
    }*/

    public void startDialog(string sentence, string Colour)
    {

        sentences.Clear();

        canvas.enabled = true;
        
        /*CloseButton.enabled = true;
        InfoButton.enabled = true;*/

        if (Colour == "red")
            dialogText.color = Color.red;
        else
            dialogText.color = Color.green;

        /*wasSuggestionShown = true;
        suggestionTimer = 0;*/
        sentences.Enqueue(sentence);

        DisplayNextSentence();
        Time.timeScale = 0f;
    }

    public void DisplayNextSentence()
    {

        string s = sentences.Dequeue();

        if (sentences.Count == 0)
        {
            CloseButton.SetActive(true);
            InfoButton.SetActive(true);

            /*EndDialog();
            return;*/
        }

        StopAllCoroutines();
        StartCoroutine(AnimateDialog(s));
    }

    IEnumerator AnimateDialog (string sentence)
    {
        dialogText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;

            yield return null;
        }
    }

    public void EndDialog()
    {
        //dialogBox.GetComponent<Canvas>().enabled = false;        
        canvas.enabled = false;
        /*suggestionTimer = 0;
        wasSuggestionShown = false;*/
        CloseButton.SetActive(false);
        InfoButton.SetActive(false);

        if (Controller != "buttons")
        {
            Joystick.GetComponentInChildren<Image>().enabled = true;
            Joystick.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    public void ShowInfoScreen()
    {
        PI++;
        PlayerPrefs.SetString("PI", PI.ToString());

        if (Controller != "buttons")
        {
            Joystick.GetComponentInChildren<Image>().enabled = false;
            Joystick.SetActive(false);
        }

        if (PauseMenu.activeSelf == false)
            this.gameObject.SetActive(false);

        PlayerControl.SetActive(false);
        InfoScreen.SetActive(true);
    }

    public void HideInfoScreen()
    {
        /*if (Controller != "buttons")
        {
            Joystick.GetComponentInChildren<Image>().enabled = true;
            Joystick.SetActive(true);
        }*/

        if (PauseMenu.activeSelf == false)
        {
            this.gameObject.SetActive(true);
            PlayerControl.SetActive(true);
        }

        InfoScreen.SetActive(false);
    }
}
