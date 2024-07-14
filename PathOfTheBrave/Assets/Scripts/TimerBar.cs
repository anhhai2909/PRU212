using Assets.Scripts.DataPersistence.Data;
using narrenschlag.extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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

    public GameObject lightning;

    public bool isForLightning;

    private List<Vector3Int> listTimer;

    private int timerIndex;

    private Tilemap tilemap;

    private bool isActivated;


    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isForLightning)
        {
            if (bar.transform.localScale.x == 0)
            {
                GameObject gameObject = GameObject.Find("ActivatedItemInGame");
                if (gameObject.GetComponent<InprogressActivatedItem>().inprogressObject.Contains(str))
                {
                    gameObject.GetComponent<InprogressActivatedItem>().inprogressObject.Remove(str);
                }
                if (gameObject.GetComponent<InprogressActivatedItem>().inProgressItem.Contains(gameItem))
                {
                    gameObject.GetComponent<InprogressActivatedItem>().inProgressItem.Remove(gameItem);
                }
                gameObject.GetComponent<InprogressActivatedItem>().LoadData();
                gameItemINP.GetComponent<Image>().sprite = Resources.Load<Sprite>("");
                gameItemINP.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                gameItemINP.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0);
                bar.transform.localScale = new Vector3(1.802f, bar.transform.localScale.y, bar.transform.localScale.z);
                
            }
        }
        else
        {
            if (bar.transform.localScale.x == 0 && !isActivated)
            {
                Vector3 position = tilemap.GetCellCenterWorld(listTimer[timerIndex]);
                position = new Vector3(position.x - 0.27f, position.y + 9.4f);

                GameObject lightningObject = Instantiate(this.lightning);
                lightningObject.transform.position = position;
                tilemap.GetComponent<TilemapCoordinate>().listTimer.Remove(timerIndex);
                // Destroy(gameObject);
                isActivated = true;
            }
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

    public int AnimateBar(List<Vector3Int> timerBar, int index, Tilemap tilemap)
    {
        int id = LeanTween.scaleX(bar, 0, time).id;
        listTimer = timerBar;
        timerIndex = index;
        this.tilemap = tilemap;
        return id;
    }

    public void CancelLeanTween(int id)
    {
        LeanTween.cancel(id);
    }
}
