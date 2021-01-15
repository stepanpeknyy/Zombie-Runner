using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShortgun : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<WeaponSwitcher>().ShortgunFinded();
        }
    }
    public void SetActive ()
    {
        GetComponent<Collider>().enabled = true;
        weapon.transform.gameObject.SetActive(true);
    }
}
