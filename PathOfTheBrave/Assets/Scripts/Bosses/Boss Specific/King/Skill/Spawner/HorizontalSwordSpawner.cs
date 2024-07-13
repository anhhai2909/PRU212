using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSwordSpawner : MonoBehaviour
{
    public GameObject prefab; 
    public int spawnCount = 3; 
    public float spawnInterval = 1.0f; 
    public float speed = 5f; 
    public float startXPosition = 25f; 
    public float endXPosition = -30f; 

    private bool isSpawning = false;

    void Update()
    {
        
    }

    private IEnumerator SpawnObjects()
    {
        isSpawning = true;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newObject = Instantiate(prefab, new Vector3(startXPosition, 0, 0), Quaternion.identity);
            HorizontalSword horizontalSword = newObject.GetComponent<HorizontalSword>();
            if (horizontalSword != null)
            {
                horizontalSword.startXPosition = startXPosition;
                horizontalSword.endXPosition = endXPosition;
                horizontalSword.speed = speed;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }
}
