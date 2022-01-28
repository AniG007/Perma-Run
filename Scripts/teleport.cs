using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    [SerializeField] GameObject teleportLocation;
    [SerializeField] GameObject player;
    
    [SerializeField] float speed;
    
    [SerializeField] Rigidbody2D rb;

    bool pool = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = teleportLocation.transform.position;
            pool = true;
        }
    }

    private void Update()
    {
        if (pool == true) {
            //Debug.Log("Pool is true");
            Vector2 direction = Vector2.left;
            Vector2 force = direction * speed * Time.deltaTime;
            //Debug.Log("Forcey");
            rb.AddForce(force);
        }
    }
}
