using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletSpawnerPrefab; // Reference to the prefab

    public void SpawnBulletSpawner()
    {

        Vector3 spawnPosition = new Vector3(2f, 7f, 0f);

        GameObject spawnedBulletSpawner = Instantiate(bulletSpawnerPrefab, spawnPosition, Quaternion.identity);

        Destroy(spawnedBulletSpawner, 15f);
    }
}
