using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionScript : MonoBehaviour
{
    public int value = 30;

    public GameObject potionPrefab;
    public float pushForce = 10f;

    public float disapearCooldown = 20;
    public float disapearTimer = 0;
    public float spawnPercent = 10f;

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
            GameObject p = Instantiate(potionPrefab, transformPosition.position, Quaternion.identity);
            p.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * pushForce, ForceMode2D.Impulse);
        }
    }
}
