using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    private bool isMovingLeft;

    private void Start()
    {
        isMovingLeft = true;
    }

    private void Update()
    {
        if (isMovingLeft)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform"))
        {
            if (isMovingLeft)
                isMovingLeft = false;

            else
                isMovingLeft = true;
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
