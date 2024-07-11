using narrenschlag.extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    Dictionary<int, int> activatedItems;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        GameObject oldParent = draggableItem.parentAfterDrag.gameObject;
        if (transform.childCount > 0)
        {
            Debug.Log("123d");
            GameObject old = transform.GetChild(0).gameObject;
            transform.GetChild(0).SetParent(oldParent.transform);
            draggableItem.parentAfterDrag = transform;
            ChangeActivatedItem(draggableItem.gameObject, old, oldParent);
        }
        else
        {
            draggableItem.parentAfterDrag = transform;
            ChangeActivatedItem(draggableItem.gameObject, null, oldParent);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void ChangeActivatedItem(GameObject newObject, GameObject oldObject, GameObject oldParent)
    {
        activatedItems = GameObject.Find("InventoryItems").GetComponent<InventoryScript>().activatedItems;
        if (activatedItems == null)
        {
            activatedItems = new Dictionary<int, int>();
        }
        int id = Convert.ToInt32(newObject.name);
        if (gameObject.name.Contains("Item") && !gameObject.name.Contains("Activated"))
        {
            Debug.Log(oldObject);
            if (oldObject.name.Contains("Item"))
            { 
                foreach (var item in activatedItems)
                {
                    if (item.Value == id)
                    {
                        activatedItems.Remove(item.Key);
                        break;
                    }
                }
            }
            else
            {
                foreach (var item in activatedItems)
                {
                    if (item.Value == id)
                    {
                        activatedItems[item.Key] = Convert.ToInt32(oldObject.name);
                        break;
                    }
                }
            }
        }
        else
        {
            int slot = Convert.ToInt32(gameObject.name.Replace("ActivatedItem", ""));
            if (oldObject == null)
            {
                if (!activatedItems.ContainsValue(id))
                {
                    activatedItems.Add(slot, id);
                }
                else
                {
                    activatedItems.Remove(Convert.ToInt32(oldParent.name.Replace("ActivatedItem", "")));
                    activatedItems[slot] = id;
                }
            }
            else
            {
                if (activatedItems.ContainsValue(id))
                {
                    activatedItems[Convert.ToInt32(oldParent.name.Replace("ActivatedItem", ""))] = activatedItems[slot];
                    activatedItems[slot] = id;
                }
                else
                {
                    activatedItems[slot] = id;
                }

            }
        }
        GameObject.Find("InventoryItems").GetComponent<InventoryScript>().activatedItems = activatedItems;
        LoadDataScript.SaveActivatedItems(activatedItems);

        GameObject inprogressActivatedItem = GameObject.Find("ActivatedItemInGame");
        inprogressActivatedItem.GetComponent<InprogressActivatedItem>().LoadData();
        /*
        string a = "";
        if (activatedItems != null) {
            foreach (var item in activatedItems)
            {
                a += item.Key + ":" + item.Value + " ";
            }
        }
        Debug.Log(a);
        */
    }
}
