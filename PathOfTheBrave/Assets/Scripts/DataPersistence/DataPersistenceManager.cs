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

    private string connectionString = "Server=localhost;port=3306;database=PRU212_Project;uid=root;pwd=haibang20042003;encrypt=false";

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

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("There are more than 1 data persistence manager in the scene");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public GameData LoadGame()
    {
        MySqlConnection conn = new MySqlConnection(connectionString);
        GameData gameData = ReadFromFile();
     
        return gameData;
    }



    public bool IsLoadGame()
    {
        MySqlConnection conn = new MySqlConnection(connectionString);
        try
        {
            conn.Open();
            string gamerIp = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            string query = "SELECT * FROM GameData WHERE gamer_ip = @gamer_ip";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@gamer_ip", gamerIp);
   

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
          
                return true;
                
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return false;
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

    public void SaveGame(int sceneIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        string gamerIp = GetLocalIPv4(NetworkInterfaceType.Ethernet);
        GameData gameData = new GameData(gamerIp, hp, sceneIndex, sceneName, x, y, coin);
        SaveToFile(gameData);

        
    }
}
