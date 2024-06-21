using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnHealth;

    public Button btnMana;

    public Button btnSD;

    public Button btnBD;

    public Button btnMD;

    public Image imgHealth;

    public Image imgMana;

    public Image imgSD;

    public Image imgBD;

    public Image imgMD;

    public float levelHealth;

    public float levelMana;

    public float levelSD;

    public float levelBD;

    public float levelMD;

    public float coin;

    public TMP_Text coinText;

    public TMP_Text healthCoinText;

    public TMP_Text manaCoinText;

    public TMP_Text sdCoinText;

    public TMP_Text bdCoinText;

    public TMP_Text mdCoinText;

    public TMP_Text maximumHealth;

    public TMP_Text maximumMana;

    public TMP_Text maximumSD;

    public TMP_Text maximumBD;

    public TMP_Text maximumMD;


    void Start()
    {
        LoadCoin();   
        imgHealth.sprite = Resources.Load<Sprite>(BarStatus(levelHealth));
        imgMana.sprite = Resources.Load<Sprite>(BarStatus(levelMana));
        imgSD.sprite = Resources.Load<Sprite>(BarStatus(levelSD));
        imgBD.sprite = Resources.Load<Sprite>(BarStatus(levelBD));
        imgMD.sprite = Resources.Load<Sprite>(BarStatus(levelMD));

        if (levelHealth < 4)
            btnHealth.onClick.AddListener(HealthClick);
        if (levelMana < 4)
            btnMana.onClick.AddListener(ManaClick);
        if (levelSD < 4)
            btnSD.onClick.AddListener(SDClick);
        if (levelBD < 4)
            btnBD.onClick.AddListener(BDClick);
        if (levelMD < 4)
            btnMD.onClick.AddListener(MDClick);
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

    string BarStatus(float level)
    {
        string sprite = "";
        switch(level)
        {
            case 0:
                {
                    sprite = "All_242";
                    break;
                }
            case 1:
                {
                    sprite = "All_227";
                    break;
                }
            case 2:
                {
                    sprite = "All_217";
                    break;
                }
            case 3:
                {
                    sprite = "All_203";
                    break;
                }
            case 4:
                {
                    sprite = "All_182";
                    break;
                }
            default:
                {
                    sprite = "All_182";
                    break;
                }
        }
        return sprite;
    }

    float CoinStatus(float level)
    {
        float coin = 0;
        switch(level)
        {
            case 0:
                {
                    coin = 100;
                    break;
                }
            case 1:
                {
                    coin = 150;
                    break;
                }
            case 2:
                {
                    coin = 200;
                    break;
                }
            case 3:
                {
                    coin = 250;
                    break;
                }
            case 4:
                {
                    coin = 300;
                    break;
                }
            default:
                {
                    coin = 300;
                    break;
                }
        }
        return coin;
    }

    void ChangeCoin(int amount)
    {
        coin -= amount;
        coinText.text = coin.ToString();
    }

    void HealthClick()
    {
        if (coin - Convert.ToInt32(healthCoinText.text) >= 0)
        {
            levelHealth++;
            imgHealth.sprite = Resources.Load<Sprite>(BarStatus(levelHealth));
            ChangeCoin(Convert.ToInt32(healthCoinText.text));
            healthCoinText.text = CoinStatus(levelHealth).ToString();
            if(levelHealth == 4)
            {
                btnHealth.gameObject.SetActive(false);
                maximumHealth.gameObject.SetActive(true);
            }
           
        }
    }

    void ManaClick()
    {
        if (coin - Convert.ToInt32(manaCoinText.text) >= 0)
        {
            levelMana++;
            imgMana.sprite = Resources.Load<Sprite>(BarStatus(levelMana));
            ChangeCoin(Convert.ToInt32(manaCoinText.text));
            manaCoinText.text = CoinStatus(levelMana).ToString();
            if (levelMana == 4)
            {
                btnMana.gameObject.SetActive(false);
                maximumMana.gameObject.SetActive(true);
            }
        }
    }

    void SDClick()
    {
        if (coin - Convert.ToInt32(sdCoinText.text) >= 0)
        {
            levelSD++;
            imgSD.sprite = Resources.Load<Sprite>(BarStatus(levelSD));
            ChangeCoin(Convert.ToInt32(sdCoinText.text));
            sdCoinText.text = CoinStatus(levelSD).ToString();
            if (levelSD == 4)
            {
                btnSD.gameObject.SetActive(false);
                maximumSD.gameObject.SetActive(true);
            }
        }
    }

    void BDClick()
    {
        if (coin - Convert.ToInt32(bdCoinText.text) >= 0)
        {
            levelBD++;
            imgBD.sprite = Resources.Load<Sprite>(BarStatus(levelBD));
            ChangeCoin(Convert.ToInt32(bdCoinText.text));
            bdCoinText.text = CoinStatus(levelBD).ToString();
            if (levelBD == 4)
            {
                btnBD.gameObject.SetActive(false);
                maximumBD.gameObject.SetActive(true);
            }
        }
    }

    void MDClick()
    {
        if (coin - Convert.ToInt32(mdCoinText.text) >= 0)
        {
            levelMD++;
            imgMD.sprite = Resources.Load<Sprite>(BarStatus(levelMD));
            ChangeCoin(Convert.ToInt32(mdCoinText.text));
            mdCoinText.text = CoinStatus(levelMD).ToString();
            if (levelMD == 4)
            {
                btnMD.gameObject.SetActive(false);
                maximumMD.gameObject.SetActive(true);
            }
        }
    }

    void CheckEnoughCoin()
    {
        if(coin < Convert.ToInt32(healthCoinText.text))
        {
            healthCoinText.color = Color.red;
        }
        else
        {
            healthCoinText.color = Color.white;
        }

        if (coin < Convert.ToInt32(manaCoinText.text))
        {
            manaCoinText.color = Color.red;
        }
        else
        {
            manaCoinText.color = Color.white;
        }

        if (coin < Convert.ToInt32(sdCoinText.text))
        {
            sdCoinText.color = Color.red;
        }
        else
        {
            sdCoinText.color = Color.white;
        }

        if (coin < Convert.ToInt32(bdCoinText.text))
        {
            bdCoinText.color = Color.red;
        }
        else
        {
            bdCoinText.color = Color.white;
        }

        if (coin < Convert.ToInt32(mdCoinText.text))
        {
            mdCoinText.color = Color.red;
        }
        else
        {
            mdCoinText.color = Color.white;
        }
    }
}
