using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public GameObject RespawnPosition;
    public GameObject player;
    public Rigidbody2D rb;
    public float speed;
    public GameObject enemy;

    public float destroyTimer = 0;

    private void Start()
    {
        RespawnPosition = GameObject.FindGameObjectWithTag("FireballRespawnPosition");
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - RespawnPosition.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }
    void Update()
    {
        destroyTimer += Time.deltaTime;
        if(destroyTimer > 5)
        {
            Destroy(gameObject);
            destroyTimer = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealthSystem>().TakeDamage(enemy.GetComponent<FlyingEnemyAttack>().damage);
        }
            Destroy(gameObject);
    }
}
