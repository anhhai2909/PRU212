using UnityEditor.UIElements;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 20;
    public float attackRange;
    public float attackCoolDown = 2f;

    private float attackTimer = Mathf.Infinity;
    public bool canAttack = true;
    public bool canMove = true;

    public Rigidbody2D rb;
    public GameObject player;
    public Animator anim;
    public LayerMask playerLayer;
    public GameObject hitbox;
    public float hitboxRadius;
    public float attackDelay = 1f;
    private float attackDelayTimer = 0;
    private bool startDelayTimer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (gameObject.GetComponent<EnemyHealthSystem>().canAttack)
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, gameObject.transform.position);

            if (distanceToPlayer < attackRange && canAttack)
            {
                StopMovement();
                AttackAnim();
            }
            else if (distanceToPlayer < attackRange)
            {
                StopMovement();
            }

            if (!canAttack)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCoolDown)
                {
                    attackTimer = 0;
                    canAttack = true;
                }
                if (attackTimer >= 1.5f)
                {
                    canMove = true;
                }
            }

            if (startDelayTimer)
            {
                attackDelayTimer += Time.deltaTime;
                if (attackDelayTimer >= attackDelay)
                {
                    Attack();
                    attackDelayTimer = 0;
                    startDelayTimer = false;
                }
            }
        }      
    }

    void StopMovement()
    {
        anim.SetBool("IsWalking", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void AttackAnim()
    {
        startDelayTimer = true;
        anim.SetTrigger("Attack");
        canAttack = false;
        canMove = false;
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitbox.transform.position, hitboxRadius, playerLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (hitbox == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitbox.transform.position, hitboxRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }
}
