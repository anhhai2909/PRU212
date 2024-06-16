using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public bool isBossDoor;

    public ReducingScript reducingScript;

    private Animator animator;

    public AnimationClip animationClip;

    private BossDoorScript bossDoor;

    public TMP_Text bossDoorText;

    void Start()
    {
        if (isBossDoor)
        {
            bossDoor = GameObject.Find("BossKey").GetComponent<BossDoorScript>();

        }
        holdTime = 0.004f;
        playerTransform = GameObject.Find("Player").transform;
        renderers = this.gameObject.GetComponent<SpriteRenderer>();
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDoor != null)
        {
            if (bossDoor.isKeyTaken && bossDoorText.enabled)
            {
                bossDoorText.enabled = false;
            }
            else if (!bossDoor.isKeyTaken)
            {
                sliderObject.SetActive(false);
                bossDoorText.enabled = true;
            }
        }

        if (slider != null)
        {
            if (isBossDoor)
            {
                if (Math.Abs(playerTransform.position.x - transform.position.x) <= 2f && Math.Abs(playerTransform.position.y - transform.position.y) <= 3f)
                {
                    sliderObject.SetActive(true);
                    if (Input.GetKey(KeyCode.E))
                    {
                        slider.value += holdTime;
                        if (slider.value == 1)
                        {
                            slider.value = 0;
                            
                            animator = GameObject.Find("BossDoor").GetComponent<Animator>();
                            animator.enabled = true;
                            
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
                    sliderObject.SetActive(false);
                }

            }
            else
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


    }
}

