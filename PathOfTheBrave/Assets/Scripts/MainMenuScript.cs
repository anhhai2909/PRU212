using System.Collections;
using System.Collections.Generic;
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



    [System.Obsolete]
    void Start()
    {
        player.active = false;
        playerScript.gameObject.SetActive(false);

        newBtn.onClick.AddListener(NewClick);
        loadBtn.onClick.AddListener(LoadClick);
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
            playerScript.hp = gameData._hp;
            playerScript.coin = gameData._coin;
            playerScript.LoadScene(gameData._sceneIndex);
            
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
                playerScript.LoadScene(gameData._sceneIndex);
                playerScript.hp = gameData._hp;
                playerScript.coin = gameData._coin;
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
