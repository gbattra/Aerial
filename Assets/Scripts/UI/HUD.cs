using DuloGames.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Vehicle vehicle;
    public Player player;
    public LevelManager levelManager;

    public UltimateStatusBar levelProgressRadial;
    public UIBulletBar healthRadialBar;
    public UIProgressBar minigunCharge;
    public UIProgressBar boostCharge;

    public Text healthRadialBarText;
    public Text minigunChargeText;
    public Text boostChargeText;

    public TextMeshProUGUI levelNumberSingle;
    public TextMeshProUGUI levelNumberDouble;
    public TextMeshProUGUI timeElapsed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI levelAnnouncement;
    public TextMeshProUGUI shieldCount;
    public TextMeshProUGUI healthUpCount;

    private int currentLevelNumber;
    private float timeLevelClearAnnounced;
    private bool announcingLevelClear;

    public void Start()
    {
        levelAnnouncement.text = "";
        currentLevelNumber = levelManager.levelNumber;
        healthRadialBar.fillAmount = 1f;
        healthRadialBarText.text = $"{(int) (healthRadialBar.fillAmount * 100)}%";

        minigunCharge.fillAmount = 1f;
        minigunChargeText.text = $"{(int) (minigunCharge.fillAmount * 100)}%";

        boostCharge.fillAmount = 1f;
        boostChargeText.text = $"{(int) (boostCharge.fillAmount * 100)}%";
        
        levelProgressRadial.UpdateStatus(1f, 1f);
        levelNumberSingle.text = levelManager.levelNumber < 10 ? $"{levelManager.levelNumber}" : "";
        levelNumberDouble.text = levelManager.levelNumber > 10 ? $"{levelManager.levelNumber}" : "";
        timeElapsed.text = levelManager.timer.Elapsed.ToString(@"m\:ss");
        score.text = $"{player.score}";

        shieldCount.text = $"{vehicle.shieldAbility.shieldCount}";
        healthUpCount.text = $"{vehicle.healthUpAbility.healthUpCount}";
    }

    public void LateUpdate()
    {
        healthRadialBar.fillAmount = vehicle.health;
        healthRadialBarText.text = $"{(int) (vehicle.health * 100)}%";

        minigunCharge.fillAmount = vehicle.minigun.charge;
        minigunChargeText.text = $"{(int) (vehicle.minigun.charge * 100)}%";

        boostCharge.fillAmount = vehicle.boost.charge;
        boostChargeText.text = $"{(int) (vehicle.boost.charge * 100)}%";

        levelProgressRadial.UpdateStatus(levelManager.percentProgress, 1f);
        levelNumberSingle.text = levelManager.levelNumber < 10 ? $"{levelManager.levelNumber}" : "";
        levelNumberDouble.text = levelManager.levelNumber >= 10 ? $"{levelManager.levelNumber}" : "";
        timeElapsed.text = levelManager.timer.Elapsed.ToString(@"m\:ss");
        score.text = $"{player.score}";

        shieldCount.text = $"{vehicle.shieldAbility.shieldCount}";
        healthUpCount.text = $"{vehicle.healthUpAbility.healthUpCount}";

        if (levelManager.isCountingDown)
        {
            levelProgressRadial.UpdateStatus(
                levelManager.countdownSecondsRemaining, levelManager.secondsAtMax);
            if (levelManager.countdownSecondsRemaining < 3)
                levelAnnouncement.text = "3";
            if (levelManager.countdownSecondsRemaining < 2)
                levelAnnouncement.text = "2";
            if (levelManager.countdownSecondsRemaining < 1)
                levelAnnouncement.text = "1";
        } else if (levelManager.levelNumber > currentLevelNumber)
        {
            announcingLevelClear = true;
            currentLevelNumber++;
            levelAnnouncement.text = "LEVEL CLEAR";
            timeLevelClearAnnounced = Time.time;
        }

        if (announcingLevelClear && Time.time - timeLevelClearAnnounced > 3f)
        {
            announcingLevelClear = false;
            levelAnnouncement.text = "";
        }
    }
}
