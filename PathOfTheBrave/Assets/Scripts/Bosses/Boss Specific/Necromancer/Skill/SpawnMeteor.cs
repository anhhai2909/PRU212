using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnHeight = 10f; // Specific Y position for spawning meteors
    public float fallDelay = 0.3f; // Delay before meteor starts falling
    public float fallSpeed = 5f; // Speed at which the meteor falls
    public Vector2 fallDirection = new Vector2(-1f, -1f); // Direction of the meteor fall
    public Transform player;
    private Animator animator;
    protected Necromancer necromancer;
    private void Awake()
    {
        necromancer = GetComponent<Necromancer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Initialize animator here
        //SpawnMeteors(10);
    }

    void Update()
    {
       
        // Any continuous behavior or checks can be added here if needed
    }
    private void SpawnMeteors(int numberOfMeteors)
    {
        for (int i = 0; i < numberOfMeteors; i++)
        {
            MeteorSpawn();
        }
    }

    private void MeteorSpawn()
    {
        // Spawn meteor at a random X position and specific Y position
        Vector2 spawnPosition = new Vector2( Random.Range(-15,30), spawnHeight + Random.Range(-5, 20));
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

    public void AnimationMeteorEnd()
    {
        necromancer.spawnMeteorState.isCastTimeOver = true;
    }
}
