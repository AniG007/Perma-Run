using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float speed = 3f;
    private bool isMovingUp;

    private void Start()
    {
        isMovingUp = true;
    }

    private void Update()
    {
        if(isMovingUp)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform"))
        {
            if (isMovingUp)
                isMovingUp = false;

            else
                isMovingUp = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Parenting the platform for the player so that the player does now keep falling down when the platform moves down or sideways and rather stays still on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //unparenting the platform for the player ps: unparenting is not a proper word
        collision.gameObject.transform.parent = null;
    }
}
