using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NecromancerController : MonoBehaviour
{
    //public int maxHealth;
    //public int currentHealth;
    //protected bool isAlive;
    //public int attackDamage;
    //public float attackRange;
    //public List<Skill> skills; 

    //public Image healthBarImage; 

    //public Animator animator;
    //public float moveSpeed = 2f;
    //public float jumpForce = 10f;
    //public Transform groundCheck;
    //public LayerMask groundLayer;
    //public Transform player;
    //private int currentSkillIndex = 0;

    //private Rigidbody2D rb;
    //private bool isFacingRight = true;
    //private bool isGrounded;

    //public void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").transform;
    //    rb = GetComponent<Rigidbody2D>();
    //    animator = GetComponent<Animator>();

    //    maxHealth = 300;
    //    attackDamage = 15;
    //    attackRange = 1.5f;
    //    currentHealth = maxHealth;
    //    UpdateHealthBar();
    //}

    //public void Update()
    //{
    //    // Check if the boss is on the ground
    //    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    //    if (isAlive)
    //    {
    //        // Perform boss actions based on game logic
    //        Flip();
    //    }
    //    else
    //    {
    //        animator.SetBool("isDead", true);
    //    }
    //}
    //public void UpdateHealthBar()
    //{
    //    if (healthBarImage != null)
    //    {
    //        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    //    }
    //}

    //void Flip()
    //{
    //    Vector2 target = new Vector2(player.position.x, rb.position.y);

    //    // Flip the boss sprite if it changes direction
    //    if ((target.x > rb.position.x && !isFacingRight) || (target.x < rb.position.x && isFacingRight))
    //    {
    //        // Switch the direction the boss is facing
    //        isFacingRight = !isFacingRight;

    //        // Flip the boss sprite horizontally
    //        Vector3 scale = transform.localScale;
    //        scale.x *= -1;
    //        transform.localScale = scale;
    //    }
    //}

    ////public override void Attack()
    ////{
    ////    // Implement boss attack logic here
    ////}
    //public void DestroyBoss()
    //{
    //    Destroy(gameObject);
    //    Debug.Log("Boss object destroyed");
    //}
    public int maxHealth = 300;
    public int currentHealth;
    protected bool isAlive = true;
    public int attackDamage = 15;
    public float attackRange = 1.5f;
    public List<Skill> skills;

    public Image healthBarImage;
    public Animator animator;
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform player;
    private int currentSkillIndex = 0;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isGrounded;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        UpdateHealthBar();
        StartCoroutine(Phase1());
    }

    IEnumerator Idle(float duration)
    {
        animator.Play("Idle");
        yield return new WaitForSeconds(duration);
    }

    IEnumerator Phase1()
    {
        while (currentHealth > maxHealth / 2)
        {
            yield return Idle(10f);
            Debug.Log("Executing SpawnSkeleton in Phase1");
            animator.Play("SpawnSkeleton");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return Idle(10f);
            Debug.Log("Executing Shoot in Phase1");
            animator.Play("Shoot");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return Idle(10f);
        }
        StartCoroutine(Phase2());
    }

    IEnumerator Phase2()
    {
        while (currentHealth > 0)
        {
            yield return Idle(10f);
            Debug.Log("Executing SpawnSkeleton in Phase2");
            animator.Play("SpawnSkeleton");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return Idle(10f);
            Debug.Log("Executing SpawnMeteor in Phase2");
            animator.Play("SpawnMeteor");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return Idle(10f);
            Debug.Log("Executing Shoot in Phase2");
            animator.Play("Shoot");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return Idle(10f);
            Debug.Log("Executing SpawnSpike in Phase2");
            animator.Play("SpawnSpike");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return Idle(10f);
        }

        StartCoroutine(Phase3());
    }

    IEnumerator Phase3()
    {
        Debug.Log("Necromancer is dead!");
        animator.Play("Death");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isAlive)
        {
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

        // Adjust the facing logic to flip the sprite correctly
        if ((target.x > rb.position.x && isFacingRight) || (target.x < rb.position.x && !isFacingRight))
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


    public void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
