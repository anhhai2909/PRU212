using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int value = 10;

    public GameObject coinPrefab;
    public float pushForce = 10f;

    public float disapearCooldown = 20;
    public float disapearTimer = 0;
    void Start()
    {

    }
    void Update()
    {
        if (disapearTimer >= 2f)
        {
            this.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
        }
        disapearTimer += Time.deltaTime;
        if (disapearTimer >= disapearCooldown)
        {
            Destroy(this.gameObject);
        }
    }

    public void Spawn(Transform transformPosition, int dropPercent, int luckPercent, int minAvr, int maxAvr, int minIfLuck, int maxIfLuck)
    {
        float randomNumber = Random.Range(0f, 10000f);
        int value = 1;
        if (randomNumber < dropPercent * 100)
        {
            float randomNumber2 = Random.Range(0f, 10000f);
            if (randomNumber2 < luckPercent * 100)
            {
                value = Random.Range(minIfLuck, maxIfLuck);
            }
            else
            {
                value = Random.Range(minAvr, maxAvr);
            }
        }
        for (int i = 0; i < value; i++)
        {
            GameObject c = Instantiate(coinPrefab, transformPosition.position, Quaternion.identity);
            c.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * pushForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(value);
            Debug.Log(collision.gameObject.GetComponent<PlayerScript>());
            collision.gameObject.GetComponent<PlayerScript>().AddCoint(value);
            Destroy(gameObject);
        }
    }
}
