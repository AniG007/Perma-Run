using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForInternet : MonoBehaviour
{

    [SerializeField] GameObject InternetDialog;
    [SerializeField] GameObject ExitGameDialog;
    [SerializeField] GameObject MainMenu;

    private void Start()
    {
        CheckInternet();   
    }

    public void DisplayExitGameDialog()
    {
        ExitGameDialog.SetActive(true);
    }

    public void CheckInternet()
    {
        StartCoroutine(checkConnection((isConnected) =>  //https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
        {
            if (isConnected)
            {
                Time.timeScale = 1f;
                InternetDialog.SetActive(false);
                MainMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 0f;
                InternetDialog.SetActive(true);
                MainMenu.SetActive(false);
            }
        }));
    }

    IEnumerator checkConnection(Action<bool> action) //https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
    {
        WWW www = new WWW("https://www.google.com/");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    public void QuitGame(string option)
    {
        if (option == "YES")
        {
            Application.Quit();
        }
        else
            ExitGameDialog.SetActive(false);
    }

}
