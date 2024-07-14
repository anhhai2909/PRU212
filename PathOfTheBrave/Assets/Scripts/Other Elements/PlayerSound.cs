using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public SoundEffectScript script;
    public AudioClip moveSound;
    public AudioClip landingSound;
    public AudioClip anhOiCoLenSound;
    public AudioClip dashSound;
    public AudioClip crawlSound;

    void moveSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = moveSound;
        script.Play();
    }
    void landingSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = landingSound;
        script.Play();
    }
    void hurtSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = anhOiCoLenSound;
        script.Play();
    }
    void dashSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = dashSound;
        script.Play();
    }
    void crawlSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = crawlSound;
        script.Play();
    }

}