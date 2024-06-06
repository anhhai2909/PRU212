using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    private string _gamerIp { get; set; }

    private float _hp { get;set; }

    private string _scene { get; set; }

    private float _xPosition { get; set; }

    private float _yPosition { get; set; }

    private float _coin { get; set; }


    public GameData()
    {
    }

    public GameData(string gamerIp, float hp, string scene, float xPosition, float yPosition, float coin)
    {
        _gamerIp = gamerIp;
        _hp = hp;
        _scene = scene;
        _xPosition = xPosition;
        _yPosition = yPosition;
        _coin = coin;
    }
}
