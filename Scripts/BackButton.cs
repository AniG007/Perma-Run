using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackButton : MonoBehaviour
{
    [SerializeField] Button back;
    [SerializeField] GameObject MainMenu;
    [SerializeField] TextMeshProUGUI Title;

    private void Awake()
    {
        back.onClick.AddListener(Back);
    }

    private void Back()
    {
        this.gameObject.SetActive(false);
        Title.enabled = true;
        MainMenu.SetActive(true);
    }
}
