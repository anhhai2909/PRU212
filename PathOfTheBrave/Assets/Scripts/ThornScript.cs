using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    public bool isUp;

    private bool isCollide;

    private float speed;

    public GameObject squareUp;

    public GameObject squareDown;

    public float limitX;

    public float limitY;

    private GameObject player;

    private bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isStart = false;
        speed = 0.31f;
    }

    // Update is called once per frame
    void Update()
    {
        if (squareUp.transform.position.y - squareDown.transform.position.y >= 1.4f)
        {
            isCollide = true;
        }

        if (!isStart)
        {
            if (player.transform.position.x >= limitX && Math.Abs(player.transform.position.y - limitY) <= 5)
            {
                isStart = true;
            }
        }

        if (!isCollide && isStart)
        {
            if (isUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Var");
        }
    }


}
