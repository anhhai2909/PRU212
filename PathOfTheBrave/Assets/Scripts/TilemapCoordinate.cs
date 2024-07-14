using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System.Linq;
using Unity.Mathematics;

public class TilemapCoordinate : MonoBehaviour
{
    private Tilemap tilemap;

    public GameObject lightning;

    public GameObject timerBar;

    public List<Vector3Int> listPos;

    public Camera mainCamera;

    public List<int> listTimer;

    private int limit;

    public float delay;

    private float timer;

    float leftBoundary;

    float distanceToAdd;

    float rightBoundary;

    public float levelTime;

    private GameObject player; 


    void Start()
    {
        player = GameObject.Find("Player");
        levelTime = Time.realtimeSinceStartup;
        distanceToAdd = 2.73f;
        limit = 2;
        delay = 6f;
        timer = 0;
        listPos = new List<Vector3Int>();
        GetCameraBoundary();
        LoadListPos();
    }

    void Update()
    {
        GetCameraBoundary();
        if (Time.realtimeSinceStartup - levelTime >= 0)
        {
            delay = delay / 2;
            levelTime += 30;
        }
        SpawnLightning();
       
    }

    void LoadListPos()
    {
        tilemap = GetComponent<Tilemap>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3 worldPos = tilemap.CellToWorld(pos);
            if (tilemap.HasTile(pos))
            {
                listPos.Add(pos);
            }
        }
    }

    void SpawnLightning()
    {
        if (timer <= Time.realtimeSinceStartup)
        {
            int positionIndex = GetRandomVector3InRange(listPos, leftBoundary, rightBoundary);
            if (positionIndex != -1)
            {
                Vector3 position = tilemap.GetCellCenterWorld(listPos[positionIndex]);
                position = new Vector3(position.x - 0.27f, position.y + 7f);
                if (!listTimer.Contains(positionIndex))
                {
                    GameObject bar = Instantiate(this.timerBar.transform.Find("Bar").gameObject);
                    bar.transform.SetParent(this.timerBar.transform);
                    bar.transform.localScale = new Vector2(0.01801228f, 0.001294975f);
                    bar.transform.position = position;
                    bar.GetComponent<TimerBar>().AnimateBar(listPos, positionIndex, tilemap);
                    bar.SetActive(true);
                    listTimer.Add(positionIndex);
                }
                timer = Time.realtimeSinceStartup + delay;
            }
        }

    }

    void CheckLightningDestroyed()
    {

    }

    void GetCameraBoundary()
    {
        Vector3 cameraPosition = mainCamera.transform.position;

        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float leftBoundary = cameraPosition.x - (cameraWidth / 2);
        float rightBoundary = cameraPosition.x + (cameraWidth / 2);
        if (player != null)
        {
            this.leftBoundary = player.transform.position.x - 12f;
            this.rightBoundary = player.transform.position.x + 12f; ;
        }
    }

    public int GetRandomVector3InRange(List<Vector3Int> vectors, float minX, float maxX)
    {
        System.Random random = new System.Random();
        List<Vector3Int> filteredVectors = vectors.Where(v => tilemap.CellToWorld(v).x >= minX && tilemap.CellToWorld(v).x <= maxX).ToList();
        if(filteredVectors.Count == 0)
        {
            return -1;
        }
        int randomIndex = random.Next(0, filteredVectors.Count);
        for(int i = 0; i < listPos.Count; i++)
        {
            if (listPos[i].x == filteredVectors[randomIndex].x && listPos[i].y == filteredVectors[randomIndex].y)
            {
                return i;
            }
        }

        return -1;
    }

 

    // Update is called once per frame
    
}
