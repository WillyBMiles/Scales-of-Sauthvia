using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI text;
    AbilityManager abilityManager;
    public Ability ability;
    Image image;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        abilityManager = FindObjectOfType<AbilityManager>();
        image = GetComponent<Image>();
        image.color = offColor;
        
    }

    Color offColor = new Color(1f, 1f,1f, 0f);
    Color onColor = new Color(1f, 1f, 1f, 1f);

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        abilityManager.AddAbility(id);
    }

    public void SetAbility(Ability ability)
    {
        text.text = $"<b>{ability.DisplayName}</b>\n{ability.DisplayDescription}";
        this.ability = ability;
    }

    public void FadeIn()
    {
        image.CrossFadeAlphaFixed(1f, 1f, false);
    }

    public void FadeOut()
    {
        image.color = offColor;
        text.text = "";
    }
}

public static class ExtensionMethod
{
    public static void CrossFadeAlphaFixed(this Graphic img, float alpha, float duration, bool ignoreTimeScale)
    {
        //Make the alpha 1
        Color fixedColor = img.color;
        fixedColor.a = 1;
        img.color = fixedColor;

        //Set the 0 to zero then duration to 0
        img.CrossFadeAlpha(0f, 0f, true);

        //Finally perform CrossFadeAlpha
        img.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }
}
