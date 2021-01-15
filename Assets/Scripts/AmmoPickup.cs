using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount=5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] int maxAmmo;
    int currentAmmoAmountReserv;
    private void OnTriggerEnter(Collider other)
    {       
        
        currentAmmoAmountReserv = FindObjectOfType<Ammo>().GetAmmoAmountReserv(ammoType);
        if (other.gameObject.tag == "Player" && currentAmmoAmountReserv <= maxAmmo - ammoAmount)
        {
            PickUp();
        }
        else if (other.gameObject.tag == "Player" && currentAmmoAmountReserv > maxAmmo - ammoAmount && currentAmmoAmountReserv < maxAmmo)
        {
            ammoAmount = maxAmmo - currentAmmoAmountReserv;
            PickUp();
        }
        else return;
              
    }

    private void PickUp()
    {  
        FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
        FindObjectOfType<Weapon>().DisplayAmmoReserv();
        Destroy(gameObject);
    }
}
