using Assets.Scripts.DataPersistence.Data;
using Cinemachine;
using QuantumTek.EncryptedSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp = 10;

    public float coin = 10;

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
        GameObject playerCamera = GameObject.Find("Player Camera");
        if (playerCamera != null && playerCamera.GetComponent<CinemachineVirtualCamera>() != null)
            playerCamera.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
        if (scene.buildIndex != 0)
            SpawnPlayer(scene.buildIndex);
    }

    private void Awake()
    {
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
                        x = -6.91f;
                        y = -0.1f;
                        break;
                    }
                case 2:
                    {
                        x = -10.78072f;
                        y = -0.3826588f;
                        break;
                    }
                case 3:
                    {
                        x = -8.7f;
                        y = -2.394825f;
                        break;
                    }
                case 4:
                    {
                        x = -6.661118f;
                        y = 2.613495f;
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
