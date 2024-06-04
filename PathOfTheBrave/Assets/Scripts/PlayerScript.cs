using System.Collections;
using System.Collections.Generic;
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

    public GameObject portal;


    private void Awake()
    {
        
    }
    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
        
        moveSpeed = 10;

    }

    // Update is called once per frame
    void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * xDirection * Time.deltaTime;
        if ((transform.position.x < -8 && xDirection < 0) )
            return;
        float y = transform.position.y;
        transform.position = transform.position + new Vector3(moveStep, 0, 0);
    }

    [System.Obsolete]
    void OnSceneLoaded(int sceneIndex)
    {
        float x = 0, y = 0;
        switch (sceneIndex)
        {
            case 0:
                {
                    portal.active = false;
                    x = 0;
                    y = 0;
                    break;
                }
            case 1:
                {
                    portal.active = true;
                    x = 31.28f;
                    y = 0.73f;
                    break;
                }
            case 2:
                {
                    portal.active = true;
                    x = 76.55f;
                    y = 0.34f;
                    break;
                }
            case 3:
                {
                    portal.active = true;
                    x = 0;
                    y = 0;
                    break;
                }
            case 4:
                {
                    portal.active = true;
                    x = 0;
                    y = 0;
                    break;
                }
        }
        portal.transform.position = new Vector3(x, y, 0);
    }


    void SpawnPlayer(int sceneIndex)
    {
        float x = 0, y = 0;
        switch(sceneIndex)
        {
            case 0:
                {
                    this.gameObject.hideFlags = HideFlags.HideInInspector;
                    x = 0;
                    y = 0;
                    break;
                }
            case 1:
                {
                    this.gameObject.hideFlags = HideFlags.None;
                    x = -6.51f;
                    y = 1.58f;
                    break;
                }
            case 2:
                {
                    this.gameObject.hideFlags = HideFlags.None;
                    x = -10.78072f;
                    y = -0.3826588f;
                    break;
                }
            case 3:
                {
                    this.gameObject.hideFlags = HideFlags.None;
                    x = 0;
                    y = 0;
                    break;
                }
            case 4:
                {
                    this.gameObject.hideFlags = HideFlags.None;
                    x = 0;
                    y = 0;
                    break;
                }
        }
        this.gameObject.transform.position = new Vector3(x, y, 0);
        DataPersistenceManager instance = new DataPersistenceManager(hp, x, y, coin);
        instance.SaveGame(sceneIndex);

    }

    public void LoadScene(int sceneIndex)
    {
        Debug.Log("2");
        hp++;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        SpawnPlayer(sceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
                LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

    }

    


}
