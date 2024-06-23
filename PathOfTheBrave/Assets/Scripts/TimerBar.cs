using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bar;

    public float time;

    void Start()
    {
        AnimateBar();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 0, time);
    }
}
