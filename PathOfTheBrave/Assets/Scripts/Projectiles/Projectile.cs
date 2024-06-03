using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float travelDistance;
    private float xStartPos;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsTarget;
    [SerializeField]
    private Transform damagePosition;

    [Header("Setting value")]
    public float projectileSpeed;
    public float projectileTravelDistance;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;
        xStartPos = transform.position.x;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);
        Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsTarget);

        if (groundHit || damageHit || Mathf.Abs(xStartPos - transform.position.x) >= travelDistance)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }

    public void FireProjectile(float speed, float travelDistance)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
