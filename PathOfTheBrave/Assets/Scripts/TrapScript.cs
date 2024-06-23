using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isCircleCollider;

    public Collider2D collider;

    public bool isRunnable;

    public float xActivated;

    public float yActivated;

    public float xDeactivated;

    private GameObject player;

    private bool isRun;

    public float speed;

    void Start()
    {
        speed = 0.03f;
        isRun = false;
        player = GameObject.Find("Player");
        if (isCircleCollider)
        {
            collider = gameObject.GetComponent<CircleCollider2D>();   
        }
        else
        {
            collider = gameObject.GetComponent<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunnable)
        {
            if (Math.Abs(player.transform.position.y - yActivated) <= 0.3F && Math.Abs(player.transform.position.x - xActivated) <= 6F)
            {
                isRun = true;
            }
            if (isRun)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
                if (transform.position.x >= xDeactivated)
                {
                    this.gameObject.SetActive(false);
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isCircleCollider)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Death");
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    Vector2 point = contact.point;

                    Vector2 trapCenter = collider.bounds.center;
                    float trapTop = collider.bounds.max.y;
                    float trapLeft = collider.bounds.min.x;
                    float trapRight = collider.bounds.max.x;

                    if (point.y >= trapTop && point.x > trapLeft && point.x < trapRight)
                    {
                        Debug.Log("Death");
                    }
                    else
                    {
                        Debug.Log("Death");
                    }
                }
            }
        }
    }
}
