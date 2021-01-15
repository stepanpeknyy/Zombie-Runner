using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 10;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>(); 
    }

    public void AttackEvent()
    {
        if (target == null) return;
        target.GetComponent<PlayerHealth>().TakeDamage(damage);
        target.GetComponent<DamageDisplay>().ShowDamageImpact();
    }
}
