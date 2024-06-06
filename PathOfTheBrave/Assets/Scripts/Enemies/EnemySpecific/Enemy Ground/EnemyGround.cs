using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Entity
{
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    public GameObject hitbox;
    public GameObject fallingCheck;

    public float moveSpeed;
    public float hitboxRadius = 1.5f;
    public float groundCheckRadius = 0.1f;
    public float detectRange = 16f;
    public float chaseSpeed = 6f;
    public int damage = 10;

    private bool isFacingRight = true;
    private bool isChasing;
    private bool isFalling;
    private bool isGrounded;
    private bool isFacingWall;

    private GameObject player;
    public Core Core { get; private set; }
    private CollisionSenses collisionSenses;

    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses);
    }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        isGrounded = CollisionSenses.Ground;
        isFacingWall = CollisionSenses.WallFront;
        Core.LogicUpdate();
        checkFalling();
        if (!isFalling)
        {
            DetectPlayer();
            if (isChasing != true)
            {
                Walk();
            }
            else
            {
                if (Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x) > gameObject.GetComponent<EnemyAttack>().attackRange)
                {
                    if (gameObject.GetComponent<EnemyAttack>().canMove)
                    {
                        ChasePlayer();
                    }
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    anim.SetBool("IsWalking", false);
                }

            }
        }
    }
    void checkFalling()
    {
        if (Physics2D.OverlapCircle(fallingCheck.transform.position, groundCheckRadius, groundLayer))
        {
            isFalling = false;
        }
        else
        {
            isFalling = true;
        }
    }
    void Walk()
    {
        rb.velocity = Vector2.right * moveSpeed;
        anim.SetBool("IsWalking", true);
        if ((CollisionSenses.Ground && isFacingRight) || (!CollisionSenses.Ground && !isFacingRight) || CollisionSenses.WallFront)
        {
            Flip();
        }
    }
    void ChasePlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            anim.SetBool("IsWalking", true);
            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            anim.SetBool("IsWalking", true);
            rb.velocity = new Vector2(chaseSpeed * -1, rb.velocity.y);
        }
        if ((!CollisionSenses.Ground && isFacingRight) || (!CollisionSenses.Ground && !isFacingRight) || CollisionSenses.WallFront)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("IsWalking", false);
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        moveSpeed = -moveSpeed;
    }
    void DetectPlayer()
    {
        float range = Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x);

        if (range <= detectRange)
        {

            if ((player.transform.position.x > transform.position.x && !isFacingRight) ||
                (player.transform.position.x < transform.position.x && isFacingRight))
            {
                Flip();
            }

            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitbox.transform.position, hitboxRadius);
        Gizmos.DrawWireSphere(fallingCheck.transform.position, groundCheckRadius);
    }
}
