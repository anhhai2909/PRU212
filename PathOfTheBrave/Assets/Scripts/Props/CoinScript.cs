using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int value;

    public GameObject coinPrefab;
    public float pushForce = 10f;

    public float disapearCooldown = 20;
    public float disapearTimer = 0;
    public float spawnPercent = 30f;

    public float lowValuecoinPercent = 60f;
    public float highValuecoinPercent = 10f;
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
    public void Spawn(Transform transformPosition)
    {
        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber < spawnPercent)
        {
            GameObject c = Instantiate(coinPrefab, transformPosition.position, Quaternion.identity);
            c.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * pushForce, ForceMode2D.Impulse);
            float randomNumber2 = Random.Range(0f, 100f);
            if (randomNumber2 <= 100f && randomNumber2 >= lowValuecoinPercent)
            {
                c.GetComponent<CoinScript>().value = 1;
            }
            else if (randomNumber2 > highValuecoinPercent && randomNumber2 < lowValuecoinPercent)
            {
                c.GetComponent<CoinScript>().value = 5;
            }
            else
            {
                c.GetComponent<CoinScript>().value = 10;
            }
            Debug.Log(c.GetComponent<CoinScript>().value);
        }
    }
}
