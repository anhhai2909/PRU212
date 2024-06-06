using Assets.Scripts.DataPersistence.Data;
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

    public List<SceneInfor> _scenesInfor { get; set; }


    public GameData()
    {
    }

    public GameData(string gamerIp, float hp, int sceneIndex, string sceneName, float xPosition, float yPosition, float coin, List<SceneInfor> sceneInfor)
    {
        _gamerIp = gamerIp;
        _hp = hp;
        _sceneIndex = sceneIndex;   
        _sceneName = sceneName;
        _xPosition = xPosition;
        _yPosition = yPosition;
        _coin = coin;
        _scenesInfor = sceneInfor;
    }

    public override string ToString()
    {
        string sceneStatus = "";
        for(int i = 0; i < this._scenesInfor.Count; i++)
        {
            sceneStatus += (this._scenesInfor[i].sceneName + "," +this._scenesInfor[i].isCompleted);
            sceneStatus += " ";
        }
        return this._gamerIp + " " + this._hp + " " + this._sceneIndex + " " + this._sceneName + " " + this._xPosition + " " + this._yPosition + " " + this._coin + " " + sceneStatus;
    }
}
