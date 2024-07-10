using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject Boss;
    private void setDeactiveBoss()
    {
        Boss.SetActive(false);
    }
}
