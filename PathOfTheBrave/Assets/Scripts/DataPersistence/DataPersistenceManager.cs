using Assets.Scripts.DataPersistence.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DataPersistenceManager
{
    private GameData gameData;

    public float hp;

    public float x;

    public float y;

    public float coin;

    private readonly byte[] key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); 
    private readonly byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

    public DataPersistenceManager(float hp, float x, float y, float coin)
    {
        this.hp = hp;
        this.x = x;
        this.y = y;
        this.coin = coin;
    }

    public DataPersistenceManager()
    {
    }

    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
        
    }

    public string GetLocalIPv4(NetworkInterfaceType _type)
    {
        string output = "";
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        output = ip.Address.ToString();
                    }
                }
            }
        }
        return output;
    }

  

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public GameData LoadGame()
    {
        GameData gameData = ReadFromFile();
        return gameData;
    }
    public void SaveToFile(GameData gameData)
    {
        
        try
        {
            string filePath = Application.persistentDataPath + "/gameData.txt";
            string json = JsonConvert.SerializeObject(gameData);
            EncryptToFile(filePath, json);
        } catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void EncryptToFile(string filePath, string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                }
            }
        }
    }

    public string DecryptFromFile(string filePath)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    public GameData ReadFromFile()
    {
        try
        {
            string filePath = Application.persistentDataPath + "/gameData.txt";
            if (File.Exists(filePath))
            {
                string readText = DecryptFromFile(filePath);
                GameData gameData = JsonConvert.DeserializeObject<GameData>(readText);
                return gameData;
            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public void SaveGame(int sceneIndex, List<SceneInfor> scenes)
    {
        try
        {
            string path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            string gamerIp = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            if (ReadFromFile() == null)
            {
                gameData = new GameData(gamerIp, hp, sceneIndex, sceneName, x, y, coin, scenes);
            }
            else
            {
                gameData = new GameData(gamerIp, hp, sceneIndex, sceneName, x, y, coin, scenes);
                GameData oldData = LoadGame();
                gameData._healthLevel = oldData._healthLevel;
                gameData._manaLevel = oldData._manaLevel;
                gameData._sdLevel = oldData._sdLevel;
                gameData._bdLevel = oldData._bdLevel;
                gameData._mdLevel = oldData._mdLevel;
                gameData._items = oldData._items;
                gameData._activatedItems = oldData._activatedItems;
            }
            
            
            SaveToFile(gameData);
            ReadFromFile();
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
        
    }
}
