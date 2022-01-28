using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermissionInfoScreen : MonoBehaviour
{
    [SerializeField] GameObject CloseButton;

    [SerializeField] TextMeshProUGUI PermissionDescription;
    [SerializeField] GameObject[] PermissionSprites = new GameObject[10];
    [SerializeField] Image PermissionImage;
    [SerializeField] GameObject Panel;
    [SerializeField] Image PermissionImage2;

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

            PermissionDescription.text = "Allows an app to make Phone Calls and retrieve Caller Id";
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

    public void HidePanel()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
        }
    }

    /*public void ClosePermissionInfoScreen()
    {
        this.gameObject.SetActive(false);
    }*/
}