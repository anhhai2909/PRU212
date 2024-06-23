using Assets.Scripts.DataPersistence.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ItemScript : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameItem> items;

    public int page = 0;

    public int totalPage;

    private int size = 6;

    public Button btnNext;

    public Button btnPrev;

    public GameObject itemA;

    public GameObject itemB;

    public GameObject itemC;

    public GameObject itemD;

    public GameObject itemE;

    public GameObject itemF;

    private List<GameObject> list;

    public float coin;

    public TMP_Text coinText;

    private GameObject itemsObject;

    private List<string> tooltips;

    private SoundEffectScript sounds;

    public AudioClip errorSounds;

    public AudioClip coinSounds;

    public int[] leenTweenId;

    void Start()
    {
        LoadDataScript.LoadPlayerData();
        sounds = gameObject.GetComponent<SoundEffectScript>();
        tooltips = new List<string>();

        itemA.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemA.name));
        itemB.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemB.name));
        itemC.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemC.name));
        itemD.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemD.name));
        itemE.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemE.name));
        itemF.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemF.name));


        itemsObject = GameObject.Find("Items");

        list = new List<GameObject>() { itemA, itemB, itemC, itemD, itemE, itemF };
        btnNext.onClick.AddListener(NextPage);
        btnPrev.onClick.AddListener(PrevPage);
        TextAsset jsonData = Resources.Load<TextAsset>("JSON\\Item");
        items = JsonConvert.DeserializeObject<List<GameItem>>(jsonData.text);
        totalPage = items.Count % 6 == 0 ? items.Count / 6 : items.Count / 6 + 1;
        LoadItem();
        LoadCoin();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnoughCoin();

    }

    void LoadCoin()
    {
        coin = LoadDataScript.GetCoin();
        coinText.text = coin.ToString();
    }

    void LoadItem()
    {
        for (int i = page * 6; i < page * 6 + 6; i++)
        {
            if (i >= items.Count)
            {
                list[i - (page * 6)].SetActive(false);
            }
            else
            {
                tooltips.Add(items[i].Description);
                GameObject gameObject = list[i % 6];
                gameObject.name = items[i].Id.ToString();
                list[i - (page * 6)].SetActive(true);
                gameObject.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(items[i].SpriteName);
                gameObject.transform.Find("ItemCost").GetComponent<TMP_Text>().text = items[i].Coin.ToString();
            }
        }
        itemsObject.GetComponent<ItemTooltip>().tooltips = tooltips;
    }

    void NextPage()
    {
        if (page == 0)
        {
            btnPrev.gameObject.SetActive(true);
        }
        page++;
        if (page == totalPage - 1)
        {
            btnNext.gameObject.SetActive(false);
        }
        LoadItem();
    }

    void PrevPage()
    {
        if (page == totalPage - 1)
        {
            btnNext.gameObject.SetActive(true);
        }
        page--;
        if (page == 0)
        {
            btnPrev.gameObject.SetActive(false);
        }
        LoadItem();

    }

    void ChangeCoin(float amount)
    {
        coin -= amount;
        coinText.text = coin.ToString();
    }

    void BuyItem(string id_raw)
    {
        int id = Convert.ToInt32(id_raw) - 1;
        if (coin - items[id].Coin >= 0)
        {
            LoadDataScript.LoadPlayerData();
            ChangeCoin(items[id].Coin);
            sounds.gameObject.GetComponent<AudioSource>().clip = coinSounds;
            sounds.Play();
            Dictionary<int, int> playerItems;
            if (LoadDataScript.items == null)
            {
                playerItems = new Dictionary<int, int>();
                playerItems.Add(id + 1, 1);

            }
            else
            {
                playerItems = LoadDataScript.items;
                if (playerItems.ContainsKey(id + 1)) {
                    playerItems[id + 1]++;
                }
                else
                {
                    playerItems.Add(id + 1, 1);
                }

            }
            LoadDataScript.SavePlayerItemData(coin, playerItems);
            GameObject inprogressActivatedItem = GameObject.Find("ActivatedItemInGame");
            inprogressActivatedItem.GetComponent<InprogressActivatedItem>().LoadData();
            inprogressActivatedItem.GetComponent<InprogressActivatedItem>().LoadToGame(inprogressActivatedItem.GetComponent<InprogressActivatedItem>().MinItem());
        }
        else
        {
            sounds.gameObject.GetComponent<AudioSource>().clip = errorSounds;
            sounds.Play();

        }
    }

    void CheckEnoughCoin()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i < items.Count)
            {
                string a = list[i].name;
                if (coin < items[Convert.ToInt32(a) - 1].Coin)
                {
                    list[i].transform.Find("ItemCost").GetComponent<TMP_Text>().color = Color.red;
                }
                else
                {
                    list[i].transform.Find("ItemCost").GetComponent<TMP_Text>().color = Color.white;
                }
            }
        }
    }
}
