using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : Skill
{
    public GameObject player;
    public GameObject skeletonPrefab;
    public float spawnRadius = 10.0f;

    private Animator animator;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //skeletonPrefab = GameObject.FindGameObjectWithTag("SpawnSkeleton");
        animator = GetComponent<Animator>(); // Initialize animator here
    }

    public override void Activate()
    {
        if (animator != null)
        {
            animator.SetBool("isSpawning", false);
        }

        if (skeletonPrefab != null)
        {
            
        }
    }
    public void Spawn()
    {
        Vector2 randomPosition = GetRandomPositionAroundPlayer();
        Instantiate(skeletonPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Spawning");
    }

    private Vector2 GetRandomPositionAroundPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        float randomX = Random.Range(playerPosition.x - spawnRadius, playerPosition.x);
        Vector2 randomPosition = new Vector2(randomX, -2.5f);

        return randomPosition;
    }

    public override void ActivateAnimation()
    {
        base.ActivateAnimation();
        if (animator != null)
        {
            animator.SetBool("isSpawning", true);
        }
    }
}

