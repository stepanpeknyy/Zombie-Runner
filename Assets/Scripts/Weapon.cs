using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range=100f;
    [SerializeField] float damage= 25;
    [SerializeField] float rechargeTime = 1.5f;
    [SerializeField] float shootDelay = 0.8f;
    [SerializeField] int ammoAmount;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Text ammoText;
    [SerializeField] Text ammoReservText;
    [SerializeField] AudioSource shotSound;
    [SerializeField] AudioSource outOfAmmoSound;
    [SerializeField] AudioSource rechargeSound;


    float headShotMultiplier = 2.5f;
    bool canShoot = true;
    bool shortgunIsActive = false;

    private void OnEnable()
    {
        DisplayAmmoReserv();
        canShoot = true;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot == true )
        {           
            StartCoroutine(Shoot());
        }
        if (Input.GetKeyDown(KeyCode.R) && ammoSlot.GetAmmoAmount(ammoType)< ammoAmount )
        {
            StartCoroutine(Recharge());
        }
        DisplayAmmo();
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetAmmoAmount(ammoType);
        ammoText.text = "Ammo " + currentAmmo.ToString ();
    }

    IEnumerator Recharge()
    {
        canShoot = false;
        rechargeSound.Play();
        yield return new WaitForSeconds(rechargeTime);
        ammoSlot.Recharge(ammoType, ammoAmount);
        canShoot = true;
        DisplayAmmoReserv();
    }

    public void DisplayAmmoReserv()
    {
        int currentAmmoReserv = ammoSlot.GetAmmoAmountReserv(ammoType);
        ammoReservText.text = "|| " + currentAmmoReserv.ToString();
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetAmmoAmount(ammoType) > 0)
        {
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayMuzzleFlash();
            RayCastProcess();
        }
        else
        {
            outOfAmmoSound.Play();         
        }
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    private void RayCastProcess()
    {
        RaycastHit hit;        
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {

            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            ShootShake();
            if (target == null) return;
            if (hit.collider is SphereCollider && !shortgunIsActive)
            {
                HeadShot(target);
            }
            else if (!shortgunIsActive)
            {
                target.TakeDamage(damage);
            }

            if (shortgunIsActive == true)
            {
                float distance = Vector3.Distance(FPCamera.transform.position, hit.point);
                if (distance >= range - range / 3 && distance < range)
                {
                    if (hit.collider is SphereCollider)
                    {
                        damage = damage / 5;
                        HeadShot(target);
                        damage = damage * 5;
                    }
                    else
                    {
                        FarDistance(target);
                    }

                }
                else if (distance >= range / 5 && distance < range - range / 3)
                {
                    if (hit.collider is SphereCollider)
                    {
                        damage = damage / 2;
                        HeadShot(target);
                        damage = damage * 2;
                    }
                    else
                    {
                        MiddleDistance(target);
                    }
                }
                else
                {
                    target.TakeDamage(damage);
                }
            }
        }
        
    }
    private void ShootShake()
    {
        float rotationFactor;
        rotationFactor = UnityEngine.Random.Range(-0.8f, 0.8f);
        Vector3 rotation = new Vector3(Mathf.Abs ( rotationFactor), rotationFactor, 0);
        FPCamera.transform.eulerAngles = FPCamera.transform.eulerAngles + rotation;
        
    }
    private void HeadShot(EnemyHealth target)
    {
        damage = damage * headShotMultiplier;
        target.TakeDamage(damage);
        damage = damage / headShotMultiplier;
    }

    private void MiddleDistance(EnemyHealth target)
    {
        damage = damage / 2f;
        target.TakeDamage(damage);
        damage = damage * 2f;
    }

    private void FarDistance(EnemyHealth target)
    {
        damage = damage / 5f;
        target.TakeDamage(damage);
        damage = damage * 5f;
    }

    private void CreateHitImpact(RaycastHit  hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
        shotSound.Play();
    }

    public void ShortGunIsActive()
    {
        shortgunIsActive = true;
    }
    public void ShortGunIsNotActive()
    {
        shortgunIsActive = false;
    }
}
