using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Death");
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
