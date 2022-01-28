using UnityEngine;

public class Coin : MonoBehaviour
{
    public Dialog dialog;

    string colour = "";

    [SerializeField] int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == "red")
            colour = "red";

        if (col.gameObject.CompareTag("Player") && !(gameObject.CompareTag("cherry") || gameObject.CompareTag("gem") || gameObject.CompareTag("Hidden")))
        {
            Score.instance.ChangeScore(coinValue);
            //Score.instance.ChangeScore(206);
            //FindObjectOfType<DialogManager>().startDialog(dialog.ReturnRandom());
            FindObjectOfType<DialogManager>().startDialog(dialog.ReturnSuggestion(this.gameObject.name, this.gameObject.tag), colour);
        }

        else if (col.gameObject.CompareTag("Player") && (gameObject.CompareTag("cherry") || gameObject.CompareTag("gem") || gameObject.CompareTag("Hidden")))
        {
            Score.instance.ChangeScore(coinValue);
        }
    }
}