using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatedScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;

    public Button openButton;

    public Canvas canvas;

    public float limitStart;

    public float limitEnd;

    private GameObject player;

    public Button closeButton;

    public bool isByClick;

    void Start()
    {
        player = GameObject.Find("Player");
        closeButton.onClick.AddListener(CloseOnClick);
        if(isByClick)
            openButton.onClick.AddListener(OpenOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isByClick)
        {
            if (player.transform.position.x >= limitStart && player.transform.position.x <= limitEnd)
            {
                if (canvas.gameObject.active == false)
                    image.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    image.gameObject.SetActive(false);
                    Time.timeScale = 0;
                    canvas.gameObject.SetActive(true);
                }
            }
            else
            {
                image.gameObject.SetActive(false);
            }
        }
       
    }

    void OpenOnClick()
    {
        Time.timeScale = 0;
        canvas.gameObject.SetActive(true);
    }

    void CloseOnClick()
    {
        Time.timeScale = 1;
        canvas.gameObject.SetActive(false);
        if(!isByClick)
            image.gameObject.SetActive(true);
    }
}
