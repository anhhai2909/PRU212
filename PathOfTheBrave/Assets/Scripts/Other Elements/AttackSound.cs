using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public SoundEffectScript script;
    public AudioClip swordSound;
    public AudioClip punchSound;
    public AudioClip spellSound;
    public AudioClip bowReleaseSound;
    void swordSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = swordSound;
        script.Play();
    }
    void punchSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = punchSound;
        script.Play();
    }
    void spellSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = spellSound;
        script.Play();
    }
    void bowReleaseSoundFunc()
    {
        script.gameObject.GetComponent<AudioSource>().clip = bowReleaseSound;
        script.Play();
    }
}
