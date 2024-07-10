using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : MonoBehaviour
{
    public GameObject player;
    public GameObject skeletonPrefab;
    public float spawnRadius = 10.0f;

    private Animator animator;
    protected Necromancer necromancer;
    private void Awake()
    {
        necromancer = GetComponent<Necromancer>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //skeletonPrefab = GameObject.FindGameObjectWithTag("SpawnSkeleton");
        animator = GetComponent<Animator>(); // Initialize animator here
    }
     void SpawnSkeletons()
    {
        Vector2 randomPosition = GetRandomPositionAroundPlayer();
        Instantiate(skeletonPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Spawning");
    }

    private Vector2 GetRandomPositionAroundPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        //float randomX = Random.Range(playerPosition.x - spawnRadius, playerPosition.x);
        Vector2 randomPosition = new Vector2(player.transform.position.x, -2.5f);

        return randomPosition;
    }
    public void AnimationSkeletonEnd()
    {
        necromancer.spawnSkeletonState.isCastTimeOver = true;
    }

}

