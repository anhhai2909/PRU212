using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource src;
    public AudioClip clip;

    public void Start()
    {
        src.clip = clip;
        src.Play();
    }

    public void Pause()
    {
        src.Pause();
    }

    public void Stop()
    {
        src.Stop();
    }
}
