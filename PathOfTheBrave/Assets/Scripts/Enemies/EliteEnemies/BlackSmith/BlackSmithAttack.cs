using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithAttack : MonoBehaviour
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
    public GameObject LightAttackHitbox;
    public GameObject HeavyAttackHitbox;

    public int countLightAttack;
    private int attackCount = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, gameObject.transform.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            if(attackCount >= countLightAttack)
            {
                StopMovement();
                HeavyAttackAnim();
                attackCount = 0;
            }
            else
            {
                StopMovement();
                LightAttackAnim();
                attackCount++;
            }
            
        }
        else if (distanceToPlayer <= attackRange)
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
    }

    void StopMovement()
    {
        anim.SetBool("IsWalking", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void LightAttackAnim()
    {
        anim.SetTrigger("LightAttack");
        canAttack = false;
        canMove = false;
    }
    void HeavyAttackAnim()
    {
        anim.SetTrigger("HeavyAttack");
        canAttack = false;
        canMove = false;
    }


    void OnDrawGizmosSelected()
    {
        if (LightAttackHitbox == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }
}
