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

    public float moveSpeed;

    public float xDirection;

    public static PlayerScript instance;

    [SerializeField] Animator transitionAnim;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
            SpawnPlayer(scene.buildIndex);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            this.name = "Ball";
            instance = this;
        }
        else
        {
            Destroy(gameObject);
       
        }


    }

    void Start()
    {
        
        moveSpeed = 10;
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

        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * xDirection * Time.deltaTime;
        if ((transform.position.x < -8 && xDirection < 0) )
            return;
        float y = transform.position.y;
        transform.position = transform.position + new Vector3(moveStep, 0, 0);
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
                        x = -6.51f;
                        y = 1.58f;
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
            instance.SaveGame(sceneIndex);
        }
    }


    public void LoadScene(int sceneIndex)
    {
        hp++;
        LoadLevel(sceneIndex);
      //  SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            //LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 
        }

    }

    


}
