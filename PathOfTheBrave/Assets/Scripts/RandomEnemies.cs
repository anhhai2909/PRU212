using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class RandomEnemies : MonoBehaviour
{
    private int totalEnemiesPerMap;

    private List<GameObject> listEnemies;

    public string folderPath = "Enemies";

    private Tilemap tilemap;

    private bool isLoaded;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            tilemap = GameObject.Find("SpawnEnemy").GetComponent<Tilemap>();
            isLoaded = true;
        }


    }

    private void Awake()
    {
        listEnemies = new List<GameObject>();
        LoadAllPrefabs();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("SpawnEnemy") != null && isLoaded)
        {
            tilemap = GameObject.Find("SpawnEnemy").GetComponent<Tilemap>();
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    {
                        totalEnemiesPerMap = 25;
                        break;
                    }
                case 3:
                    {
                        totalEnemiesPerMap = 30;
                        break;
                    }
                case 4:
                    {
                        totalEnemiesPerMap = 15;
                        break;
                    }
                case 5:
                    {
                        totalEnemiesPerMap = 45;
                        break;
                    }
            }
            for (int i = 0; i < totalEnemiesPerMap; i++)
            {
                RandomEnemy();
            }
            isLoaded = false;
        }
    }

    void LoadAllPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(folderPath);
        listEnemies.AddRange(prefabs);
    }

    void RandomEnemy()
    {
        if (listEnemies.Count == 0)
        {
            Debug.LogError("Enemies is empty!");
            return;
        }

        GameObject randomObject = listEnemies[Random.Range(0, listEnemies.Count)];

        Vector3Int randomCellPosition = GetRandomCellPosition();
        Vector3 worldPosition = tilemap.CellToWorld(randomCellPosition);
        worldPosition = new Vector3(worldPosition.x, worldPosition.y + 2, worldPosition.z);
        Instantiate(randomObject, worldPosition, Quaternion.identity);
    }

    Vector3Int GetRandomCellPosition()
    {
        BoundsInt bounds = tilemap.cellBounds;
        List<Vector3Int> availablePositions = new List<Vector3Int>();

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPosition))
                {
                    availablePositions.Add(cellPosition);
                }
            }
        }

        if (availablePositions.Count == 0)
        {
            Debug.LogError("No available positions found in Tilemap!");
            return Vector3Int.zero;
        }

        return availablePositions[Random.Range(0, availablePositions.Count)];
    }
}
