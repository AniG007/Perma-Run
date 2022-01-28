using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform firePoint;
    [SerializeField] Transform attackPoint;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject enemy;

    //[SerializeField] AudioSource bulletFire;
    [SerializeField] AudioSource punch;
    
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float attackDamage;
    bool IsAttacked = false;

    public LayerMask Layer;

    // Update is called once per frame

    private void Start()
    {
     punch.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire2") && playerMovement.getHorizontalMove() <= 0) //writing as two if statements since Fire2 interferes with mobile controls.
        {
            //Shoot();
            //Attack();
            animator.SetBool("attack", true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopAttack();
        }
#endif
#if UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Shoot") && playerMovement.getHorizontalMove() <= 0)
        {
            //Shoot();
            //Attack();
            animator.SetBool("attack", true);
        }

        if (CrossPlatformInputManager.GetButtonUp("Shoot"))
        {
            //Shoot();
            StopAttack();
        }
#endif
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        //bulletFire.Play();
    }

    void Attack()
    {
        //animator.SetBool("attack", true);
        punch.Play();
        //Circle collider hits player
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Layer);

        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<Goblin>().CheckHealth() != 0)
                hit.GetComponent<Goblin>().TakeDamageFromPlayer(attackDamage);
        }
    }

    void StopAttack()
    {
        animator.SetBool("attack", false);
    }

    private void OnDrawGizmosSelected()
    {
        //For Drawing lines around the invisible 2D overlap circle
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
