using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMachineGun : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<WeaponSwitcher>().MachineGunFinded();
        }
    }
    public void SetActive()
    {
        GetComponent<Collider>().enabled = true;
        weapon.transform.gameObject.SetActive(true);
    }
}
