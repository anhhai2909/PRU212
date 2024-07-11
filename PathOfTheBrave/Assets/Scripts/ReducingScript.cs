using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducingScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isHorizontal;

    private Vector3 oldLocalScale;

    public bool isActivated;


    void Start()
    {
        isActivated = false;
        oldLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if(isActivated)
        {
            if (isHorizontal)
            {
                if (transform.localScale.x >= 0)
                    Resize(-0.01f, new Vector3(1f, 0f, 0f));
            }
            else
            {
                if (transform.localScale.y >= 0)
                    Resize(-0.01f, new Vector3(0f, 1f, 0f));
            }
        }
        else
        {
            if (isHorizontal)
            {
                if (transform.localScale.x < oldLocalScale.x)
                    Resize(0.01f, new Vector3(1f, 0f, 0f));
            }
            else
            {
                if (transform.localScale.y < oldLocalScale.y)
                    Resize(0.01f, new Vector3(0f, 1f, 0f));
            }
        }
        

    }

    public void Resize(float amount, Vector3 direction)
    {
        transform.position += direction * amount / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
        transform.localScale += direction * amount; // Scale object in the specified direction
    }
}
