using Combat.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject groundCheck;
    public GameObject wallCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public GameObject player;
    public GameObject fallingCheck;
    public GameObject hitbox;
    public LayerMask playerLayer;
    public float hitboxRadius;
    public float groundCheckRadius = 0.1f;
    public float detectRange = 16f;
    public float chaseSpeed = 6f;
    public int damage = 10;

    private bool isFacingWall;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isChasing;
    private bool isFalling;

    public float exploreTime = 4f;
    public float exploreTimer = 0f;
    public float disapearTime = 0.45f;
    public float disapearTimer = 0;

    public bool isExplore = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        checkFalling();
        if (!isFalling)
        {
            DetectPlayer();
            if (isChasing != true)
            {
                anim.SetBool("IsSleeping", true);
            }
            else
            {
                anim.SetBool("IsSleeping", false);
                ChasePlayer();
                exploreTimer += Time.deltaTime;

                if (exploreTimer >= exploreTime && !isExplore)
                {
                    anim.SetTrigger("Explore");
                    isExplore = true;
                }
            }
            if(Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x) <= 0.3)
            {
                anim.SetTrigger("Explore");
                isExplore = true;
            }
        }
        if (isExplore)
        {

            rb.velocity = Vector2.zero;
            disapearTimer += Time.deltaTime;
            Explore();
            if (disapearTimer >= disapearTime)
            {
                Destroy(gameObject);
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
    void Explore()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitbox.transform.position, hitboxRadius, playerLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.gameObject.name + " take " + damage + " damage");
            if (enemy.TryGetComponent(out IDamageable damageable))
            {
                //damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
            }
        }
    }
    void ChasePlayer()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
        isFacingWall = Physics2D.OverlapCircle(wallCheck.transform.position, groundCheckRadius, groundLayer);

        if (player.transform.position.x > transform.position.x)
        {

            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
        }
        if ((!isGrounded && isFacingRight) || (!isGrounded && !isFacingRight) || isFacingWall)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        chaseSpeed = -chaseSpeed;
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

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(wallCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(fallingCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(hitbox.transform.position, hitboxRadius);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Explore");
            Debug.Log("Hit");
            isExplore = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

}
