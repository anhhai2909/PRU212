using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public ParticleSystem portal;
    public static PortalScript portalScript;
    public bool isEnabled;

    public bool isBossDead;
    
    
    [Obsolete]
    private void Awake()
    {
  
        portal.loop = true;
        portal.playOnAwake = false;
        portal.enableEmission = false;
    }

    void Start()
    {
        isBossDead = true;
        isEnabled = false;
        GameObject ball = GameObject.Find("Player");
        player = ball;
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        if(!portal.enableEmission)
        {
            if (portal != null && portal.gameObject != null)
            {
                if (portal.gameObject.transform.position.x - player.transform.position.x <= 10 && isBossDead)
                {
                    isEnabled = true;
                    portal.Play();
                    portal.enableEmission = true;

                }
            }

        }
        
    
    }
    
}
