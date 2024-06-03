using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;
    private Core core;
    private Stats stats;
    private Animator anim;

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");

        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if(stats == null) stats = core.GetCoreComponent(ref stats);
        stats.DecreaseHealth(amount);
        if (stats.isAlive())
        {
            anim.SetTrigger("damage");
        }
        else
        {
            anim.SetBool("dead", true);
            gameObject.layer = LayerMask.NameToLayer("Dead");
        }
        
        //Destroy(gameObject);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        core = GetComponentInChildren<Core>();
        stats = core.GetCoreComponent(ref stats);
    }
}
