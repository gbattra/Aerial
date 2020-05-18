using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpAbility : MonoBehaviour
{
    public Vehicle vehicle;
    public float healingTime;
    
    public int healthUpCount => _healthUpCount;
    private int _healthUpCount;

    public bool isHealing => _isHealing;
    private bool _isHealing;
    
    private float healingStartTime;

    public void Start()
    {
        _healthUpCount = 3;
    }

    public void Update()
    {
        if (!isHealing && Controller.x && healthUpCount > 0)
        {
            _healthUpCount = Mathf.Clamp(_healthUpCount - 1, 0, 3);
            _isHealing = true;
            healingStartTime = Time.time;
            vehicle.IncreaseHealth(.25f);
        }

        _isHealing &= Time.time - healingStartTime < healingTime;

        if (isHealing)
            return;

        _isHealing = false;
        healingStartTime = 0;
    }

    public void AddHealthUp(int count)
    {
        _healthUpCount = Mathf.Clamp(_healthUpCount + count, 0, 3);
    }
}
