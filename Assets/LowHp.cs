using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHp : MonoBehaviour
{

    public Player player;
    public bool IsPlayerHurt => player.health <= Mathf.Max(player.maxHealth / 5, 1);
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
    }

    public Color lowColor;
    public Color nonLowColor;
    // Update is called once per frame
    void Update()
    {
        image.color = Color.Lerp(image.color, IsPlayerHurt ? lowColor : nonLowColor, RealTime.deltaTime);
    }
}
