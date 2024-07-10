using Combat.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public BossFiniteStateMachine stateMachine;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    private Vector2 velocityWorkspace;

    public Transform player;

    private bool isFacingRight = true;

    public D_Boss bossData;

    public PortalScript portal;

    [SerializeField]
    private Transform playerCheck;

    public float currentHealth;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = new BossFiniteStateMachine();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = bossData.maxHealth;
    }
    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
        if (currentHealth <= 0)
        {
            animator.SetBool("dead", true);
            portal.isBossDead = true;
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void FacingToPlayer()
    {
        facingDirection *= -1;
        Vector2 target = new Vector2(player.position.x, rb.position.y);

        // Adjust the facing logic to flip the sprite correctly
        if ((target.x > rb.position.x && isFacingRight) || (target.x < rb.position.x && !isFacingRight))
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Damage(DamageData data)
    {
        currentHealth -= data.Amount;
        animator.SetTrigger("hurt");
    }

    //public virtual bool CheckPlayerInCloseRangeAction()
    //{
    //    return Physics2D.Raycast(playerCheck)
    //}
}
