using Combat.Damage;
using UnityEngine;
using Utilities;

public class GroundRangeAttackStuff : MonoBehaviour
{
    public GameObject RespawnPosition;
    public GameObject player;
    public Rigidbody2D rb;
    public float speed;
    public GameObject enemy;
    public Animator anim;
    private GameObject stuffprefab;
    public float destroyTimer = 0;
    public float damage;

    private void Start()
    {
        stuffprefab = this.gameObject;
        RespawnPosition = GameObject.FindGameObjectWithTag("GRAttackStuffResPosition");
        player = GameObject.FindGameObjectWithTag("Player");
        if (enemy.GetComponent<IsFacingRight>().facingRight == false)
        {
            Flip();
        }

        //if (player.transform.position.x < gameObject.transform.position.x)
        //{
        //    Flip();
        //}
        rb.velocity = new Vector2(speed, 0);
    }
    void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > 5)
        {
            Destroy(gameObject);
            destroyTimer = 0;
        }
    }
    void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log(enemy.gameObject.name + " take " + damage + " damage");
            if (collision.gameObject.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
                //Core.GetCoreComponent<DamageReceiver>().Damage(new Combat.Damage.DamageData(currentAttackData.Amount, item.gameObject));
            }
        }
        Destroy(gameObject);
        anim.SetTrigger("Explore");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log(enemy.gameObject.name + " take " + damage + " damage");
            if (collision.gameObject.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(new Combat.Damage.DamageData(damage, gameObject));
                //Core.GetCoreComponent<DamageReceiver>().Damage(new Combat.Damage.DamageData(currentAttackData.Amount, item.gameObject));
            }
        }
        Destroy(gameObject);
        anim.SetTrigger("Explore");
    }
}
