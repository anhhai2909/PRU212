using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public GameObject rootValue;
    public Slider ValueSlider;
    public Slider EaseSlider;
    private float maxValue;
    private float currentValue;
    private float lerpSpeed = 0.05f;

    private void Start()
    {
        maxValue = rootValue.GetComponent<EnemyHealthSystem>().maxHealth;
        currentValue = maxValue;
        ValueSlider.maxValue = maxValue;
        EaseSlider.maxValue = maxValue;
    }

    private void Update()
    {

        currentValue = rootValue.GetComponent<EnemyHealthSystem>().getCurrentHealth();
        if (ValueSlider.value != currentValue)
        {
            ValueSlider.value = currentValue;    
        }

        if(ValueSlider.value != EaseSlider.value)
        {
            EaseSlider.value = Mathf.Lerp(EaseSlider.value, currentValue, lerpSpeed);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentValue -= 10;
        }
    }
}
