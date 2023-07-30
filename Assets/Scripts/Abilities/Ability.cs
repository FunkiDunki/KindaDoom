using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Ability : MonoBehaviour
{
    protected float cooldown;
    protected float curCooldown;
    protected Image abilityDisplay;
    protected TextMeshProUGUI cooldownText;
    protected Image coverImage;
    protected AbilityInfo info;


    protected void _Init(Image abilityDisplay, TextMeshProUGUI cooldownText, Image coverImage, AbilityInfo info)
    {
        cooldown = info.cooldown;
        curCooldown = 0.0f;
        this.abilityDisplay = abilityDisplay;
        this.cooldownText = cooldownText;
        this.cooldownText.text = "";
        this.coverImage = coverImage;
        this.info = info;
        this.abilityDisplay.sprite = info.sprite;
    }

        public abstract void Init(Image abilityDisplay, TextMeshProUGUI cooldownText, Image coverImage, AbilityInfo info);

    private void MoveCooldown(float timeDelta)
    {
        if (curCooldown < 0)
        {
            cooldownText.text = "";
            return;
        }
        curCooldown -= timeDelta;
        cooldownText.text = Mathf.Round(curCooldown + 0.5f).ToString();
        coverImage.rectTransform.sizeDelta = new Vector2(coverImage.rectTransform.sizeDelta.x, coverImage.rectTransform.sizeDelta.x * (curCooldown / cooldown));
    }

    public void Tick(float timeDelta)
    {
        MoveCooldown(timeDelta);
    }

    public virtual bool canUse()
    {
        return curCooldown <= 0.0f;
    }

    public abstract bool UseDown();//when the button is pressed initially

    public abstract bool UseUp();//for when the button is un-pressed
}
