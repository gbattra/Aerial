using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DuloGames.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TutorialHUD : MonoBehaviour
{
    public Vehicle vehicle;
    public Player player;

    public AudioSource audioSource;
    public AudioClip scoreUpAudioClip;

    public UIBulletBar healthRadialBar;
    public UIProgressBar minigunCharge;
    public UIProgressBar boostCharge;

    public Text healthRadialBarText;
    public Text minigunChargeText;
    public Text boostChargeText;

    public TextMeshProUGUI shieldCount;
    public TextMeshProUGUI healthUpCount;

    private float currentPlayerScore;
    

    public void Start()
    {
        healthRadialBar.fillAmount = 1f;
        healthRadialBarText.text = $"{(int) (healthRadialBar.fillAmount * 100)}%";

        minigunCharge.fillAmount = 1f;
        minigunChargeText.text = $"{(int) (minigunCharge.fillAmount * 100)}%";

        boostCharge.fillAmount = 1f;
        boostChargeText.text = $"{(int) (boostCharge.fillAmount * 100)}%";

        shieldCount.text = $"{vehicle.shieldAbility.shieldCount}";
        healthUpCount.text = $"{vehicle.healthUpAbility.healthUpCount}";
    }

    public void LateUpdate()
    {
        if (currentPlayerScore < player.score)
        {
            audioSource.PlayOneShot(scoreUpAudioClip);
            currentPlayerScore = player.score;
        }
        healthRadialBar.fillAmount = vehicle.health;
        healthRadialBarText.text = $"{(int) (vehicle.health * 100)}%";

        minigunCharge.fillAmount = vehicle.minigun.charge;
        minigunChargeText.text = $"{(int) (vehicle.minigun.charge * 100)}%";

        boostCharge.fillAmount = vehicle.boost.charge;
        boostChargeText.text = $"{(int) (vehicle.boost.charge * 100)}%";

        shieldCount.text = $"{vehicle.shieldAbility.shieldCount}";
        healthUpCount.text = $"{vehicle.healthUpAbility.healthUpCount}";
    }
}
