using System.Collections;
using System.Collections.Generic;
using Combat.Damage;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private float maxHealth;
    private float health;
    private Animator anim;


    public void Damage(DamageData data)
    {
        Debug.Log(data.Amount + " Damage taken");
        health -= data.Amount;
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        anim.SetTrigger("damage");
        if(health <= 0)
        {
            health = maxHealth;
            //Destroy(gameObject);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
    }
}
