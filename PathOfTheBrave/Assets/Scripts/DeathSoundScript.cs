using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundScript : MonoBehaviour
{
    // Start is called before the first frame update
    private SoundEffectScript sounds;
    public AudioClip errorSounds;

    void Start()
    {
        sounds = gameObject.GetComponent<SoundEffectScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SoundPlay()
    {
        sounds.Play();
    }
}
