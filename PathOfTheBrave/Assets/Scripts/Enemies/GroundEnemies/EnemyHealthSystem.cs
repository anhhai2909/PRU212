using Combat.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
    public Animator anim;
    public bool canMove = true;
    public bool canAttack = true;
    public float maxHealth = 100;
    private float currentHealth;
    public GameObject coinSpawnPosition;
    public GameObject coin;
    public GameObject potion;
    private bool isDeath = false;
    public float disapearCooldown = 2f;
    public float disapearTimer = Mathf.Infinity;
    private bool onGround = false;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (gameObject.CompareTag("GroundEnemy") == true)
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
        else if (gameObject.CompareTag("FlyEnemy") == true)
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


    }
    void DeactiveEnemy()
    {
        if (gameObject.CompareTag("GroundEnemy") == true)
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
            canMove = false;
            canAttack = false;
        }
        else if (gameObject.CompareTag("FlyEnemy") == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            canMove = false;
            canAttack = false;           
        }
    }
    public void GetDamage(float damage)
    {
        if (gameObject.CompareTag("Slime") == true)
        {
            gameObject.GetComponent<SlimeScript>().isExplore = true;
        }
        else
        {
            if (gameObject.CompareTag("GroundEnemy") == true)
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
            else if (gameObject.CompareTag("FlyEnemy") == true)
            {
                if (isDeath == false)
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
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("GroundEnemy") == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //gameObject.GetComponent<EnemyHealthSystem>().GetDamage(20);
            }
        }
        else if (gameObject.CompareTag("FlyEnemy") == true)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                onGround = true;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                //gameObject.GetComponent<EnemyHealthSystem>().GetDamage(20);
            }
        }
    }

    public void Damage(DamageData data)
    {
        //Debug.Log("Enemy take damage " + data.Amount);
        GetDamage(data.Amount);
    }
}
