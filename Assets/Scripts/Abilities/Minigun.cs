using System;
using UnityEngine;


public class Minigun : MonoBehaviour
{
    public Vehicle vehicle;
    public ParticleSystem ProjectilePs;
    public ParticleSystem MuzzleFlashPs;
    public ParticleSystem SleevesPs;

    public float FireRate;

    public AudioClip AudioClip;
    public AudioSource AudioSource;

    private float _time;

    private void FixedUpdate()
    {
		
        if (Math.Abs(Controller.rightTrigger) < 1f)
            return;
		
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