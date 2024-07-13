using Combat.KnockBack;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundRangeEnemyMovement : MonoBehaviour, IKnockBackable
{
    public Rigidbody2D rb;
    public GameObject groundCheck;
    public GameObject wallCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public GameObject player;
    public GameObject fallingCheck;

    public float moveSpeed;
    public float groundCheckRadius = 0.1f;
    public float detectRange = 16f;
    public float chaseSpeed = 6f;
    public int damage = 10;
    public float maxKnockBackTime = 0.2f;

    private bool isFacingWall;
    private bool isGrounded;
    public bool isFacingRight = true;
    private bool isChasing;
    private bool isFalling;

    private bool isKnockBackActive;
    private float knockBackStartTime;
    public bool canMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canMove = true;
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CheckKnockBack();
        checkFalling();
        if (!isFalling)
        {
            if (gameObject.GetComponent<EnemyHealthSystem>().canMove && canMove)
            {
                if(gameObject.GetComponent<GroundRangeAttack>().canAttack)
                {
                    DetectPlayer();
                    if (isChasing != true)
                    {
                        Walk();
                    }
                    else
                    {
                        if (player != null && Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x) + 0.2f > gameObject.GetComponent<GroundRangeAttack>().attackRange - 0.1f)
                        {
                            if (gameObject.GetComponent<GroundRangeAttack>().canMove)
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
            gameObject.GetComponent<IsFacingRight>().facingRight = isFacingRight;
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
        isFacingWall = Physics2D.OverlapCircle(wallCheck.transform.position, groundCheckRadius, groundLayer);
        rb.velocity = Vector2.right * moveSpeed;
        anim.SetBool("IsWalking", true);
        if ((!isGrounded && isFacingRight) || (!isGrounded && !isFacingRight) || isFacingWall)
        {
            Flip();
        }
    }
    void ChasePlayer()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
        isFacingWall = Physics2D.OverlapCircle(wallCheck.transform.position, groundCheckRadius, groundLayer);

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
        if ((!isGrounded && isFacingRight) || (!isGrounded && !isFacingRight) || isFacingWall)
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
        if (player != null)
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
        else
        {
            isChasing = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(wallCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(fallingCheck.transform.position, groundCheckRadius);
    }

    public void KnockBack(KnockBackData data)
    {
        data.Angle.Normalize();

        Vector2 workspace = new Vector2(data.Angle.x * data.Strength * data.Direction, data.Angle.y * data.Strength);
        rb.AddForce(workspace, ForceMode2D.Impulse);

        canMove = false;
        gameObject.GetComponent<GroundRangeAttack>().canAttack = false;
        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }

    private void CheckKnockBack()
    {
        if (isKnockBackActive
            && Time.time >= knockBackStartTime + maxKnockBackTime
           )
        {
            isKnockBackActive = false;
            canMove = true;
            gameObject.GetComponent<GroundRangeAttack>().canAttack = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
