using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TreeEditor;
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

//    private string connectionString = "server=localhost;user=root;database=car_rental;port=3306;password=haibang20042003";
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

    public void LoadGame()
    {
        MySqlConnection conn = new MySqlConnection(connectionString);
        try
        {
            conn.Open();
            string gamerIp = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            string query = "SELECT * FROM GameData WHERE gamer_ip = @gamer_ip";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@gamer_ip", gamerIp);

            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                GameData gameData = new GameData();

                while (rdr.Read())
                {
                     
                }
            }
            

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
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

    public void SaveGame(string sceneName)
    {
        Debug.Log(IsLoadGame());
        if(!IsLoadGame())
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                string gamerIp = GetLocalIPv4(NetworkInterfaceType.Ethernet);
                string query = "INSERT INTO GameData (gamer_ip, hp, scene, xPosition, yPosition, coin) VALUES (@gamer_ip, @hp, @scene, @xPosition, @yPosition, @coin)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@gamer_ip", gamerIp);
                cmd.Parameters.AddWithValue("@hp", hp);
                cmd.Parameters.AddWithValue("scene", sceneName);
                cmd.Parameters.AddWithValue("@xPosition", x);
                cmd.Parameters.AddWithValue("@yPosition", y);
                cmd.Parameters.AddWithValue("@coin", coin);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();
        }
        else
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                string gamerIp = GetLocalIPv4(NetworkInterfaceType.Ethernet);
                string query = "UPDATE GameData SET hp = @hp, scene = @scene, xPosition = @xPosition, yPosition = @yPosition, coin = @coin WHERE gamer_ip = @gamer_ip";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@hp", hp);
                cmd.Parameters.AddWithValue("scene", sceneName);
                cmd.Parameters.AddWithValue("@xPosition", x);
                cmd.Parameters.AddWithValue("@yPosition", y);
                cmd.Parameters.AddWithValue("@coin", coin);
                cmd.Parameters.AddWithValue("@gamer_ip", gamerIp);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            conn.Close();
        }
        
    }
}
