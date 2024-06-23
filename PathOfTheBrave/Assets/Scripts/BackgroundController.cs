using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Start is called before the first frame update
    private float startPos;

    public GameObject cam;

    public float parallaxEffect;
    void Start()
    {
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect; // 0 move with camera ; 1 not move

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
