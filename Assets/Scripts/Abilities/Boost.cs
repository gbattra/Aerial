using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public ParticleSystem boostEffect;
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public float boostSpeed;
    public float boostThrust;
    public float boostRoll;
    public float boostLift;
    public float rollBuffer;
    public float pitchBuffer;

    public bool isBoosting;
    public bool isCharging;
    public float consumptionRate;
    public float chargeRate;
    public float boostEnergy;
    
    public float charge => _charge;
    private float _charge;

    public void Start()
    {
        _charge = 1f;
    }

    private void Update()
    {
        if (isBoosting)
        {
            boostEffect.gameObject.SetActive(true);
            var newCharge = _charge * 100 - Time.deltaTime * consumptionRate;
            _charge = Mathf.Clamp(newCharge, 0, 100) / 100;
        } else
            boostEffect.gameObject.SetActive(false);

        if (isCharging)
        {
            var newCharge = _charge * 100 + Time.deltaTime * chargeRate;
            _charge = Mathf.Clamp(newCharge, 0, 100) / 100;
        }
    }

    private void FixedUpdate()
    {

        if (!Controller.b)
        {
            isCharging = true;
            isBoosting = false;
            audioSource.Stop();
            return;
        }

        if (_charge <= 0f)
        {
            audioSource.Stop();
            isBoosting = false;
            return;
        }

        isCharging = false;
        isBoosting = true;
        // audioSource.PlayOneShot(audioClip);
    }
}
