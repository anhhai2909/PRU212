using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyTime = 5f;
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
        if (collision.CompareTag("Player"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
