using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100;
    [SerializeField] AudioSource dieSound;
    bool isDead=false;

    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        BroadcastMessage("OnDamageTaken");
        if (hitPoints<= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        
        GetComponent<CapsuleCollider>().enabled = false;
        if (isDead) return;
        isDead = true; 
        GetComponent<Animator>().SetTrigger("die");
        dieSound.Play();
        Destroy(gameObject, 5f);
        FindObjectOfType<PlayerHealth>().EnemyKilled();
        FindObjectOfType<PlayerHealth>().DisplayScore();       
    }
}
