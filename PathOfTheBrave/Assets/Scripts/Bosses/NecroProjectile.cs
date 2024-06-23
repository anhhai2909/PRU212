using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroProjectile : Skill
{
    public GameObject fireballPrefab;
    public Transform fireballPos;
    private GameObject currentFireball;
    private Animator animator;
    private float timer = 0f;
    private float delay = 2f; // Adjust this value as needed

    void Start()
    {
        animator = GetComponent<Animator>(); // Initialize animator here
        currentFireball = null;
    }

    void Update()
    {
        // Check if current fireball exists and is active
        if (currentFireball == null)
        {
            // No active fireball, ready to shoot
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                Shoot();
                timer = 0f; // Reset the timer
            }
        }
        Debug.Log(timer);
    }

    public void Shoot()
    {
        if (fireballPrefab != null && fireballPos != null)
        {
            // Instantiate a new fireball
            currentFireball = Instantiate(fireballPrefab, fireballPos.position, Quaternion.identity);
            Debug.Log("Shooting");
        }
    }

    public override void Activate()
    {
        if (animator != null)
        {
            animator.SetBool("isShooting", false);
        }
        Shoot();
    }

    public override void ActivateAnimation()
    {
        base.ActivateAnimation();
        if (animator != null)
        {
            animator.SetBool("isShooting", true);
        }
    }
}
