using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;

    private void Start()
    {
        audio.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("playerCollision", true);
    }

    private void stopAnimation()
    {
        animator.SetBool("playerCollision", false); //this is called in the animation as an event
    }

    private void playAudio()
    {
        audio.Play();
    }
}
