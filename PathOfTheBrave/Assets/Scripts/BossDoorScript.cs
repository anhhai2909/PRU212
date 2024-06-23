using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isKeyTaken;

    private GameObject player;

    private GameObject key;

    private GameObject door;

    void Start()
    {
        isKeyTaken = false;
        player = GameObject.Find("Player");
        key = GameObject.Find("BossKey");
        door = GameObject.Find("BossDoor");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            if (gameObject.name == "BossKey")
            {
                key.SetActive(false);
                isKeyTaken = true;
            }
            
        }
    }
}
