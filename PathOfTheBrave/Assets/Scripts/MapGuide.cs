using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGuide : MonoBehaviour
{
    private string guideText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void LoadGuide()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                {
                    guideText = "Dodge obstacles and conquer monsters to earn rewards.";
                    break;
                }
            case 3:
                {
                    guideText = "Dodge obstacles and conquer monsters to earn rewards.\n\nDodge lightning bolts.\n\nThe longer you play, the more lightning bolts will strike.";
                    break;
                }
            case 4:
                {
                    guideText = "Dodge obstacles and conquer monsters to earn rewards.\n\nBeware of the toxic fog..\n\nThe longer you play, the thicker fog become.";
                    break;
                }
            case 5:
                {
                    guideText = "Dodge obstacles and conquer monsters to earn rewards.\n\nEach level has its own unique mechanics. Be prepared.";
                    break;
                }
            default:
                {
                    guideText = "";
                    break;
                }

        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != 0)
            LoadGuide();
    }

    void PlayGuide()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject guide = GameObject.Find("Guide");
            GameObject.Find("GuideText").GetComponent<TMP_Text>().text = guideText;
            guide.GetComponent<Animator>().Play("Guide", 0, 0);
        }
    }
}
