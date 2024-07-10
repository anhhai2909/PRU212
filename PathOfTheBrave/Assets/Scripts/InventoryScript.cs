using Assets.Scripts.DataPersistence.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<GameItem> items;

    public Dictionary<int, int> playerItems;

    public Texture2D cursor;

    public TMP_Text descriptionTitle;

    public TMP_Text descriptionText;

    public TMP_Text coinTitle;

    public TMP_Text coinText;

    public Dictionary<int, int> activatedItems;


    void OnEnable()
    {
        LoadDataScript.LoadPlayerData();
        playerItems = LoadDataScript.items;
        activatedItems = LoadDataScript.activatedItems;
        string a = "";
        if (activatedItems != null)
        {
            foreach (var item in activatedItems)
            {
                a += item.Key + ":" + item.Value + " ";
            }
        }
        Debug.Log(a);
        LoadAllItem();
        LoadPlayerItem();
        LoadPlayerItem2();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForActivatedItem();
    }

    private void LoadAllItem()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("JSON\\Item");
        items = JsonConvert.DeserializeObject<List<GameItem>>(jsonData.text);
    }

    private void LoadPlayerItem()
    {
        int index = 1;
        if (playerItems != null)
        {
            foreach (var item in playerItems)
            {
                GameObject playerItem = GameObject.Find("Item" + index + "Image");
                if (playerItem != null)
                {
                    playerItem.GetComponent<Image>().color = new Color(255, 255, 255, 255);


                    playerItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[item.Key - 1].SpriteName);
                    playerItem.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);

                    playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().text = item.Value.ToString();
                    playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);

                    playerItem.AddComponent<ChangeCursorScript>();
                    playerItem.GetComponent<ChangeCursorScript>().cursorTexture = cursor;

                    playerItem.AddComponent<DraggableItem>();
                    playerItem.GetComponent<DraggableItem>().image = playerItem.GetComponent<Image>();
                    playerItem.GetComponent<DraggableItem>().childImage = playerItem.transform.Find("ItemImage").GetComponent<Image>();


                    playerItem.AddComponent<Button>();
                    playerItem.GetComponent<Button>().onClick.AddListener(() => DisplayDescription(item.Key - 1));
                    playerItem.GetComponent<Button>().onClick.AddListener(() => DisplayDescription(item.Key - 1));
                    playerItem.name = (item.Key).ToString();

                    if (activatedItems != null)
                    {
                        if (activatedItems.ContainsValue(item.Key))
                        {
                            foreach (var activatedItem in activatedItems)
                            {
                                if (activatedItem.Value == item.Key)
                                {
                                    GameObject activatedSlot = GameObject.Find("ActivatedItem" + activatedItem.Key);
                                    playerItem.transform.parent = activatedSlot.transform;
                                }
                            }
                           
                        }
                    }
                }
                index++;
            }
        }

    }

    private void LoadPlayerItem2()
    {
        foreach (var item in playerItems)
        {
            GameObject playerItem = GameObject.Find(item.Key.ToString());
            if (playerItem != null)
            {
                playerItem.GetComponent<Image>().color = new Color(255, 255, 255, 255);


                playerItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[item.Key - 1].SpriteName);
                playerItem.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);

                playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().text = item.Value.ToString();
                playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);


            }
        }
    }

    void CheckForActivatedItem()
    {
        if (activatedItems != null)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (!activatedItems.ContainsKey(i))
                {
                    GameObject activatedSlot = GameObject.Find("ActivatedItem" + i);
                    if (activatedSlot.transform.childCount > 0)
                        Destroy(activatedSlot.transform.GetChild(0).gameObject);
                }
            }
        }
    }

    void DisplayDescription(int id)
    {
        descriptionTitle.text = "Description: ";
        descriptionText.text = "- " + items[id].Description;
        coinTitle.text = "Coin: ";
        coinText.text = "- " + items[id].Coin + " coin";
    }

}
