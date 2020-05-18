﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DuloGames.UI;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Vehicle vehicle;
    public UIBulletBar healthRadialBar;
    public UIProgressBar minigunCharge;
    public UIProgressBar boostCharge;

    public List<GameObject> shields;
    public List<GameObject> healthUps;
    
    public Text healthRadialBarText;
    public Text minigunChargeText;
    public Text boostChargeText;

    public void Start()
    {
        healthRadialBar.fillAmount = 1f;
        healthRadialBarText.text = $"{(int) (healthRadialBar.fillAmount * 100)}%";

        minigunCharge.fillAmount = 1f;
        minigunChargeText.text = $"{(int) (minigunCharge.fillAmount * 100)}%";

        boostCharge.fillAmount = 1f;
        boostChargeText.text = $"{(int) (boostCharge.fillAmount * 100)}%";

        for (var i = 0; i < vehicle.shieldAbility.shieldCount; i++)
        {
            shields[i].SetActive(true);
        }
    }

    public void LateUpdate()
    {
        healthRadialBar.fillAmount = vehicle.health;
        healthRadialBarText.text = $"{(int) (vehicle.health * 100)}%";

        minigunCharge.fillAmount = vehicle.minigun.charge;
        minigunChargeText.text = $"{(int) (vehicle.minigun.charge * 100)}%";

        boostCharge.fillAmount = vehicle.boost.charge;
        boostChargeText.text = $"{(int) (vehicle.boost.charge * 100)}%";

        shields[0].SetActive(false);
        shields[1].SetActive(false);
        shields[2].SetActive(false);
        for (var i = 0; i < vehicle.shieldAbility.shieldCount; i++)
        {
            shields[i].SetActive(true);
        }
    }
}
