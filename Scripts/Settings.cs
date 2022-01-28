using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] Button back;
    [SerializeField] Button Volume;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer mainMixer;
    [SerializeField] TextMeshProUGUI Title;    

    [SerializeField] ToggleGroup ControllerOptions;
    [SerializeField] Toggle JoyToggle;
    [SerializeField] Toggle ButtonToggle;

    [SerializeField] Sprite[] VolumeSprites;

    [SerializeField] AudioSource MenuMusic;
    [SerializeField] AudioSource ClickSound;

    // Update is called once per frame
    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        
        if (volumeSlider.value == 0.0001f)
            Volume.GetComponent<Image>().sprite = VolumeSprites[0];
        else
            Volume.GetComponent<Image>().sprite = VolumeSprites[1];
    }

    public void Back()
    {
        Title.enabled = true;
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void SetVolume(float VolumeLevel) 
    {   
        PlayerPrefs.SetFloat("masterVolume", VolumeLevel);
        mainMixer.SetFloat("volume", Mathf.Log10(VolumeLevel) * 20); //Ref: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/#playpref
        MenuMusic.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
        ClickSound.volume = PlayerPrefs.GetFloat("masterVolume", 5f);

        if (VolumeLevel == 0.0001f)
            Volume.GetComponent<Image>().sprite = VolumeSprites[0];
        else
            Volume.GetComponent<Image>().sprite = VolumeSprites[1];
    }

    public void Controller()
    {
        if (ButtonToggle.isOn)
        {
            PlayerPrefs.SetString("controller", "buttons");
        }
        else
        {
            PlayerPrefs.SetString("controller", "joystick");
        }
    }

    public void ToggleJoyStick()
    {
        JoyToggle.isOn = true;
        ButtonToggle.isOn = false;
    }

    public void ToggleButton()
    {
        JoyToggle.isOn = false;
        ButtonToggle.isOn = true;
    }

    public void MuteUnMute()
    {
        float CurrentVolume = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        float PrevVolume = PlayerPrefs.GetFloat("PrevVolume", 0.75f);
        
        if (CurrentVolume == 0.0001f)
        {
            PlayerPrefs.SetFloat("PrevVolume", CurrentVolume);
            PlayerPrefs.SetFloat("masterVolume", PrevVolume);
            mainMixer.SetFloat("volume", Mathf.Log10(PrevVolume) * 20); //Ref: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/#playpref
            volumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.75f);
            Volume.GetComponent<Image>().sprite = VolumeSprites[1];

            MenuMusic.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
            ClickSound.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
        }

        else
        {
            PlayerPrefs.SetFloat("PrevVolume", CurrentVolume);
            PlayerPrefs.SetFloat("masterVolume", 0.0001f);
            mainMixer.SetFloat("volume", Mathf.Log10(0.0001f) * 20); //Ref: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/#playpref
            volumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.75f);
            Volume.GetComponent<Image>().sprite = VolumeSprites[0];

            MenuMusic.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
            ClickSound.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
        }
        //Debug.Log(PlayerPrefs.GetFloat("masterVolume", 0.75f));
    }
}
