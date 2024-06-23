using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject player;
    public GameObject wallCheck;
    public LayerMask wallLayer;

    public float detectRange = 10f;
    public float chaseSpeed = 6f;
    public int damage = 10;
    public float wallCheckRadius = 0.1f;

    private bool isFacingWall;
    private bool isChasing;
    private bool isFacingRight = true;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (gameObject.GetComponent<EnemyHealthSystem>().canMove)
        {
            DetectPlayer();
            if ((Mathf.Abs((player.transform.position.x - this.gameObject.transform.position.x)) > this.gameObject.GetComponent<FlyingEnemyAttack>().attackRange - 1) && isChasing)
            {
                if (gameObject.GetComponent<FlyingEnemyAttack>().canMove == true)
                {
                    ChasePlayer();
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("IsChasing", false);
            }
        }      
    }
    void ChasePlayer()
    {
        isFacingWall = Physics2D.OverlapCircle(wallCheck.transform.position, wallCheckRadius, wallLayer);
        anim.SetBool("IsChasing", true);
        rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);

        if (isFacingWall)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("IsChasing", false);
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
        float distance = Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x);

        if (distance <= detectRange)
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
        Gizmos.DrawWireSphere(wallCheck.transform.position, wallCheckRadius);
    }
}
