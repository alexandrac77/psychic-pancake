using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private AudioSource deathSfx;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bee"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSfx.Play();
        // Switch to death animation
        animator.SetTrigger("dying");

        // Change rigid body type to static
        rb.bodyType = RigidbodyType2D.Static;

        // reset score to 0
        Collectibles.collectCounter = 0;
    }

    private void ReloadLevel()
    {
        // reload level 1 
        // called by trigger function in death animation.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
