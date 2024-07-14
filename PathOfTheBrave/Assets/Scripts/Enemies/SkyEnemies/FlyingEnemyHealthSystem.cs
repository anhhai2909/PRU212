using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyHealthSystem : MonoBehaviour
{
    public Animator anim;

    public int maxHealth = 100;
    private int currentHealth;
    public GameObject coinSpawnPosition;
    private bool isDeath = false;
    public float disapearCooldown = 2f;
    public float disapearTimer = 0;
    public GameObject coin;
    public GameObject potion;
    private bool onGround = false;
    void Start()
    {
        currentHealth = maxHealth;    
    }

    void Update()
    {
        if (isDeath == true)
        {
            DeactiveEnemy();
            if (onGround == false)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            disapearTimer += Time.deltaTime;
            if (disapearTimer >= disapearCooldown)
            {
                coin.GetComponent<CoinScript>().Spawn(coinSpawnPosition.transform);
                potion.GetComponent<HealthPotionScript>().Spawn(coinSpawnPosition.transform);
                gameObject.SetActive(false);               
            }
        }
    }
    void DeactiveEnemy()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        gameObject.GetComponent<FlyingEnemyMovement>().enabled = false;
        gameObject.GetComponent<FlyingEnemyAttack>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void GetDamage(int damage)
    {
        if (isDeath==false)
        {
            currentHealth -= damage;
            anim.SetTrigger("GetHit");
            if (currentHealth <= 0)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                anim.SetTrigger("Die");
                isDeath = true;
            }
        }     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<FlyingEnemyHealthSystem>().GetDamage(20);
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
