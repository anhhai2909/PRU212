using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    Vector3 direction;
    public float speed = 2f;
    public float distanceLimit = 0f;
    public float destroyTime = 8f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        SetDestroyTime();
    }

    private void SetDestroyTime()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the fireball to the player
        direction = player.transform.position - transform.position;

        // Calculate the rotation angle from the movement direction
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Update the rotation of the fireball
        this.transform.rotation = Quaternion.Euler(0, 0, rot + 97);

        // Call the Follow method to move the fireball towards the player
        this.Follow();
    }

    void Follow()
    {
        // Calculate the distance between the fireball and the player
        Vector3 distance = this.player.transform.position - transform.position;

        // Determine the target point the fireball will move towards
        Vector3 targetPoint = this.player.transform.position - distance.normalized * distanceLimit;

        // Move the fireball towards the target point at a certain speed
        gameObject.transform.position =
            Vector3.MoveTowards(gameObject.transform.position, targetPoint, this.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
