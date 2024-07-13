using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSword : MonoBehaviour
{
    public float startXPosition = 25f;
    public float endXPosition = -30f;
    public float speed = 5f; 
    private Transform playerTransform;
    private bool movingRight = false; 

    void Start()
    {
  
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            
            transform.position = new Vector3(startXPosition, playerTransform.position.y, transform.position.z);
            
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {

            float step = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            if (transform.position.x <= endXPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
