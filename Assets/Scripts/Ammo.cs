using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AudioSource pickupAmmoSound;
    [SerializeField] AmmoSlot[] ammoSlots;
    [System.Serializable]   
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
        public int ammoAmountReserv; 
    }

    public int GetAmmoAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public int GetAmmoAmountReserv(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmountReserv;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType )
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }   
    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount )
    {
        GetAmmoSlot(ammoType).ammoAmountReserv+=ammoAmount ;
        pickupAmmoSound.Play();
    }  
    
    public void Recharge(AmmoType ammoType, int ammoAmount )
    {
        //int reserv = GetAmmoSlot(ammoType).ammoAmountReserv;
        //int ammo = GetAmmoAmount(ammoType);
        if (GetAmmoAmount(ammoType) > 0 && GetAmmoAmount(ammoType) < ammoAmount)
        {
            ammoAmount = ammoAmount - GetAmmoAmount(ammoType);
        }
        if (GetAmmoSlot(ammoType).ammoAmountReserv < ammoAmount)
        {
            ammoAmount = GetAmmoSlot(ammoType).ammoAmountReserv;
        }
        GetAmmoSlot(ammoType).ammoAmountReserv -= ammoAmount;
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot (AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}

