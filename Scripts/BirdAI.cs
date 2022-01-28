using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdAI : MonoBehaviour
{
    private bool isFacingRight;
    [SerializeField] float speed;

    private void Start()
    {
        isFacingRight = false;
        speed = 5;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Through"))
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }

            else
            {
                isFacingRight = false;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }
    }

    private void Update()
    {
        if (isFacingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }



}
