using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : Skill
{
    public GameObject meteorPrefab;
    public float spawnHeight = 10f; // Specific Y position for spawning meteors
    public float fallDelay = 0.3f; // Delay before meteor starts falling
    public float fallSpeed = 5f; // Speed at which the meteor falls
    public Vector2 fallDirection = new Vector2(-1f, -1f); // Direction of the meteor fall

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Initialize animator here
    }

    void Update()
    {
        // Any continuous behavior or checks can be added here if needed
    }

    private void Spawn()
    {
        // Spawn meteor at a random X position and specific Y position
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), spawnHeight);
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

        // Add delay before the meteor starts falling
        StartCoroutine(FallAfterDelay(meteor));
    }

    private IEnumerator FallAfterDelay(GameObject meteor)
    {
        yield return new WaitForSeconds(fallDelay);

        // Add diagonal falling behavior to the meteor
        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // Disable gravity
            rb.velocity = fallDirection.normalized * fallSpeed; // Set velocity to make meteor fall diagonally
        }
    }

    public override void Activate()
    {
        if (animator != null)
        {
            animator.SetBool("isCastingMeteor", false);
        }
        Spawn();
    }

    public override void ActivateAnimation()
    {
        base.ActivateAnimation();
        if (animator != null)
        {
            animator.SetBool("isCastingMeteor", true);
        }
    }
}
