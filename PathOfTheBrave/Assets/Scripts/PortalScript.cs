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

    private void Awake()
    {
        portal.loop = true;
        portal.playOnAwake = false;
        portal.enableEmission = false;
        DontDestroyOnLoad(player);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(portal.gameObject.transform.position.x - player.transform.position.x <= 10)
        {
            portal.Play();
            portal.enableEmission = true;

        }
    
    }






}
