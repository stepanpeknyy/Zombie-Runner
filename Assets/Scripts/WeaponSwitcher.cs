using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int currentWeapon = 0;
    bool isShotgunAvailable = false;
    bool isMachineGunAvailable = false;
 
    void Start()
    {
        SetWeaponActive();
    }

    public void ShortgunFinded()
    {
        isShotgunAvailable = true;
        currentWeapon = 1;
        SetWeaponActive();
        GetComponent<AudioSource>().Play();
    }
    public void MachineGunFinded()
    {
        isMachineGunAvailable = true;
        currentWeapon = 2;
        SetWeaponActive();
        GetComponent<AudioSource>().Play();
    }
    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
                GetComponent<AudioSource>().Play();
                if (currentWeapon == 1)
                {
                    FindObjectOfType<Weapon>().ShortGunIsActive();
                }
                else
                {
                    FindObjectOfType<Weapon>().ShortGunIsNotActive();
                }
            }
            else
            {
                weapon.gameObject.SetActive(false );
            }
            weaponIndex++;
        }
    }

    void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessScrollWheel();
        ProcessKeyInput();

        if (previousWeapon != currentWeapon) SetWeaponActive();
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeapon = 0;
        if  (Input.GetKeyDown(KeyCode.Alpha2) && isShotgunAvailable ==true) currentWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && isMachineGunAvailable ==true) currentWeapon = 2;
    }

    private void ProcessScrollWheel()
    {
        //Only Pistol
        if (Input.GetAxis("Mouse ScrollWheel")<0 && isMachineGunAvailable==false && isShotgunAvailable ==false)
        {
            currentWeapon = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isMachineGunAvailable == false && isShotgunAvailable == false)
        {
            currentWeapon = 0;
        }

        //Pistol + Shortgun
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && isMachineGunAvailable == false && isShotgunAvailable == true)
        {
            if (currentWeapon >= transform.childCount - 2)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon ++ ;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isMachineGunAvailable == false && isShotgunAvailable == true)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 2;
            }
            else
            {
                currentWeapon--;
            }
        }

        //Pistol + Shortgun + Machine gun
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && isMachineGunAvailable == true && isShotgunAvailable == true)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isMachineGunAvailable == true && isShotgunAvailable == true)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

        //Pistol + Machine gun
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && isMachineGunAvailable == true && isShotgunAvailable == false )
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon = currentWeapon + 2;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isMachineGunAvailable == true && isShotgunAvailable == false)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon = currentWeapon - 2;
            }
        }

    }
}
