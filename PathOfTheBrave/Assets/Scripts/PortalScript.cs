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
    
    [Obsolete]
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (portalScript == null)
        {
            portalScript = this;
        }
        else
        {
            Destroy(portal);

        }

        portal.loop = true;
        portal.playOnAwake = false;
        portal.enableEmission = false;
    

    }

    void Start()
    {
      
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        if(!portal.enableEmission)
        {
            if (portal.gameObject.transform.position.x - player.transform.position.x <= 10)
            {
                portal.Play();
                portal.enableEmission = true;

            }

        }
        
    
    }
}
