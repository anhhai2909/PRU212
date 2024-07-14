using UnityEngine;

public class GolemController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Glowing,
        Moving,
        Immune,
        ArmorBuff,
        RangeAttack,
        LaserCast,
        MeleeAttack,
        Dead
    }

    private State currentState;

    [SerializeField] private float maxHealth, currentHealth;
    [SerializeField] private float meleeAttackRange, rangeAttackRange, laserCastRange;
    [SerializeField] private float immuneDuration, armorBuffDuration, glowingDuration, movementSpeed;
    [SerializeField] private float groundCheckDistance, wallCheckDistance;
    [SerializeField] private float touchDamage, lastTouchDamageTime, touchDamageCoolDown, touchDamageWidth, touchDamageHeight;

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
    private Vector2 movement, touchDamageBotLeft, touchDamageTopRight;
    private int facingDirection = 1, damageDirection;
    private float knockbackStartTime;
    public GameObject arm;
    public Transform armPos;
    private bool isFacingRight = true;

    private bool isDetected = false;
    [SerializeField] private float detectionRadius = 20f;

    private void Start()
    {
        golem = this.gameObject;
        golemRb = GetComponent<Rigidbody2D>();
        golemAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player").transform;
        SwitchState(State.Idle);
    }

    private void Update()
    {
        if (!isDead)
        {
            DetectPlayer();

            if (isDetected)
            {
                if (Vector2.Distance(player.position, transform.position) <= meleeAttackRange)
                {
                    SwitchState(State.MeleeAttack);
                }
                else if (Vector2.Distance(player.position, transform.position) <= rangeAttackRange)
                {
                    SwitchState(State.RangeAttack);
                }
                else
                {
                    SwitchState(State.Moving);
                }
            }

            switch (currentState)
            {
                case State.Idle:
                    UpdateIdle();
                    break;
                case State.Moving:
                    UpdateMoving();
                    break;
                case State.Glowing:
                    UpdateGlowing();
                    break;
                case State.Immune:
                    UpdateImmune();
                    break;
                case State.ArmorBuff:
                    UpdateArmorBuff();
                    break;
                case State.RangeAttack:
                    UpdateRangeAttack();
                    break;
                case State.MeleeAttack:
                    UpdateMeleeAttack();
                    break;
                case State.LaserCast:
                    UpdateLaserCast();
                    break;
                case State.Dead:
                    UpdateDead();
                    break;
            }
        }
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Idle:
                ExitIdle();
                break;
            case State.Moving:
                ExitMoving();
                break;
            case State.Glowing:
                ExitGlowing();
                break;
            case State.Immune:
                ExitImmune();
                break;
            case State.ArmorBuff:
                ExitArmorBuff();
                break;
            case State.RangeAttack:
                ExitRangeAttack();
                break;
            case State.MeleeAttack:
                ExitMeleeAttack();
                break;
            case State.LaserCast:
                ExitLaserCast();
                break;
            case State.Dead:
                ExitDead();
                break;
        }

        switch (state)
        {
            case State.Idle:
                EnterIdle();
                break;
            case State.Moving:
                EnterMoving();
                break;
            case State.Glowing:
                EnterGlowing();
                break;
            case State.Immune:
                EnterImmune();
                break;
            case State.ArmorBuff:
                EnterArmorBuff();
                break;
            case State.RangeAttack:
                EnterRangeAttack();
                break;
            case State.MeleeAttack:
                EnterMeleeAttack();
                break;
            case State.LaserCast:
                EnterLaserCast();
                break;
            case State.Dead:
                EnterDead();
                break;
        }

        currentState = state;
    }

    private void EnterIdle()
    {
        golemAnim.SetBool("idle", true);
    }

    private void UpdateIdle()
    {
        if (isDetected)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitIdle()
    {
        golemAnim.SetBool("idle", false);
    }

    private void EnterGlowing()
    {
        damage += 20;
        Invoke("EndGlowing", glowingDuration);
        golemAnim.SetBool("glowing", true);
    }

    private void UpdateGlowing()
    {
        // Glowing state logic here
    }

    private void ExitGlowing()
    {
        damage -= 20; // Reset damage
        golemAnim.SetBool("glowing", false);
    }

    private void EndGlowing()
    {
        SwitchState(State.Idle);
    }

    private void EnterMoving()
    {
        golemAnim.SetBool("moving", true);
    }

    private void UpdateMoving()
    {
        if (isDetected)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            movement = new Vector2(direction.x * movementSpeed, golemRb.velocity.y);
            golemRb.velocity = movement;

            groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

            if ((wallDetected || !groundDetected) && Mathf.Abs(player.position.x - transform.position.x) > 1f)
            {
                Flip();
            }
            else if ((facingDirection == 1 && player.position.x < transform.position.x) ||
                     (facingDirection == -1 && player.position.x > transform.position.x))
            {
                Flip();
            }
        }
        else
        {
            golemRb.velocity = new Vector2(0, golemRb.velocity.y);
            SwitchState(State.Idle);
        }
    }

    private void ExitMoving()
    {
        golemAnim.SetBool("moving", false);
    }

    private void EnterImmune()
    {
        isImmune = true;
        Invoke("EndImmunity", immuneDuration);
        golemAnim.SetBool("immune", true);
    }

    private void UpdateImmune()
    {
        // Logic for updating the Immune state
    }

    private void ExitImmune()
    {
        isImmune = false;
        golemAnim.SetBool("immune", false);
    }

    private void EndImmunity()
    {
        SwitchState(State.Idle);
    }

    private void EnterArmorBuff()
    {
        if (armor < 200)
        {
            armor += 50;
            Invoke("EndArmorBuff", armorBuffDuration);
        }
        golemAnim.SetBool("armorBuff", true);
    }

    private void UpdateArmorBuff()
    {
        // Logic for updating the ArmorBuff state
    }

    private void ExitArmorBuff()
    {
        golemAnim.SetBool("armorBuff", false);
    }

    private void EndArmorBuff()
    {
        armor -= 50;
        SwitchState(State.Idle);
    }

    private void EnterRangeAttack()
    {
        golemAnim.SetTrigger("rangeAttack");
    }

    private void UpdateRangeAttack()
    {
        // Logic for updating the RangeAttack state
        SwitchState(State.Idle);
    }

    private void ExitRangeAttack()
    {
        // Logic for exiting the RangeAttack state
    }

    private void EnterLaserCast()
    {
        golemAnim.SetTrigger("laserCast");
    }

    private void UpdateLaserCast()
    {
        // Logic for updating the LaserCast state
        SwitchState(State.Idle);
    }

    private void ExitLaserCast()
    {
        // Logic for exiting the LaserCast state
    }

    private void EnterMeleeAttack()
    {
        golemAnim.SetTrigger("meleeAttack");
    }

    private void UpdateMeleeAttack()
    {
        if (Vector2.Distance(player.position, transform.position) <= meleeAttackRange)
        {
            player.SendMessage("Damage", new float[] { damage, transform.position.x });
        }
        SwitchState(State.Idle);
    }

    private void ExitMeleeAttack()
    {
        // Logic for exiting the MeleeAttack state
    }

    private void EnterDead()
    {
        isDead = true;
        golemAnim.SetBool("dead", true);
        Invoke("HandleDeath", 1.5f);
    }

    private void UpdateDead()
    {
        // Logic for updating the Dead state
    }

    private void ExitDead()
    {
        // Logic for exiting the Dead state
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
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
                attackDetails[1] = transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Damage(float[] attackDetails)
    {
        if (!isImmune)
        {
            currentHealth -= attackDetails[0] - armor;
            if (attackDetails[1] > transform.position.x)
            {
                damageDirection = -1;
            }
            else
            {
                damageDirection = 1;
            }
            if (currentHealth > 0.0f)
            {
                golemAnim.SetTrigger("damaged");
                Knockback();
            }
            else if (currentHealth <= 0.0f)
            {
                SwitchState(State.Dead);
            }
        }
    }

    private void Knockback()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        golemRb.velocity = movement;
    }

    void DetectPlayer()
    {
        float range = Mathf.Abs(player.transform.position.x - this.gameObject.transform.position.x);

        if (range <= detectionRadius)
        {

            if ((player.transform.position.x > transform.position.x && !isFacingRight) ||
                (player.transform.position.x < transform.position.x && isFacingRight))
            {
                Flip();
            }

            isDetected = true;
        }
        else
        {
            isDetected = false;
        }

    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
