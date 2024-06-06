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

    void Start()
    {
        newBtn.onClick.AddListener(NewClick);
        loadBtn.onClick.AddListener(LoadClick);
        settingBtn.onClick.AddListener(SettingClick);
    }

    void NewClick()
    {
        if (option == 1)
        {
            canvas.active = false;
            SceneManager.LoadScene("InsideCastle", LoadSceneMode.Single);
        }
        else
        {
            option = 1;
            ChangeOption();
        }
    }

    void LoadClick()
    {
        if (option == 2)
        {
            canvas.active = false;
            DataPersistenceManager data = new DataPersistenceManager();
            data.LoadGame();
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
            if(option == 1)
            {
                canvas.active = false;
                SceneManager.LoadScene("InsideCastle", LoadSceneMode.Single);
            }
            else if(option == 2)
            {
                canvas.active = false;
                SceneManager.LoadScene("Jungle", LoadSceneMode.Single);
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
