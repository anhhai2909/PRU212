using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FogAroundScript : MonoBehaviour
{
    public VisualEffect fogEffect;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos = new Vector3(pos.x - 7, pos.y - 3, pos.z);
        fogEffect.SetVector3("FogPos", pos);
    }
}
