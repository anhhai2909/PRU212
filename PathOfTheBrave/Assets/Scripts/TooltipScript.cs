using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipScript : MonoBehaviour
{
    private static TooltipScript instance;


    // Start is called before the first frame update
    private TMP_Text tooltipText;

    private RectTransform backgroundRectTransform;

    public Camera uiCamera;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = GameObject.Find("BackgroundTooltip").GetComponent<RectTransform>();
        tooltipText = GameObject.Find("TextTooltip").GetComponent<TMP_Text>();
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = new Vector2(localPoint.x + 40, localPoint.y - 25);
        if(transform.localPosition.y <= -24)
        {
            transform.localPosition = new Vector2(localPoint.x + 40, localPoint.y + 30);

        }
        if (transform.localPosition.x >= 32)
        {
            transform.localPosition = new Vector2(transform.localPosition.x - 30, transform.localPosition.y);

        }
    }

    private void ShowTooltip(string tooltip)
    {
        gameObject.SetActive(true);
        tooltipText.text = tooltip;

      
    }

   

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltip)
    {
        instance.ShowTooltip(tooltip);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
