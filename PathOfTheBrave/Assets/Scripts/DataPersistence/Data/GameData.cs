using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public string _gamerIp { get; set; }

    public float _hp { get;set; }

    public int _sceneIndex { get; set; }

    public string _sceneName { get; set; }

    public float _xPosition { get; set; }

    public float _yPosition { get; set; }

    public float _coin { get; set; }


    public GameData()
    {
    }

    public GameData(string gamerIp, float hp, int sceneIndex, string sceneName, float xPosition, float yPosition, float coin)
    {
        _gamerIp = gamerIp;
        _hp = hp;
        _sceneIndex = sceneIndex;   
        _sceneName = sceneName;
        _xPosition = xPosition;
        _yPosition = yPosition;
        _coin = coin;
    }

    public override string ToString()
    {
        return this._gamerIp;
    }
}
