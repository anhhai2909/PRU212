using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightningScript : MonoBehaviour
{

    public VisualEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        
        if(effect != null)
        {
            effect.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(effect.aliveParticleCount < 0)
        {
            effect.Play();

        }
    }
}
