using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static LoadDataScript instance;

    public static int playerHealthLevel;

    public static int playerManaLevel;

    public static int playerSdLevel;

    public static int playerBdLevel;

    public static int playerMdLevel;

    public static Dictionary<int, int> items;

    public static Dictionary<int, int> activatedItems;

    public static void LoadPlayerData()
    {
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        GameData gameData = dataPersistenceManager.LoadGame();
        if(gameData != null)
        {
            playerHealthLevel = gameData._healthLevel;
            playerManaLevel = gameData._manaLevel;
            playerSdLevel = gameData._sdLevel;
            playerBdLevel = gameData._bdLevel;
            playerMdLevel = gameData._mdLevel;
            items = gameData._items;
            activatedItems = gameData._activatedItems;
        }
        
        
      

    }


    public static void SavePlayerItemData(
        float coin,
        Dictionary<int, int> items
        )
    {
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        GameData gameData = dataPersistenceManager.LoadGame();
        gameData._items = items;
        gameData._coin = coin;
        dataPersistenceManager.SaveToFile(gameData);
    }

    public static void SavePlayerLevelData(
        float coin,
        int healthLevel,
        int manaLevel,
        int sdLevel,
        int bdLevel,
        int mdLevel
        )
    {
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        GameData gameData = dataPersistenceManager.LoadGame();
        gameData._coin = coin;
        gameData._healthLevel = healthLevel;
        gameData._manaLevel = manaLevel;
        gameData._sdLevel = sdLevel;
        gameData._bdLevel = bdLevel;
        gameData._mdLevel = mdLevel;
        dataPersistenceManager.SaveToFile(gameData);
    }

    public static float GetHealth()
    {
        return 100;
    }

    public static float GetCoin()
    {
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        GameData gameData = dataPersistenceManager.LoadGame();
        return gameData._coin;
    }

    public static void SaveCoin()
    {
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        GameData gameData = dataPersistenceManager.LoadGame();
        gameData._coin = 2000;
        dataPersistenceManager.SaveToFile(gameData);
    }

    public static void SaveActivatedItems(Dictionary<int, int> newActivatedItems)
    {
        DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();
        GameData gameData = dataPersistenceManager.LoadGame();
        gameData._activatedItems = newActivatedItems;
        dataPersistenceManager.SaveToFile(gameData);
    }

}
