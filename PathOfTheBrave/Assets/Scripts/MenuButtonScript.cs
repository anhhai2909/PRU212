using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
    public GameObject menu;

    public Button menuButton;

    public Button homeButton;

    public Button optionButton;

    public Button quitButton;

    public Button resumeButton;

    
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            resumeButton.onClick.AddListener(ResumeOnClick);
            menuButton.onClick.AddListener(MenuOnClick);
            homeButton.onClick.AddListener(QuitOnClick);
            quitButton.onClick.AddListener(ResumeOnClick);
            optionButton.onClick.AddListener(OptionOnClick);
            
        } catch (Exception e)
        {
            Debug.Log(e);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void QuitOnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    void OptionOnClick()
    {
    }

    void MenuOnClick()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    void ResumeOnClick()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }
}