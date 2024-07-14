

using Combat.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;


    private Vector2 spawnPoint;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }


    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
    //TODO:Add damage to FireBall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Destroy(gameObject);

            if (collision.gameObject.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
            }
        }
    }
}
