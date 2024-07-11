using System.Collections;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    private enum State
    {
        Idle,
        LookingForPlayer,
        PlayerDetected,
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float maxHealth,
        currentHealth,
        meleeAttackRange,
        rangeAttackRange,
        laserCastRange,
        immuneDuration,
        movementSpeed,
        knockbackDuration,
        groundCheckDistance,
        wallCheckDistance;

    [SerializeField]
    private int damage, armor;

    [SerializeField]
    private Vector2 knockbackSpeed;

    private bool isImmune, isDead, groundDetected, wallDetected;

    private float knockbackStartTime;

    private GameObject golem;
    private Rigidbody2D golemRb;
    private Animator golemAnim;

    private Transform player;
    [SerializeField]
    private Transform groundCheck, wallCheck;

    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    private float[] attackDetails = new float[2];

    private Vector2 movement;

    private int facingDirection, damageDirection;

    [SerializeField]
    private GameObject hitParticle, deathChunkParticle, deathBloodParticle;


    public GameObject arm;
    public Transform armPos;

    private void Start()
    {
        golem = GameObject.FindGameObjectWithTag("Enemy");
        golemRb = golem.GetComponent<Rigidbody2D>();
        golemAnim = golem.GetComponent<Animator>();
        currentHealth = maxHealth;
        facingDirection = 1;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StateMachine());
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                UpdateIdleState();
                break;
            case State.LookingForPlayer:
                UpdateLookingForPlayerState();
                break;
            case State.PlayerDetected:
                UpdatePlayerDetectedState();
                break;
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private IEnumerator StateMachine()
    {
        while (!isDead)
        {
            switch (currentState)
            {
                case State.Idle:
                    UpdateIdleState();
                    break;
                case State.LookingForPlayer:
                    UpdateLookingForPlayerState();
                    break;
                case State.PlayerDetected:
                    UpdatePlayerDetectedState();
                    break;
                case State.Moving:
                    UpdateMovingState();
                    break;
                case State.Knockback:
                    UpdateKnockbackState();
                    break;
                case State.Dead:
                    UpdateDeadState();
                    break;
            }
            yield return null;
        }
    }

    private void EnterIdleState()
    {
        if (currentHealth > maxHealth / 2)
        {
            golemAnim.SetBool("glowing", false);
            golemAnim.SetBool("idle", true);
        }
        else
        {
            golemAnim.SetBool("idle", false);
            golemAnim.SetBool("glowing", true);
            Debug.Log("Gl");
        }
    }

    private void UpdateIdleState()
    {
        if (currentHealth > maxHealth / 2)
        {
            golemAnim.SetBool("glowing", false);
            golemAnim.SetBool("idle", true);
        }
        else
        {
            golemAnim.SetBool("idle", false);
            golemAnim.SetBool("glowing", true);
        }
        SwitchState(State.LookingForPlayer);
    }

    private void ExitIdleState() 
    {
        golemAnim.SetBool("idle",false);
    }

    private void EnterLookingForPlayerState() { }

    private void UpdateLookingForPlayerState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        //if (Vector2.Distance(transform.position, player.position) < rangeAttackRange)
        //{
        //    SwitchState(State.PlayerDetected);
        //}
    }

    private void ExitLookingForPlayerState() { }

    private void EnterPlayerDetectedState() { }

    private void UpdatePlayerDetectedState()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance < meleeAttackRange)
        {
            MeleeAttack();
        }
        else if (playerDistance < laserCastRange)
        {
            LaserCast();
        }
        else if (playerDistance < rangeAttackRange)
        {
            RangeAttack();
        }
        else
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitPlayerDetectedState() { }

    private void EnterMovingState() { }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, golemRb.velocity.y);
            golemRb.velocity = movement;
            golemAnim.Play("Moving");
        }

        if (Vector2.Distance(transform.position, player.position) < meleeAttackRange)
        {
            SwitchState(State.PlayerDetected);
        }
    }

    private void ExitMovingState() { }

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        golemRb.velocity = movement;
        golemAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        golemAnim.SetBool("Knockback", false);
    }

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, golem.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, golem.transform.position, deathBloodParticle.transform.rotation);
        isDead = true;
        Destroy(gameObject);
    }

    private void UpdateDeadState() { }

    private void ExitDeadState() { }

    private void Damage(float[] attackDetails)
    {
        if (isImmune) return;

        currentHealth -= attackDetails[0];
        Instantiate(hitParticle, golem.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        damageDirection = attackDetails[1] > golem.transform.position.x ? -1 : 1;

        if (currentHealth > 0.0f)
        {
            StartCoroutine(ImmuneTimer());
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private IEnumerator ImmuneTimer()
    {
        isImmune = true;
        yield return new WaitForSeconds(immuneDuration);
        isImmune = false;
    }

    private void MeleeAttack()
    {
        // Implement melee attack logic
        golemAnim.Play("MeleeAttack");
        // Assuming there's a method to deal damage to the player
        
    }

    private void RangeAttack()
    {
        // Implement range attack logic
        golemAnim.Play("RangeAttack");
        // Assuming there's a method to shoot a projectile
        ShootProjectile();
    }

    private void LaserCast()
    {
        // Implement laser cast logic
        golemAnim.Play("LaserCast");
        // Assuming there's a method to cast a laser
        CastLaser();
    }

    private void CheckTouchDamage()
    {
        
       
    }

    private void Flip()
    {
        facingDirection *= -1;
        golem.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Idle:
                ExitIdleState();
                break;
            case State.LookingForPlayer:
                ExitLookingForPlayerState();
                break;
            case State.PlayerDetected:
                ExitPlayerDetectedState();
                break;
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Idle:
                EnterIdleState();
                break;
            case State.LookingForPlayer:
                EnterLookingForPlayerState();
                break;
            case State.PlayerDetected:
                EnterPlayerDetectedState();
                break;
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    private void ShootProjectile()
    {
        Instantiate(arm, armPos.position, Quaternion.identity);
    }

    private void CastLaser()
    {
        // Implement laser casting logic here
    }
}
