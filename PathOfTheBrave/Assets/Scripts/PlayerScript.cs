using Assets.Scripts.DataPersistence.Data;
using Cinemachine;
using QuantumTek.EncryptedSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;

    public float coin;

    public float xSpawn = 0;

    public float ySpawn = 0;


    public float xDirection;

    public static PlayerScript instance;

    public PlayerAfterImagePool afterImagePool;

    [SerializeField] Animator transitionAnim;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

  

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(gameObject);
        GameObject playerCamera = GameObject.Find("Player Camera");
        if (playerCamera != null && playerCamera.GetComponent<CinemachineVirtualCamera>() != null)
            playerCamera.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
        if (scene.buildIndex != 0)
            SpawnPlayer(scene.buildIndex);
    }

    

    private void Awake()
    {
        hp = 10;
        coin = 2000;
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            this.name = "Player";
            instance = this;
        }
        else
        {
            Destroy(gameObject);
       
        }


    }

    void Start()
    {
    }

    void LoadLevel(int sceneIndex)
    {
        float timer = 0;
        timer = Time.deltaTime;
        transitionAnim.SetTrigger("End");
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        transitionAnim.SetTrigger("Start");
    }

    // Update is calledL once per frame
    void Update()
    {

       
    }



    void SpawnPlayer(int sceneIndex)
    {
        if (sceneIndex != 0)
        {


            float x = 0, y = 0;
            switch (sceneIndex)
            {

                case 1:
                    {
                        x = -36.5f;
                        y = 2.5f;
                        break;
                    }
                case 2:
                    {
                        x = -16f;
                        y = -1.5f;
                        break;
                    }

                case 3://Scene 2
                    {
                        x = -13f;
                        y = 0.3f;
                        break;
                    }
                case 4://Scene 3
                    {
                        x = -13f;
                        y = 0.3f;
                        break;
                    }
                case 5://Scene 4
                    {
                        x = -10f;
                        y = -3f;
                        break;
                    }
                case 6://Scene 5
                    {
                        x = -3.902224f;
                        y = -1.111056f;
                        break;
                    }
                case 7:
                    {
                        x = -20.5f;
                        y = -2.65f;
                        break;
                    }
                case 8:
                    {
                        x = 34.4f;
                        y = -26f;
                        break;
                    }
            }
            this.gameObject.transform.position = new Vector3(x, y, 0);
            DataPersistenceManager instance = new DataPersistenceManager(hp, x, y, coin);
            instance.SaveGame(sceneIndex, GetAllScenes());
        }
    }

    List<SceneInfor> GetAllScenes()
    {
        List<SceneInfor> scenes = new List<SceneInfor>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool isCompleted = false;
            if(i <= SceneManager.GetActiveScene().buildIndex - 1)
            {
                isCompleted = true;
            }
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            SceneInfor scene = new SceneInfor(i, path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1), isCompleted);
            scenes.Add(scene);
        }
        return scenes;
    }



    public void LoadScene(int sceneIndex)
    {
        hp++;
        LoadLevel(sceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            if(collision.gameObject.GetComponent<PortalScript>().isEnabled)
            {
                if (SceneManager.sceneCountInBuildSettings <= SceneManager.GetActiveScene().buildIndex + 1)
                {
                    LoadLevel(0);
                }
                else
                {
                    LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

                }
            }
            

        }

    }

    


}
