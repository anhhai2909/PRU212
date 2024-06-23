using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : MonoBehaviour
{
    public GameObject player;
    public float spawnRadius = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(GameObject skeleton)
    {
        if(skeleton != null)
        {
            Vector2 randomPosition = GetRandomPositionAroundPlayer();
            Instantiate(skeleton, randomPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionAroundPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        float randomX = Random.Range(playerPosition.x - spawnRadius, playerPosition.x + spawnRadius); // Random x position within the radius
        Vector2 randomPosition = new Vector2(randomX, playerPosition.y); // Keep the y position the same

        return randomPosition;
    }
}
