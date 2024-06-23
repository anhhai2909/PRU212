using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerController : Boss
{
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform player;
    private int currentSkillIndex = 0;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isGrounded;

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        maxHealth = 300;
        attackDamage = 15;
        attackRange = 1.5f;
        currentHealth = maxHealth;
    }

    public void Update()
    {
        // Check if the boss is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isAlive)
        {
            // Perform boss actions based on game logic
            Flip();
        }
        else
        {
            animator.SetBool("isDead", true);
        }
    }

    void Flip()
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);

        // Flip the boss sprite if it changes direction
        if ((target.x > rb.position.x && !isFacingRight) || (target.x < rb.position.x && isFacingRight))
        {
            // Switch the direction the boss is facing
            isFacingRight = !isFacingRight;

            // Flip the boss sprite horizontally
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public override void Attack()
    {
        // Implement boss attack logic here
    }
    public override void DestroyBoss()
    {
        Destroy(gameObject);
        Debug.Log("Boss object destroyed");
    }
}
