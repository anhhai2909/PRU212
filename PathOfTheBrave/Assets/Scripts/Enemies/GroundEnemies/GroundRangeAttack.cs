using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRangeAttack : MonoBehaviour
{
    public int damage = 20;
    public float attackRange = 8f;
    public float attackCoolDown = 2f;

    private float attackTimer = Mathf.Infinity;
    public bool canAttack = false;
    public float verticalGap;

    public Rigidbody2D rb;
    public GameObject player;
    public Animator anim;
    public LayerMask playerLayer;
    public GameObject fireball;
    public Transform fireballPosition;

    public bool canMove = true;
    public float attackDelay = 1f;
    private float attackDelayTimer = Mathf.Infinity;

    private bool startDelayTimer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, gameObject.transform.position);
        if (player.transform.position.y <= this.gameObject.transform.position.y + verticalGap)
        {
            if (distanceToPlayer <= attackRange && canAttack)
            {
                StopMovement();
                AttackAnim();
            }
            else if (distanceToPlayer <= attackRange)
            {
                StopMovement();
            }

            if (!canAttack)
            {
                if (startDelayTimer)
                {
                    attackDelayTimer += Time.deltaTime;
                    if (attackDelayTimer > attackDelay)
                    {
                        Attack();
                        attackDelayTimer = 0;
                        startDelayTimer = false;
                    }
                }

                attackTimer += Time.deltaTime;
                if (attackTimer >= (attackCoolDown + attackDelay))
                {
                    attackTimer = 0;
                    canAttack = true;
                }
                if (attackTimer >= (1.5f + attackDelay))
                {
                    canMove = true;
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
        anim.SetTrigger("Attack");
        canAttack = false;
        startDelayTimer = true;
        canMove = false;
    }
    void Attack()
    {
        Instantiate(fireball, fireballPosition.position, Quaternion.identity);
    }
}
