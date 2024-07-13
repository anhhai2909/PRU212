using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int numberOfProjectiles;             // Initial number of projectiles to shoot.
    public float projectileSpeed;               // Speed of the projectile.
    public GameObject ProjectilePrefab;         // Prefab to spawn.

    [Header("Private Variables")]
    private Vector3 startPoint;                 // Starting position of the bullet.
    private const float radius = 1F;            // Help us find the move direction.
    private float spawnInterval = 0.5f;           // Time interval between spawns in seconds.
    private float timer;
    private bool toggle = true;                 // Toggle to alternate the number of projectiles

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval; // Initialize the timer with the interval
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; // Decrease timer by the time passed since the last frame

        if (timer <= 0)
        {
            startPoint = transform.position;
            numberOfProjectiles = toggle ? 5 : 8; // Alternate between 4 and 6 projectiles
            SpawnProjectile(numberOfProjectiles);
            toggle = !toggle; // Toggle the boolean
            timer = spawnInterval; // Reset the timer
        }
    }

    // Spawns x number of projectiles.
    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 180f / (_numberOfProjectiles - 1); // Adjust the step to spread projectiles evenly
        float angle = 270f; // Start from 180 degrees to cover the lower half of the circle.

        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            // Direction calculations.
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            // Create vectors.
            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            // Create game objects.
            GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            // Destroy the gameobject after 10 seconds.
            Destroy(tmpObj, 3F);

            angle -= angleStep; // Move to the next angle
        }
    }
}
