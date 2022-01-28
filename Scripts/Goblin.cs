using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Goblin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 3f;

    //Transform player;

    [SerializeField] Animator animator;
    private bool isFacingRight;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float attackDamage;
    public LayerMask Layer;
    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] HealthBar healthBar;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] AudioSource enemyAttack;
    [SerializeField] int score = 2;

    int destroyer;

    void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
        isFacingRight = false;
        healthBar.setMaxHealth(maxHealth);
        currentHealth = maxHealth;
        enemyAttack.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
        //enemyDeath = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("attack"))
        {
            //To move enemy when the attack animation is not played and when enemy is not dead
            if (isFacingRight && !animator.GetBool("IsDead"))
            {
                
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }

            else if(!isFacingRight && !animator.GetBool("IsDead"))
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }

        else
        {
            //To make the enemy still when the attack animation is playing

            if (isFacingRight)
            {
                transform.Translate(Vector2.right * 0);
            }

            else
            {
                transform.Translate(Vector2.left * 0);
            }
        }

        //To rotate the enemy's health bar according to the sprite's movement

        if (isFacingRight)
        {
            healthBar.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }

        else
        {
            healthBar.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 360, transform.eulerAngles.z);
        }

        //Death Animation
        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            StartCoroutine(DestroyEnemy());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        /*if (collision.CompareTag("Player"))
        {
            //to continue patrolling when player moves on
            animator.SetBool("attack", false);
        }*/
         if ( collision.CompareTag("bullet") || collision.CompareTag("platform") || collision.CompareTag("red") || collision.CompareTag("Player") || collision.CompareTag("mic") || collision.CompareTag("location") || collision.CompareTag("storage") || collision.CompareTag("camera") || collision.CompareTag("contacts") || collision.CompareTag("activity") || collision.CompareTag("sensors") || collision.CompareTag("calendar") || collision.CompareTag("sms") || collision.CompareTag("phone"))
        {
            //To avoid enemy flip when colliding with coin, bullet
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }

        else
        {
            // To flip direction after reaching the end of a platform

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

        if (collision.CompareTag("Player"))
        {
            animator.SetBool("attack", true);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        //To play attacking animation when colliding with player
        if (collision.CompareTag("Player"))
        {   
            animator.SetBool("attack", true);
        }
        
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("storage") || collision.CompareTag("bullet") || collision.CompareTag("mic") || collision.CompareTag("location") || collision.CompareTag("platform") || collision.CompareTag("red"))
        {
            //To avoid enemy flip when colliding with coin, bullet
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }

        if (collision.CompareTag("Player"))
        {
            //to continue patrolling when player moves on
            animator.SetBool("attack", false);
        }

    }

    private void Attack()
    {
        //Circle collider hits player
        enemyAttack.Play(); //If audio source is missing, the code will fail here and the attack won't happen. Atleast use a dummy audio source
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Layer);

        foreach(Collider2D hit in hits)
        {
            if (player.GetComponent<PlayerMovement>().CheckHealth() != 0)
                player.GetComponent<PlayerMovement>().TakeDamage(attackDamage);

            //player.GetComponent<PlayerMovement>().TakeDamage(0); //Goblin won't attack player
        }
    }

    private void OnDrawGizmosSelected()
    {
        //For Drawing lines around the invisible 2D overlap circle
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamageFromPlayer(float damage)
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.setHealth(currentHealth);
            //this.gameObject.SetActive(false);
            //enemyDeath.Play();   
        }
        else
        {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);
        }
    }

    //To Destroy enemy game object when enemy's health becomes 0, so as to remove the object from the scene
    IEnumerator DestroyEnemy()
    {
        if (!(this.gameObject.tag == "mushroom"))
            yield return new WaitForSeconds(1f);
        else
        {
            enemyAttack.Stop();
            yield return new WaitForSeconds(1.5f);
        }
        Destroy(gameObject);
        Score.instance.ChangeScore(score);
        IncreaseEnemyCountForDestroyerBadge();
        StopCoroutine(DestroyEnemy());
    }

    public float CheckHealth()
    {
        return currentHealth;
    }

    void IncreaseEnemyCountForDestroyerBadge()
    {
        destroyer = int.Parse(PlayerPrefs.GetString("destroyer", "0"));

        if (destroyer < 25)
        {
            destroyer += 1;
            PlayerPrefs.SetString("destroyer", destroyer.ToString());
        }

        else
        {
            PlayerPrefs.SetInt("D", 1);
            Score.instance.UploadBadges();
        }
    }

}