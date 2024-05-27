using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled = true;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private int currentAttack;
    private int numOfAttack = 3;
    private bool gotInput;
    private bool hasNext = false;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    public bool isAttacking;

    private void Start()
    {
        currentAttack = 0;
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (combatEnabled)
            {
                if (isAttacking)
                {
                    if (Time.time <= (lastInputTime + inputTimer))
                    {
                        Debug.Log("Has next");
                        currentAttack = (currentAttack + 1) % numOfAttack;
                        hasNext = true;
                    }
                    else
                    {
                        hasNext = false;
                    }
                }
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            //Perform Attack
            if (!isAttacking)
            {
                isAttacking = true;
                anim.SetBool("attack_ground", true);
                anim.SetBool("isAttacking", isAttacking);
            }
            if (Time.time >= lastInputTime + inputTimer)
            {
                //Wait for new input
                gotInput = false;
                currentAttack = 0;
                hasNext = false;
            }
            else
            {
                hasNext = true;
            }
            anim.SetInteger("currentAttack", currentAttack);
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);
            //Instantiate hit particle
        }
    }

    private void FinishAttack1()
    {
        if (!hasNext)
        {
            gotInput = false;
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
            anim.SetBool("attack_ground", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

}
