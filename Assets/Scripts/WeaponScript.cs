using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] Camera FPcamera;
    [SerializeField] float range = 100f;
    [SerializeField] ParticleSystem muzzleflash;

    EnemyAi Enemy;
    public RaycastHit hit;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        PlayMuzzleFlash();
        Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hit, range);
        Debug.Log(hit.transform.name);
    }

    public void PlayMuzzleFlash()
    {
        muzzleflash.Play();
    }
}
