using Assets.Scripts.DataPersistence.Data;
using narrenschlag.extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class TimerBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bar;

    public float time;

    private string str;

    private GameItem gameItem;

    private GameObject gameItemINP;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(bar.transform.localScale.x == 0)
        {
            GameObject gameObject = GameObject.Find("ActivatedItemInGame");
            if(gameObject.GetComponent<InprogressActivatedItem>().inprogressObject.Contains(str))
            {
                gameObject.GetComponent<InprogressActivatedItem>().inprogressObject.Remove(str);
            }
            if (gameObject.GetComponent<InprogressActivatedItem>().inProgressItem.Contains(gameItem))
            {
                gameObject.GetComponent<InprogressActivatedItem>().inProgressItem.Remove(gameItem);
            }
            gameItemINP.GetComponent<Image>().sprite = Resources.Load<Sprite>("");
            gameItemINP.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            gameItemINP.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0);
            bar.transform.localScale = new Vector3(1.802f, bar.transform.localScale.y, bar.transform.localScale.z);
        }
    }

    public int AnimateBar(string index, GameItem item, GameObject itemINP)
    {
        int id = LeanTween.scaleX(bar, 0, time).id;
        str = index;
        gameItem = item;
        gameItemINP = itemINP;
        return id;
    }

    public void CancelLeanTween(int id)
    {
        LeanTween.cancel(id);
    }
}
