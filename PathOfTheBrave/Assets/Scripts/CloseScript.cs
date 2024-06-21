using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameO;

    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(CloseOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseOnClick()
    {
        gameO.SetActive(false);
    }
}
