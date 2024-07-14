using Combat.Damage;
using ProjectileSystem.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MeteorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 20;
    public float destroyTime = 5f;
    public LayerMask whatDestroyMeteor;
    [SerializeField]
    private ParticleSystem groundParticle;
    [SerializeField]
    public ParticleSystem playerHitParticle;
    void Start()
    {
        SetDestroyTime();
    }

    private void SetDestroyTime()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((whatDestroyMeteor.value & (1 << collision.gameObject.layer)) > 0)
        {
            SpawnCollisionEffect(playerHitParticle);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            GameObject.Destroy(gameObject);

            if (collision.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
            }

            //if (collision.TryGetComponentInChildren(out IKnockBackable knockBackable))
            //{
            //    knockBackable.KnockBack(new Combat.KnockBack.KnockBackData(knockbackAngle,knockbackStrength, gameObject.GetComponent<EnemyMovement>().isFacingRight ? 1 : -1, gameObject));
            //}
        }
    }

    private void SpawnCollisionEffect(ParticleSystem particleEffect)
    {
        // Instantiate the particle system at the current position and rotation of the game object
        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }
    }
}
