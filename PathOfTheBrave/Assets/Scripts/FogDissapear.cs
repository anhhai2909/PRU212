using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEditor.PlayerSettings;

public class FogDissapear : MonoBehaviour
{
    // Start is called before the first frame update
    public VisualEffect fogEffect;

    private GameObject player;

    public GameObject guide;

    void Start()
    {
        player = GameObject.Find("Player");
        fogEffect.SetFloat("FogLifetime", 50);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= -1f)
        {
            fogEffect.SetFloat("FogAmount", fogEffect.GetFloat("FogAmount") - 1000);
        }
        if(fogEffect.GetFloat("FogAmount") <= -1770000)
        {
            guide.SetActive(true);
            guide.GetComponent<Animator>().Play("GuideLastMap");
        }
    }
}
