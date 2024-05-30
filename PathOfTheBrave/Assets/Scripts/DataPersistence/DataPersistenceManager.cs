using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;

    private string connectionString = "Server=localhost;port=3306;database=car_rental;uid=root;pwd=haibang20042003;encrypt=false";

//    private string connectionString = "server=localhost;user=root;database=car_rental;port=3306;password=haibang20042003";
    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
        SaveGame();   
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

    }

    public void SaveGame()
    {
        MySqlConnection conn = new MySqlConnection(connectionString);
        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT * FROM brand";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int r = Convert.ToInt32(result);
                Debug.Log("Number of countries in the world database is: " + r);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        conn.Close();
        Debug.Log("Done.");
    }
}
