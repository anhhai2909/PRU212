using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDHealthSystem : MonoBehaviour
{
    public Animator anim;

    public int maxHealth = 100;
    private int currentHealth;
    public GameObject coinSpawnPosition;
    public GameObject coin;
    public GameObject potion;
    private bool isDeath = false;
    public float disapearCooldown = 1f;
    public float disapearTimer = Mathf.Infinity;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDeath == true)
        {
            DeactiveEnemy();
            disapearTimer += Time.deltaTime;
            if (disapearTimer >= disapearCooldown)
            {
                potion.GetComponent<HealthPotionScript>().Spawn(coinSpawnPosition.transform);
                coin.GetComponent<CoinScript>().Spawn(coinSpawnPosition.transform);
                gameObject.SetActive(false);
            }
        }
    }
    void DeactiveEnemy()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<CircleCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<BoDeathMovement>().enabled = false;
        gameObject.GetComponent<BoDeathAttack>().enabled = false;
    }
    public void GetDamage(int damage)
    {
        if (isDeath == false)
        {
            currentHealth -= damage;
            anim.SetTrigger("IsHit");
            if (currentHealth <= 0)
            {
                anim.SetTrigger("Die");
                isDeath = true;
            }
        }
    }
}
