using Combat.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class BoDSpell : MonoBehaviour
{
    public float disapearTime;
    private float disapearTimer;

    void Update()
    {
        disapearTimer += Time.deltaTime;
        if(disapearTimer >= disapearTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
