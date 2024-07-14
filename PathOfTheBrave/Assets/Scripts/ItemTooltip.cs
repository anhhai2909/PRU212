using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


public class ItemTooltip : MonoBehaviour
{
    // Start is called before the first frame update
    private string tooltip;

    public List<string> tooltips;

    public GameObject tooltipObject;
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if(IsPointerOverUIElement())
        {
            tooltipObject.SetActive(true);
            TooltipScript.ShowTooltip_Static(tooltip);
        }
        else
        {
            tooltipObject.SetActive(false);
            TooltipScript.HideTooltip_Static();
        }
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == 5 && curRaysastResult.gameObject.tag == "Item")
            {
                int id = Convert.ToInt32(curRaysastResult.gameObject.name);
                
                tooltip = tooltips[id - 1];
                return true;
            }
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
  

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        tooltipObject.SetActive(true);
        TooltipScript.ShowTooltip_Static(tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipObject.SetActive(false);
        TooltipScript.HideTooltip_Static();
    }
    */
}
