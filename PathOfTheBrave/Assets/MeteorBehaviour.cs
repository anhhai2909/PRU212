using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
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
            SpawnCollisionEffect(playerHitParticle);
            GameObject.Destroy(gameObject);
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
