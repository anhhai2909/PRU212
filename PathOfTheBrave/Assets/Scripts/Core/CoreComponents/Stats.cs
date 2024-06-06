using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float disapearCooldown = 2f;
    private float disapearTimer = 0;
    private float currentHealth;
    private Animator anim;
    private Transform root;

    public GameObject coinSpawnPosition;
    public GameObject coin;
    public GameObject potion;

    protected override void Awake()
    {
        base.Awake();
        root = gameObject.transform.parent.parent;
        anim = root.GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isAlive())
        {
            disapearTimer += Time.deltaTime;
            if (disapearTimer >= disapearCooldown)
            {
                Disapear();
            }
        }
    }

    public void DecreaseHealth(float amount)
    {
        anim.SetTrigger("damage");
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Health is zero!!");
            if (anim == null) Debug.Log("Can not find animator");
            anim.SetBool("dead", true);
            root.gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }

    public void Deactive()
    {
        root.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (root.GetComponent<BoxCollider2D>() != null)
        {
            root.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (root.GetComponent<CircleCollider2D>() != null)
        {
            root.GetComponent<BoxCollider2D>().enabled = false;
        }
        root.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void Disapear()
    {
        root.gameObject.SetActive(false);
        if (root.tag == "Enemy")
        {
            potion.GetComponent<HealthPotionScript>().Spawn(coinSpawnPosition.transform);
            coin.GetComponent<CoinScript>().Spawn(coinSpawnPosition.transform);
        }
    }
}
