using Assets.Scripts.DataPersistence.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InprogressActivatedItem : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<int, int> activatedItems;

    public Dictionary<int, int> oldActivatedItems;

    public Dictionary<int, int> playerItems;

    public List<GameItem> items;

    private bool canPressKey;

    private GameItem activeItem;

    private int activeSlot;

    private float coin;

    public List<string> inprogressObject;

    public Dictionary<string, int> keyINP;

    public List<GameItem> inProgressItem;

    public AudioClip useItemSound;

    private SoundEffectScript sounds;
    void Start()
    {
        sounds = gameObject.GetComponent<SoundEffectScript>();
        keyINP = new Dictionary<string, int>();
        inProgressItem = new List<GameItem>();
        activeSlot = 1;
        canPressKey = true;
        LoadAllItem();
        LoadData();
        LoadToGame(MinItem());
        oldActivatedItems = activatedItems;
    }

    public void LoadData()
    {
        LoadDataScript.LoadPlayerData();
        coin = LoadDataScript.GetCoin();
        playerItems = LoadDataScript.items;
        activatedItems = LoadDataScript.activatedItems;
    }

    private void LoadAllItem()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("JSON\\Item");
        items = JsonConvert.DeserializeObject<List<GameItem>>(jsonData.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (activatedItems != oldActivatedItems)
        {
            string b = "Activated Items: ";
            if (items != null)
            {
                foreach (var item in activatedItems)
                {
                    b += (item.Key + ":" + item.Value + " ");
                }
            }
            LoadToGame(MinItem());
            oldActivatedItems = activatedItems;
        }
        SwapMainItem();
        UseItem();
    }

    public void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.V) && canPressKey)
        {
            foreach (var item in playerItems)
            {
                if (item.Key == activeItem.Id)
                {
                    sounds.gameObject.GetComponent<AudioSource>().clip = useItemSound;
                    sounds.Play();
                    SetTimerBar(activeItem);
                    
                    if (item.Value - 1 > 0)
                    {
                        playerItems[item.Key] = item.Value - 1;
                        LoadDataScript.SavePlayerItemData(coin, playerItems);
                        LoadDataScript.SaveActivatedItems(activatedItems);
                        LoadToGame(activeSlot);
                    }
                    else
                    {
                        playerItems.Remove(item.Key);
                        activatedItems.Remove(activeSlot);
                        LoadDataScript.SavePlayerItemData(coin, playerItems);
                        LoadDataScript.SaveActivatedItems(activatedItems);
                        LoadToGame(MinItem());
                    }
                    

                    break;
                }
            }
            StartCoroutine(DelayCoroutine());
        }
    }

    void SetTimerBar(GameItem item)
    {
        float timer = 15;

        if (inProgressItem.Contains(item))
        {
            Debug.Log("1sa");
            for(int i = 0; i < inProgressItem.Count; i++)
            {
                if(item.Id == inProgressItem[i].Id)
                {
                    GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().CancelLeanTween(keyINP[inprogressObject[i]]);
                    GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().bar = GameObject.Find(inprogressObject[i]).transform.GetChild(0).GetChild(0).gameObject;
                    GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().time = 10;
                    GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().bar.transform.localScale = new Vector3(1.802f, GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().bar.transform.localScale.y, GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().bar.transform.localScale.z);
                    int id = GameObject.Find(inprogressObject[i]).GetComponent<TimerBar>().AnimateBar(inprogressObject[i], item, GameObject.Find(inprogressObject[i]));
                    if (keyINP.ContainsKey(inprogressObject[i]))
                    {
                        keyINP[inprogressObject[i]] = id;
                    }
                    else
                    {
                        keyINP.Add(inprogressObject[i], id);
                    }
                    break;
                }
            }
        }
        else
        {
            if (inProgressItem.Count == 4)
            {
                return;
            }
            else
            {
                
                GameObject itemINP;
                int index = 0;
                while(inprogressObject.Contains("ActivatedInprogress" + (index != 0 ? (index) : (""))) && index <= 3)
                {
                    index++;
                }
                
                inprogressObject.Add("ActivatedInprogress" + (index != 0 ? (index) : ("")));
                itemINP = GameObject.Find("ActivatedInprogress" + (index != 0 ? (index) : ("")));
                itemINP.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SpriteName);
                itemINP.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                itemINP.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.green;
                
                if(itemINP.GetComponent<TimerBar>() == null)
                    itemINP.AddComponent<TimerBar>();
                itemINP.GetComponent<TimerBar>().bar = itemINP.transform.GetChild(0).GetChild(0).gameObject;
                itemINP.GetComponent<TimerBar>().time = 10;
                int id = itemINP.GetComponent<TimerBar>().AnimateBar("ActivatedInprogress" + (index != 0 ? (index) : ("")), item, itemINP);
                if (keyINP.ContainsKey("ActivatedInprogress" + (index != 0 ? (index) : (""))))
                {
                    keyINP["ActivatedInprogress" + (index != 0 ? (index) : (""))] = id;
                }
                else
                {
                    keyINP.Add("ActivatedInprogress" + (index != 0 ? (index) : ("")), id);
                }
            }
            inProgressItem.Add(item);

        }
        string str = "";
        for (int i = 0; i < inprogressObject.Count; i++)
        {
            str += inprogressObject[i].ToString() + " ";
        }
        Debug.Log(str);
    }


    public void SwapMainItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canPressKey)
        {
            activeSlot = 1;
            if (activatedItems.ContainsKey(1))
                LoadToGame(1);

            StartCoroutine(DelayCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canPressKey)
        {
            activeSlot = 2;
            if (activatedItems.ContainsKey(2))
                LoadToGame(2);

            StartCoroutine(DelayCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canPressKey)
        {
            activeSlot = 3;
            if (activatedItems.ContainsKey(3))
                LoadToGame(3);

            StartCoroutine(DelayCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && canPressKey)
        {
            activeSlot = 4;
            if (activatedItems.ContainsKey(4))
                LoadToGame(4);

            StartCoroutine(DelayCoroutine());
        }
    }

    IEnumerator DelayCoroutine()
    {
        canPressKey = false;
        yield return new WaitForSeconds(0.2f);
        canPressKey = true;
    }

    public int MinItem()
    {
        int min = 100;
        if (activatedItems != null)
        {
            foreach (var item in activatedItems)
            {
                if (min > item.Key)
                {
                    min = item.Key;
                }
            }
        }
        return min;
    }

    public void LoadToGame(int index)
    {
        GameObject activatedInGame = GameObject.Find("ActivatedItemInGame");
        if (index == 100)
        {
            Debug.Log("100");
            activatedInGame.GetComponent<Image>().color = new Color(255, 255, 255, 0);

            GameObject activatedInGameImage = GameObject.Find("ActivatedItemInGameImage");
            activatedInGameImage.GetComponent<Image>().color = new Color(255, 255, 255, 0);

            GameObject activatedInGameText = GameObject.Find("ActivatedItemInGameAmount");
            activatedInGameText.GetComponent<TMP_Text>().text = "";
            activatedInGameText.GetComponent<TMP_Text>().color = new Color(255, 255, 255, 0);

        }
        else
        {
            foreach (var item in items)
            {
                if (item.Id == activatedItems[index])
                {
                    activeItem = item;
                    activatedInGame.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                    GameObject activatedInGameImage = GameObject.Find("ActivatedItemInGameImage");
                    activatedInGameImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.SpriteName);
                    activatedInGameImage.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                    GameObject activatedInGameText = GameObject.Find("ActivatedItemInGameAmount");
                    activatedInGameText.GetComponent<TMP_Text>().text = playerItems[item.Id].ToString();
                    activatedInGameText.GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
                    break;
                }
            }
        }
    }
}
