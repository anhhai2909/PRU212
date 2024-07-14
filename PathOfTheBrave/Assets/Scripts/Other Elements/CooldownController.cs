using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{
    public Image imageCooldown;
    public TextMeshProUGUI text;

    private float cooldown = 0;
    private float currentCooldown;

    private bool isCooldown;

    private void Start()
    {
        currentCooldown = cooldown;
        isCooldown = false;
        imageCooldown.fillAmount = 0;
        text.text = "";
    }

    void Update()
    {
        text.text = currentCooldown.ToString("F2") + "";

        if (isCooldown && cooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            imageCooldown.fillAmount = currentCooldown / cooldown;
        }

        if (imageCooldown.fillAmount <= 0)
        {
            isCooldown = false;
            currentCooldown = cooldown;
            text.text = "";
        }
    }

    public void setCooldown(float v)
    {
        cooldown = v;
        currentCooldown = v;
        if(v <= 0)
        {
            imageCooldown.fillAmount = 0;
            text.text = "";
        }
    }

    public void setStartCooldown(bool b)
    {
        if (b)
        {
            isCooldown = b;
        }
        else
        {
            isCooldown = false;
            currentCooldown = cooldown;
            imageCooldown.fillAmount = 0;
            text.text = "";
        }
    }
}
