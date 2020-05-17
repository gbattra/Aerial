using System;
using System.Collections;
using System.Collections.Generic;
using DuloGames.UI;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;
    public UIBulletBar healthRadialBar;
    public UIProgressBar minigunCharge;
    public UIProgressBar boostCharge;
    
    public Text healthRadialBarText;
    public Text minigunChargeText;
    public Text boostChargeText;

    public void Start()
    {
        healthRadialBar.fillAmount = 1f;
        healthRadialBarText.text = "100%";

        minigunCharge.fillAmount = 1f;
        minigunChargeText.text = "100%";

        boostCharge.fillAmount = 1f;
        boostChargeText.text = "100%";
    }
}
