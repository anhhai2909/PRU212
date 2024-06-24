using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    public int damage = 20;

    public float verticalGap;

    public Rigidbody2D rb;
    public GameObject player;
    public Animator anim;
    public LayerMask playerLayer;
    public GameObject weapon;
    public Transform weaponPosition;

    private float rangeAttackTimer = 0;
    public bool canRangeAttack = true;

    private float attackTimer = 0;
    private float meleeAttackTimer = 0;
    public bool canMeleeAttack = true;

    public float attackRange = 8f;
    public float meleeAttackRange = 2f;

    public float rangeAttackCooldown = 2f;
    public float meleeAttackCooldown = 2f;

    public bool canMove = true;

    public float rangeAttackDelay = 0.7f;

    private float rangeAttackDelayTimer = 0;

    private bool startRangeAttackDelayTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (gameObject.GetComponent<EnemyHealthSystem>().canAttack)
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, gameObject.transform.position);
            if (player.transform.position.y <= this.gameObject.transform.position.y + verticalGap)
            {
                if (distanceToPlayer <= attackRange && canRangeAttack && distanceToPlayer >= meleeAttackRange)
                {
                    StopMovement();
                    RangeAttackAnim();
                }
                else if (distanceToPlayer <= attackRange && distanceToPlayer > meleeAttackRange)
                {
                    StopMovement();
                }
                else if (distanceToPlayer <= meleeAttackRange && canMeleeAttack)
                {
                    StopMovement();
                    MeleeAttackAnim();
                }

                if (!canRangeAttack)
                {
                    if (startRangeAttackDelayTimer)
                    {
                        rangeAttackDelayTimer += Time.deltaTime;
                        if (rangeAttackDelayTimer > rangeAttackDelay)
                        {
                            Attack();
                            rangeAttackDelayTimer = 0;
                            startRangeAttackDelayTimer = false;
                        }
                    }

                    rangeAttackTimer += Time.deltaTime;
                    if (rangeAttackTimer >= (rangeAttackCooldown + rangeAttackDelay))
                    {
                        rangeAttackTimer = 0;
                        canRangeAttack = true;
                    }
                    if (rangeAttackTimer >= (1.5f + rangeAttackDelay))
                    {
                        canMove = true;
                    }
                }
                if (!canMeleeAttack)
                {
                    meleeAttackTimer += Time.deltaTime;
                    if (meleeAttackTimer >= meleeAttackCooldown)
                    {
                        meleeAttackTimer = 0;
                        canMeleeAttack = true;
                        canMove = true;
                    }
                }
            }
        }
    }

    void StopMovement()
    {
        anim.SetBool("IsWalking", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void RangeAttackAnim()
    {
        anim.SetTrigger("RangeAttack");
        canRangeAttack = false;
        startRangeAttackDelayTimer = true;
        canMove = false;
    }
    void MeleeAttackAnim()
    {
        anim.SetTrigger("MeleeAttack");
        canMeleeAttack = false;
        canMove = false;
    }
    void Attack()
    {
       GameObject arrow = Instantiate(weapon, weaponPosition.position, Quaternion.identity);
        arrow.GetComponent<GroundRangeAttackStuff>().enemy = gameObject;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, meleeAttackRange);
    }
}