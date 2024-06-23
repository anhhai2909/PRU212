using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Boss : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    protected bool isAlive;
    public int attackDamage;
    public float attackRange;
    public List<Skill> skills; // Danh sách các kỹ năng cho boss

    public Image healthBarImage; // Tham chiếu tới Image UI (Fill)

    public Animator animator;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        UpdateHealthBar();
    }

    public virtual void TakeDamage(int amount)
    {
        if (!isAlive) return;

        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isAlive = false;

        if (animator != null)
        {
            animator.SetTrigger("dead");
            Debug.Log("Animator isDead parameter set to true");
        }
        else
        {
            Debug.LogError("Animator component not found when trying to set isDead");
        }

        Debug.Log("Boss died");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeDamage(20);
        }
    }

    public abstract void Attack();

    public void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
    public abstract void DestroyBoss();
}
