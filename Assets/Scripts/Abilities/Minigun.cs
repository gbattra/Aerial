using System;
using UnityEngine;


public class Minigun : MonoBehaviour
{
    public Vehicle vehicle;
    public ParticleSystem ProjectilePs;
    public ParticleSystem MuzzleFlashPs;
    public ParticleSystem SleevesPs;

    public float charge => _charge;
    private float _charge;
    
    public float FireRate;
    public float consumptionRate;
    public float chargeRate;
    
    public bool isFiring;
    public bool isCharging;

    public AudioClip AudioClip;
    public AudioSource AudioSource;

    private float _time;

    public void Start()
    {
        _charge = 1f;
    }

    private void Update()
    {
        if (isFiring)
        {
            var newCharge = _charge * 100 - Time.deltaTime * consumptionRate;
            _charge = Mathf.Clamp(newCharge, 0, 100) / 100;
        }
        if (isCharging)
        {
            var newCharge = _charge * 100 + Time.deltaTime * chargeRate;
            _charge = Mathf.Clamp(newCharge, 0, 100) / 100;
        }
    }

    private void FixedUpdate()
    {

        if (Math.Abs(Controller.rightTrigger) < 1f)
        {
            isCharging = true;
            isFiring = false;
            return;
        }

        if (_charge <= 0f)
            return;

        isCharging = false;
        isFiring = true;

        _time += Time.deltaTime;
		
        if (_time < FireRate || vehicle.shieldAbility.isShielding)
            return;

        ProjectilePs.Emit(1);
        MuzzleFlashPs.Play(true);
        SleevesPs.Emit(1);

        if (AudioSource != null && AudioClip != null)
            AudioSource.PlayOneShot(AudioClip);

        _time = 0;
    }
}