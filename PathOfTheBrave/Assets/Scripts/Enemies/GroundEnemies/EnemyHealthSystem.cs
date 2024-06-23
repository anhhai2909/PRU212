using Combat.Damage;
using CoreSystem;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
    private Animator anim;

    [SerializeField] private GameObject hitParticles;
    [SerializeField] private float maxHealth = 100;

    private float currentHealth;
    public GameObject coinSpawnPosition;
    public GameObject coin;
    public GameObject potion;
    private bool isDeath = false;
    public float disapearCooldown = 2f;
    public float disapearTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
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
                Destroy(gameObject);
                //gameObject.SetActive(false);
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
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<EnemyAttack>().enabled = false;
    }
    public void GetDamage(float damage)
    {
        if (isDeath == false)
        {
            currentHealth -= damage;
            Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            anim.SetTrigger("damage");
            if (currentHealth <= 0)
            {
                anim.SetTrigger("Die");
                isDeath = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<EnemyHealthSystem>().GetDamage(20);
        }
    }

    public void Damage(DamageData data)
    {
        Debug.Log(data.Amount + " Damage taken");
        GetDamage(data.Amount);
    }
}
