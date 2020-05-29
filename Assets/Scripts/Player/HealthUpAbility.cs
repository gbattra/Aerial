using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpAbility : MonoBehaviour
{
    public Vehicle vehicle;
    public ParticleSystem healAura;
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public float healingTime;
    
    public int healthUpCount => _healthUpCount;
    private int _healthUpCount;

    public bool isHealing => _isHealing;
    private bool _isHealing;
    
    private float healingStartTime;

    public void Start()
    {
        _healthUpCount = 1;
    }

    public void Update()
    {
        if (!isHealing && Controller.x && healthUpCount > 0)
        {
            _healthUpCount = Mathf.Clamp(healthUpCount - 1, 0, healthUpCount);
            _isHealing = true;
            healingStartTime = Time.time;
            vehicle.IncreaseHealth(.25f);
            
            healAura.gameObject.SetActive(true);
            healAura.Play();
            audioSource.PlayOneShot(audioClip);
        }

        _isHealing &= Time.time - healingStartTime < healingTime;

        if (isHealing)
            return;

        _isHealing = false;
        healingStartTime = 0;
        healAura.gameObject.SetActive(false);
    }

    public void AddHealthUp(int count)
    {
        _healthUpCount = Mathf.Clamp(healthUpCount + count, 0, healthUpCount + count);
    }
}
