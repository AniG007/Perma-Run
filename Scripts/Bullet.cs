using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float speed;
    [SerializeField] Rigidbody2D skull;
    [SerializeField] float bulletDamage;

    void Start()
    {
        skull.velocity = transform.right * speed;
        bulletDamage = 20;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Goblin goblin = collision.GetComponent<Goblin>();

        if (goblin != null)
        {
            goblin.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }*/

    //To destroy the bullet object once it goes beyond the game, reference: https://answers.unity.com/questions/1230388/how-to-destroy-object-after-it-moves-out-of-screen.html
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
