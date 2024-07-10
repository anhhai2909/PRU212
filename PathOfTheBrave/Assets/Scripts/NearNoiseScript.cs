using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NearNoiseScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    public AudioClip sound;

    private SoundEffectScript soundScript;

    public float distanceX;

    public float distanceY;

    void Start()
    {
        soundScript = gameObject.GetComponent<SoundEffectScript>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(player.transform.position.y - gameObject.transform.position.y) <= distanceY &&
             Math.Abs(player.transform.position.x - gameObject.transform.position.x) <= distanceX)
        {
            soundScript.gameObject.GetComponent<AudioSource>().clip = sound;
            if(!gameObject.GetComponent<AudioSource>().isPlaying )
            {
                soundScript.Play();

            }
        }
        else
        {
            soundScript.Stop();
        }
    }
}
