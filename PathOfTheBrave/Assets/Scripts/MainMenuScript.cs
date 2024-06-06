using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float xDirection;

    public int option = 1;

    public bool check = false;

    public float begin = -1;

    public Button newBtn;

    public Button loadBtn;

    public Button settingBtn;

    public TMP_Text newText;

    public TMP_Text loadText;

    public TMP_Text settingText;

    public GameObject canvas;

    public PlayerScript playerScript;

    public GameObject player;

    private void Awake()
    {
        DataPersistenceManager data = new DataPersistenceManager();
        if (data.ReadFromFile() == null)
        {
            Debug.Log(data.ReadFromFile());
            loadText.overrideColorTags = true;
            loadText.color = Color.gray;
        }
        else
        {
            loadBtn.onClick.AddListener(LoadClick);

        }
    }

    [System.Obsolete]
    void Start()
    {
        List<GameObject> objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Ball").ToList();
        if(objects.Count > 1 )
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (!objects[i].active)
                {
                }
                else
                {
                    this.player = objects[i];
                    this.playerScript = objects[i].GetComponent<PlayerScript>();
                }
            }
        }
        
        //0C0C0C

        player.active = false;
        playerScript.gameObject.SetActive(false);

        newBtn.onClick.AddListener(NewClick);
        settingBtn.onClick.AddListener(SettingClick);
    }

    [System.Obsolete]
    void NewClick()
    {
        if (option == 1)
        {
            player.active = true;
            playerScript.gameObject.SetActive(true);
            canvas.active = false;
            playerScript.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            option = 1;
            ChangeOption();
        }
    }

    [System.Obsolete]
    void LoadClick()
    {
        if (option == 2)
        {
            player.active = true;
            playerScript.gameObject.SetActive(true);
            canvas.active = false;
            DataPersistenceManager data = new DataPersistenceManager();
            GameData gameData = data.LoadGame();
            if(gameData == null)
            {
                playerScript.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
            else
            {
                playerScript.hp = gameData._hp;
                playerScript.coin = gameData._coin;
                playerScript.LoadScene(gameData._sceneIndex);
            }
            
            
        }
        else
        {
            option = 2;
            ChangeOption();
        }
    }

    void SettingClick()
    {
        option = 3;
        ChangeOption();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Time.realtimeSinceStartup - begin > 0.2)
        {
            xDirection = Input.GetAxisRaw("Vertical");
            if (xDirection != 0)
            {
                begin = Time.realtimeSinceStartup;
                MoveOption(xDirection);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (option == 1)
            {
                player.active = true;
                playerScript.gameObject.SetActive(true);
                canvas.active = false;
                playerScript.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (option == 2)
            {
                player.active = true;
                playerScript.gameObject.SetActive(true);
                canvas.active = false;
                DataPersistenceManager data = new DataPersistenceManager();
                GameData gameData = data.LoadGame();
                if (gameData == null)
                {
                    playerScript.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                }
                else
                {
                    playerScript.hp = gameData._hp;
                    playerScript.coin = gameData._coin;
                    playerScript.LoadScene(gameData._sceneIndex);
                }
            }
            else
            {

            }
        }


    }


    void MoveOption(float dir)
    {
        if (dir == -1)
        {
            option++;
            if (option > 3)
            {
                option = 1;
            }
        }
        else
        {
            option--;
            if (option < 1)
            {
                option = 3;
            }
        }
        ChangeOption();

    }

    void ChangeOption()
    {
        switch (option)
        {
            case 1:
                {
                    newBtn.image.enabled = true;
                    newText.color = Color.black;
                    newText.fontStyle = FontStyles.Bold;

                    loadBtn.image.enabled = false;
                    loadText.color = Color.white;
                    loadText.fontStyle = FontStyles.Normal;

                    settingBtn.image.enabled = false;
                    settingText.color = Color.white;
                    settingText.fontStyle = FontStyles.Normal;
                    break;
                }
            case 2:
                {
                    newBtn.image.enabled = false;
                    newText.color = Color.white;
                    newText.fontStyle = FontStyles.Normal;

                    loadBtn.image.enabled = true;
                    loadText.color = Color.black;
                    loadText.fontStyle = FontStyles.Bold;

                    settingBtn.image.enabled = false;
                    settingText.color = Color.white;
                    settingText.fontStyle = FontStyles.Normal;
                    break;
                }
            case 3:
                {
                    newBtn.image.enabled = false;
                    newText.color = Color.white;
                    newText.fontStyle = FontStyles.Normal;

                    loadBtn.image.enabled = false;
                    loadText.color = Color.white;
                    loadText.fontStyle = FontStyles.Normal;

                    settingBtn.image.enabled = true;
                    settingText.color = Color.black;
                    settingText.fontStyle = FontStyles.Bold;
                    break;
                }
        }
    }



}
