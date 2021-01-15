using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] int healhtAmount = 25;
    private void OnTriggerEnter(Collider other)
    {  
        float currentHealth =FindObjectOfType<PlayerHealth>().GetHealth();
        if (currentHealth  < 100)
        {
            if (other.gameObject.tag == "Player")
            {
                //FindObjectOfType<PickupSpawner>().SetSpawnTrue();
                FindObjectOfType<PlayerHealth>().IncreaseCurrentHealth(healhtAmount);
                Destroy(gameObject);
            }
        }
        else return;
    }
    
   


}
