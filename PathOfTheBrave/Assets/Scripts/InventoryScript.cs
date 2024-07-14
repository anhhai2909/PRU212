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
        LoadAllItem();
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
        LoadDataScript.LoadPlayerData();
        playerItems = LoadDataScript.items;
        activatedItems = LoadDataScript.activatedItems;
        LoadPlayerItem();
        LoadPlayerItem2();
        //        Debug.Log(a);

    }

    // Start is called before the first frame update
    void Start()
    {
        
        LoadAllItem();
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
                
                GameObject playerItem = GameObject.Find("Item" + item.Key + "Image");
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
                                    Transform g = activatedSlot.transform.GetChild(0);
                                    Transform p = playerItem.transform.parent;
                                    playerItem.transform.parent = activatedSlot.transform;
                                    g.transform.parent = p.transform;
                                }
                            }
                           
                        }
                    }
                }
                else
                {
                    
                    playerItem = GameObject.Find(item.Key.ToString());
                    if (playerItem != null)
                    {
                        if (activatedItems.ContainsValue(item.Key))
                        {
                            playerItem.GetComponent<Image>().color = new Color(255, 255, 255, 255);


                            playerItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[item.Key - 1].SpriteName);
                            playerItem.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);

                            playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().text = item.Value.ToString();
                            playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
                        }
                        else
                        {
                            Debug.Log(item.Key);
                            Debug.Log(playerItem.transform.parent.name);
                            if(!playerItem.transform.parent.name.Contains("Activated"))
                            {
                                playerItem.GetComponent<Image>().color = new Color(255, 255, 255, 255);


                                playerItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[item.Key - 1].SpriteName);
                                playerItem.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);

                                playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().text = item.Value.ToString();
                                playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
                            }
                            else
                            {
                                int cnt = 1;
                                while (cnt < 5)
                                {
                                    playerItem = GameObject.Find("Item" + cnt + "ImageK");
                                    if (playerItem != null)
                                    {
                                        Debug.Log("K: " + item.Key);
                                        playerItem.GetComponent<Image>().color = new Color(255, 255, 255, 255);


                                        playerItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[item.Key - 1].SpriteName);
                                        playerItem.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);

                                        playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().text = item.Value.ToString();
                                        playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
                                        playerItem.name = item.Key.ToString();

                                        playerItem.AddComponent<ChangeCursorScript>();
                                        playerItem.GetComponent<ChangeCursorScript>().cursorTexture = cursor;

                                        playerItem.AddComponent<DraggableItem>();
                                        playerItem.GetComponent<DraggableItem>().image = playerItem.GetComponent<Image>();
                                        playerItem.GetComponent<DraggableItem>().childImage = playerItem.transform.Find("ItemImage").GetComponent<Image>();


                                        playerItem.AddComponent<Button>();
                                        playerItem.GetComponent<Button>().onClick.AddListener(() => DisplayDescription(item.Key - 1));
                                        playerItem.GetComponent<Button>().onClick.AddListener(() => DisplayDescription(item.Key - 1));
                                        break;

                                    }
                                    cnt++;
                                }
                            }
                        }

                    }
                    else
                    {
                        int cnt = 1;
                        while (cnt < 5)
                        {
                            playerItem = GameObject.Find("Item" + cnt + "ImageK");
                            if (playerItem != null)
                            {
                                Debug.Log("K: " + item.Key);
                                playerItem.GetComponent<Image>().color = new Color(255, 255, 255, 255);


                                playerItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[item.Key - 1].SpriteName);
                                playerItem.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 255);

                                playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().text = item.Value.ToString();
                                playerItem.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
                                playerItem.name = item.Key.ToString();
                                break;

                            }
                            cnt++;
                        }
                    }
                }
                index++;
            }
        }

    }

    private void LoadPlayerItem2()
    {
        if (playerItems != null)
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
                    {
                        if (!activatedSlot.transform.GetChild(0).gameObject.name.Contains("ImageK"))
                        {
                            activatedSlot.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);

                            activatedSlot.transform.GetChild(0).gameObject.transform.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 0);

                            activatedSlot.transform.GetChild(0).gameObject.transform.Find("ItemAmount").GetComponent<TMP_Text>().color = new Color(255, 255, 255, 0);
                        }
                    }
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
