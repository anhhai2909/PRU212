using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GolemController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Glowing,
        LookingForPlayer,
        PlayerDetected,
        Moving,
        Knockback,
        Dead,
        Immune,
        ArmorBuff,
        RangeAttack,
        LaserCast,
        MeleeAttack
    }

    private State currentState;

    [SerializeField] private float maxHealth, currentHealth;
    [SerializeField] private float meleeAttackRange, rangeAttackRange, laserCastRange;
    [SerializeField] private float immuneDuration, armorBuffDuration, movementSpeed, knockbackDuration;
    [SerializeField] private float groundCheckDistance, wallCheckDistance;
    [SerializeField]
    private float
       touchDamage,
       lastTouchDamageTime,
       touchDamageCoolDown,
       touchDamageWidth,
       touchDamageHeight;

    [SerializeField] private int damage, armor;

    [SerializeField] private Vector2 knockbackSpeed;

    private bool isImmune, isDead, groundDetected, wallDetected;

    private GameObject golem;
    private Rigidbody2D golemRb;
    private Animator golemAnim;

    private Transform player;
    [SerializeField] private Transform groundCheck, wallCheck, touchDamageCheck;

    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    private float[] attackDetails = new float[2];

    private Vector2
        movement,
        touchDamageBotLeft,
        touchDamageTopRight;

    private int facingDirection, damageDirection;

    private float knockbackStartTime;

    [SerializeField]
    private GameObject hitParticle, deathChunkParticle, deathBloodParticle;
    public GameObject arm;
    public Transform armPos;

    private void Start()
    {
        InitializeVariables();
        StartCoroutine(StateMachine());
    }

    private void InitializeVariables()
    {
        golem = GameObject.FindGameObjectWithTag("Enemy");
        golemRb = golem.GetComponent<Rigidbody2D>();
        golemAnim = golem.GetComponent<Animator>();
        currentHealth = maxHealth;
        facingDirection = 1;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                UpdateIdleState();
                break;
            case State.Glowing:
                UpdateGlowingState();
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
            case State.Immune:
                UpdateImmuneState();
                break;
            case State.ArmorBuff:
                UpdateArmorBuffState();
                break;
            case State.RangeAttack:
                UpdateRangeAttackState();
                break;
            case State.LaserCast:
                UpdateLaserCastState();
                break;
            case State.MeleeAttack:
                UpdateMeleeAttackState();
                break;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchState(State.RangeAttack);
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
                case State.Glowing:
                    UpdateGlowingState();
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
                case State.Immune:
                    UpdateImmuneState();
                    break;
                case State.ArmorBuff:
                    UpdateArmorBuffState();
                    break;
                case State.RangeAttack:
                    UpdateRangeAttackState();
                    break;
                case State.LaserCast:
                    UpdateLaserCastState();
                    break;
                case State.MeleeAttack:
                    UpdateMeleeAttackState();
                    break;
            }
            yield return null;
        }
    }

    private void EnterState(State state)
    {
        ExitCurrentState();

        currentState = state;

        switch (currentState)
        {
            case State.Idle:
                EnterIdleState();
                break;
            case State.Glowing:
                EnterGlowingState();
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
            case State.Immune:
                EnterImmuneState();
                break;
            case State.ArmorBuff:
                EnterArmorBuffState();
                break;
            case State.RangeAttack:
                EnterRangeAttackState();
                break;
            case State.LaserCast:
                EnterLaserCastState();
                break;
            case State.MeleeAttack:
                EnterMeleeAttackState();
                break;
        }
    }

    private void ExitCurrentState()
    {
        switch (currentState)
        {
            case State.Idle:
                ExitIdleState();
                break;
            case State.Glowing:
                ExitGlowingState();
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
            case State.Immune:
                ExitImmuneState();
                break;
            case State.ArmorBuff:
                ExitArmorBuffState();
                break;
            case State.RangeAttack:
                ExitRangeAttackState();
                break;
            case State.LaserCast:
                ExitLaserCastState();
                break;
            case State.MeleeAttack:
                ExitMeleeAttackState();
                break;
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
        }
    }

    private void UpdateIdleState()
    {
        Debug.Log("Current Health: " + currentHealth);
        Debug.Log("Current State: " + currentState);
        if (currentHealth <= maxHealth / 2)
        {
            EnterState(State.Glowing);
        }
        // Implement idle state logic here
    }

    private void ExitIdleState()
    {
        golemAnim.SetBool("idle", false);
        golemAnim.SetBool("glowing", false);
    }

    private void EnterGlowingState()
    {
        Debug.Log("Entering Glowing State");
        golemAnim.SetBool("idle", false);
        golemAnim.SetBool("glowing", true);

    }

    private void UpdateGlowingState()
    {
        // Implement glowing state update logic here
        SwitchState(State.LookingForPlayer);
    }

    private void ExitGlowingState()
    {
        golemAnim.SetBool("glowing", false);
    }

    private void EnterLookingForPlayerState()
    {
        // Implement looking for player state entry logic here
    }

    private void UpdateLookingForPlayerState()
    {
        // Implement looking for player state update logic here
        if (Vector2.Distance(transform.position, player.position) < rangeAttackRange)
        {
            SwitchState(State.PlayerDetected);
        }
    }

    private void ExitLookingForPlayerState()
    {
        // Implement looking for player state exit logic here
    }

    private void EnterPlayerDetectedState()
    {
        // Implement player detected state entry logic here
    }

    private void UpdatePlayerDetectedState()
    {
        // Implement player detected state update logic here
        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance < meleeAttackRange)
        {
            SwitchState(State.MeleeAttack);
        }
        else if (playerDistance < laserCastRange)
        {
            SwitchState(State.LaserCast);
        }
        else if (playerDistance < rangeAttackRange)
        {
            SwitchState(State.RangeAttack);
        }
        else
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitPlayerDetectedState()
    {
        // Implement player detected state exit logic here
        golemAnim.SetBool("MeleeAttack", false);
        golemAnim.SetBool("LaserCast", false);
        golemAnim.SetBool("RangeAttack", false);
    }

    private void EnterMovingState()
    {
        // Implement moving state entry logic here
    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, golemRb.velocity.y);
            golemRb.velocity = movement;
        }

        if (Vector2.Distance(transform.position, player.position) < meleeAttackRange)
        {
            SwitchState(State.PlayerDetected);
        }
    }

    private void ExitMovingState()
    {
        // Implement moving state exit logic here
        movement.Set(0, golemRb.velocity.y);
        golemRb.velocity = movement;
    }

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        golemRb.velocity = movement;
        golemAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        // Implement knockback state update logic here
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        // Implement knockback state exit logic here
        golemAnim.SetBool("Knockback", false);
        movement.Set(0, golemRb.velocity.y);
        golemRb.velocity = movement;
    }

    private void EnterDeadState()
    {
        // Implement dead state entry logic here
        Instantiate(deathChunkParticle, golem.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, golem.transform.position, deathBloodParticle.transform.rotation);
        isDead = true;
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {
        // Implement dead state update logic here
    }

    private void ExitDeadState()
    {
        // Implement dead state exit logic here
    }

    private void EnterImmuneState()
    {
        isImmune = true;
        // Implement immune state entry logic here
    }

    private void UpdateImmuneState()
    {
        // Implement immune state update logic here
        if (Time.time >= knockbackStartTime + immuneDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitImmuneState()
    {
        isImmune = false;
        // Implement immune state exit logic here
    }

    private void EnterArmorBuffState()
    {
        armor += 10;
        // Implement armor buff state entry logic here
    }

    private void UpdateArmorBuffState()
    {
        // Implement armor buff state update logic here
        if (Time.time >= knockbackStartTime + armorBuffDuration)
        {
            armor -= 10;
            SwitchState(State.Moving);
        }
    }

    private void ExitArmorBuffState()
    {
        // Implement armor buff state exit logic here
        armor -= 10;
    }

    private void EnterRangeAttackState()
    {
        golemAnim.SetBool("RangeAttack", true);
        // Implement range attack state entry logic here
    }

    private void UpdateRangeAttackState()
    {
        // Implement range attack state update logic here
        ShootProjectile();
        SwitchState(State.Moving);
    }

    private void ExitRangeAttackState()
    {
        golemAnim.SetBool("RangeAttack", false);
    }

    private void EnterLaserCastState()
    {
        golemAnim.SetBool("LaserCast", true);
        // Implement laser cast state entry logic here
    }

    private void UpdateLaserCastState()
    {
        // Implement laser cast state update logic here
        // Your laser cast logic
        SwitchState(State.Moving);
    }

    private void ExitLaserCastState()
    {
        golemAnim.SetBool("LaserCast", false);
    }

    private void EnterMeleeAttackState()
    {
        golemAnim.SetBool("MeleeAttack", true);
        // Implement melee attack state entry logic here
    }

    private void UpdateMeleeAttackState()
    {
        // Implement melee attack state update logic here
        // Your melee attack logic
        SwitchState(State.Moving);
    }

    private void ExitMeleeAttackState()
    {
        golemAnim.SetBool("MeleeAttack", false);
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCoolDown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = golem.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        golem.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        ExitCurrentState();
        EnterState(state);
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
}
