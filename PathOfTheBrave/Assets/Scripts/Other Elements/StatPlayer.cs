using CoreSystem;
using UnityEngine;
using UnityEngine.UI;

public class StatPlayer : MonoBehaviour
{
    private GameObject player;
    public Slider ValueSlider;
    public Slider EaseSlider;
    private float maxValue;
    private float currentValue;
    private float lerpSpeed = 0.05f;
    [SerializeField] private StatType typeStat;

    void Start()
    {
        player = GameObject.Find("Player");
        if(typeStat == StatType.health)
        {
            maxValue = player.GetComponentInChildren<Stats>().Health.MaxValue;
        }
        else if (typeStat == StatType.mana)
        {
            maxValue = player.GetComponentInChildren<Stats>().Mana.MaxValue;
        }else
        {
            Debug.LogError("Can not get player stats");
            maxValue = 0;
        }


        currentValue = maxValue;
        ValueSlider.maxValue = maxValue;
        EaseSlider.maxValue = maxValue;
    }

    void Update()
    {
        if (player == null) player = GameObject.Find("Player");
        if (typeStat == StatType.health)
        {
            currentValue = player.GetComponentInChildren<Stats>().Health.CurrentValue;
        }
        else if (typeStat == StatType.mana)
        {
            currentValue = player.GetComponentInChildren<Stats>().Mana.CurrentValue;
        }
        else
        {
            Debug.LogError("Can not get player stats");
            currentValue = 0;
        }

        if (ValueSlider.value != currentValue)
        {
            ValueSlider.value = currentValue;
        }

        if (ValueSlider.value != EaseSlider.value)
        {
            EaseSlider.value = Mathf.Lerp(EaseSlider.value, currentValue, lerpSpeed);
        }
    }


}

public enum StatType
{
    health,
    mana
}