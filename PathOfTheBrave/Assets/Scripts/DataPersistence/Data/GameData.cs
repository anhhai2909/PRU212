using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public int hp;

    public float xPosition;

    public float yPosition;

    public GameData()
    {
    }

    public GameData(int hp, float xPosition, float yPosition)
    {
        this.hp = hp;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }
}
