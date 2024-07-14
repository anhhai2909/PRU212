using Combat.Damage;
using ProjectileSystem.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SpikeBehaviour : MonoBehaviour
{
    public int damage = 20;
    public float destroyTime = 3f;
    private void Start()
    {
        SetDestroyTime();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Spike Hitted");
            if (collision.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
            }
        }
    }

    public void destroySpike()
    {
        Destroy(gameObject);
    }

    private void SetDestroyTime()
    {
        Destroy(this.gameObject, destroyTime);
    }
}
