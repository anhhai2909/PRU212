using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Button upgradeBtn;

    public Button shopBtn;

    public GameObject shop;

    public GameObject upgrade;

    private string mode;

    void Start()
    {
        mode = "upgrade";
        upgradeBtn.onClick.AddListener(UpgradeClick);
        shopBtn.onClick.AddListener(ShopClick);
    }

    // Update is called once per frame
    void Update()
    {

    }



 

    void UpgradeClick()
    {
        mode = "upgrade";
        upgrade.SetActive(true);
        shop.SetActive(false);
        upgradeBtn.GetComponent<Image>().color =  new Color(upgradeBtn.GetComponent<Image>().color.r, upgradeBtn.GetComponent<Image>().color.g, upgradeBtn.GetComponent<Image>().color.b, 255);
        shopBtn.GetComponent<Image>().color = new Color(shopBtn.GetComponent<Image>().color.r, shopBtn.GetComponent<Image>().color.g, shopBtn.GetComponent<Image>().color.b, 0);

    }

    void ShopClick()
    {
        mode = "shop";
        upgrade.SetActive(false);
        shop.SetActive(true);
        upgradeBtn.GetComponent<Image>().color = new Color(upgradeBtn.GetComponent<Image>().color.r, upgradeBtn.GetComponent<Image>().color.g, upgradeBtn.GetComponent<Image>().color.b, 0);
        shopBtn.GetComponent<Image>().color = new Color(shopBtn.GetComponent<Image>().color.r, shopBtn.GetComponent<Image>().color.g, shopBtn.GetComponent<Image>().color.b, 255);
    }
}
