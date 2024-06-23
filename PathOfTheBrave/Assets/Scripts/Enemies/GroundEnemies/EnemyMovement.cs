using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject groundCheck;
    public GameObject wallCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public GameObject player;
    public GameObject hitbox;
    public GameObject fallingCheck;

    public float moveSpeed;
    public float hitboxRadius = 1.5f;
    public float groundCheckRadius = 0.1f;
    public float detectRange = 16f;
    public float chaseSpeed = 6f;
    public int damage = 10;

    private bool isFacingWall;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isChasing;
    private bool isFalling;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (gameObject.GetComponent<EnemyAttack>().canAttack)
        {
            checkFalling();
            if (!isFalling)
            {
                if (gameObject.GetComponent<EnemyHealthSystem>().canMove)
                {
                    DetectPlayer();
                    if (isChasing != true)
                    {
                        Walk();
                    }
                    else
                    {
                        if (Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x) + 0.2f > gameObject.GetComponent<EnemyAttack>().attackRange - 0.1f)
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
        }
    }
    void checkFalling()
    {
        if(Physics2D.OverlapCircle(fallingCheck.transform.position, groundCheckRadius, groundLayer))
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
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(wallCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(fallingCheck.transform.position, groundCheckRadius);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        gameObject.GetComponent<EnemyHealthSystem>().GetDamage(10);
    //    }
    //}
}
