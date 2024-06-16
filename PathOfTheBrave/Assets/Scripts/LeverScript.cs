using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite activated;

    public Sprite deactivated;

    public SpriteRenderer renderers;

    public bool isActivated;

    public Slider slider;

    public GameObject sliderObject;

    public Transform playerTransform;

    private float delay = 0.5f;

    private float timer = 0;

    private float holdTime;

    public ReducingScript reducingScript;

    void Start()
    {
        holdTime = 0.002f;
        playerTransform = GameObject.Find("Player").transform;
        renderers = this.gameObject.GetComponent<SpriteRenderer>();
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < delay)
        {
            if (slider != null)
            {
                if (Math.Abs(playerTransform.position.x - transform.position.x) <= 2f && Math.Abs(playerTransform.position.y - transform.position.y) <= 3f)
                {
                    sliderObject.active = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        slider.value += holdTime;

                        if (slider.value == 1)
                        {
                            isActivated = !isActivated;
                            if (isActivated)
                            {
                                renderers.sprite = activated;
                                reducingScript.isActivated = true;
                            }
                            else
                            {
                                renderers.sprite = deactivated;
                                reducingScript.isActivated = false;

                            }
                            slider.value = 0;
                        }
                    }
                    else
                    {
                        slider.value = 0;
                    }
                }
                else
                {
                    slider.value = 0;
                    sliderObject.active = false;
                }
            }
        }
        else
        {
            timer = 0;
        }
 
    }
}
