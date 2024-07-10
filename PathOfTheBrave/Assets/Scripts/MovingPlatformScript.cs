using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform platform;

    public Transform startPoint;

    public Transform endPoint;

    public Transform playerTransform;

    public float speed;

    public bool isFirstPlatform;

    public bool isMoving;

    int direction = 1;

    bool isPlayerOn = false;

    public MovingPlatformScript firstPlatform;



    public PlayerScript playerScript;

    private float distanceTurn;

    void Start()
    {
        distanceTurn = 0.1f;
        isMoving = false;
        speed = 1.35f;
        playerTransform = GameObject.Find("Player").transform;
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = currentMovementTarget();

        if(isFirstPlatform)
        {
            if(isMoving)
            {
                platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime );

            }
        }
        else
        {
            if(firstPlatform.isMoving)
            {
                platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime );

            }
        }

        //if(isPlayerOn ) 
        //playerTransform.position = Vector2.Lerp(playerTransform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;
        if(distance <= distanceTurn)
        {
            direction *= -1;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(isFirstPlatform && !isMoving)
            {
                isMoving = true;
            }
            isPlayerOn = true;
            collision.gameObject.transform.parent = this.transform;
            //collision.gameObject.GetComponent<Animator>().applyRootMotion = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
            collision.gameObject.transform.parent = null;
           // collision.gameObject.GetComponent<Animator>().applyRootMotion = true;

        }
    }

    private void OnDrawGizmos()
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.transform.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.transform.position);
        }
    }
}
