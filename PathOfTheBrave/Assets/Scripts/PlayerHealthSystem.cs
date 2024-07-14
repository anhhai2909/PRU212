using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject skeleton;
    public GameObject flyingEye;
    public GameObject coin;
    public GameObject potion;

    [Header("Coin Drop Manage")]
    public int dropPercent = 50;
    public int luckPercent = 50;
    public int minAvg = 3;
    public int maxAvg = 5;
    public int minIfLuck = 10;
    public int maxIfLuck = 15;

    public GameObject propSpawnPosition;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(skeleton,gameObject.transform.position,Quaternion.identity);
            Instantiate(flyingEye, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            coin.GetComponent<CoinScript>().Spawn(propSpawnPosition.transform, dropPercent, luckPercent, minAvg, maxAvg, minIfLuck, maxIfLuck);
            potion.GetComponent<HealthPotionScript>().Spawn(propSpawnPosition.transform);
        }
    }

    public void TakeDamage(int ATK)
    {
        currentHealth -= ATK;
    }
}
