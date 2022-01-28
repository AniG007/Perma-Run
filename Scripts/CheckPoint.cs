using System.Collections;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PositionKeeper pk;
    [SerializeField] GameObject text;

    private void Start()
    {
        pk = GameObject.FindObjectOfType<PositionKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            pk.lastCheckPointPosition = transform.position;
            StartCoroutine(displayCheckPointText());
        }
    }

    IEnumerator displayCheckPointText()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(1f);
        text.SetActive(false);
        StopCoroutine(displayCheckPointText());
    }
}
