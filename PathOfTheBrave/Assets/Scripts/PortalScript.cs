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

    public static PortalScript instance;



    [Obsolete]
    private void Awake()
    {
        portal.loop = true;
        portal.playOnAwake = false;
        portal.enableEmission = false;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(portal);
        }
        else
        {
            //Destroy(this.gameObject);
        }
        
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

    void OnEnable()
    {
        // Đăng ký sự kiện sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        float x = 0, y = 0;
        switch (scene.buildIndex)
        {
            case 0:
                {
                    x = 0;
                    y = 0;
                    break;
                }
            case 1:
                {
                    x = 31.28f;
                    y = 0.73f;
                    break;
                }
            case 2:
                {
                    x = 76.55f;
                    y = 0.34f;
                    break;
                }
            case 3:
                {
                    x = 0;
                    y = 0;
                    break;
                }
            case 4:
                {
                    x = 0;
                    y = 0;
                    break;
                }
        }
        portal.transform.position = new Vector3(x, y, 0);
        portal.enableEmission = false;
    }




}
