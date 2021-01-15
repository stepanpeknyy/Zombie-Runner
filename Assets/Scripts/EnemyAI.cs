using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] float chaseRange=15f;
    [SerializeField] float turnSpeed = 5;
    [SerializeField] float increaseChaseRange = 1.75f;
    [SerializeField] AudioSource gotDamageSound;
    [SerializeField] AudioSource provokeSound;
    [SerializeField] GameObject idleTargetObject;

    Transform target;
    Transform idleTarget;
    EnemyHealth health;
    NavMeshAgent navMeshAgent;
    float distanseToTarget = Mathf.Infinity;
    float distanseToIdleTarget = Mathf.Infinity;
    bool isProvoked = false;
    bool provokeSoundPlayed = false;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health  = GetComponent<EnemyHealth >();
        target = FindObjectOfType<PlayerHealth>().transform ;
        idleTarget = idleTargetObject.transform ; 
    }

    // Update is called once per frame
    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            
        }
        distanseToTarget = Vector3.Distance(target.position, transform.position);
        distanseToIdleTarget = Vector3.Distance(idleTarget.position, transform.position);
        if (isProvoked && !health.IsDead ())
        {
            SetNormalspeed();
            EngageTarget();
        }
        else if (distanseToTarget <= chaseRange && !health.IsDead())
        {
            SetNormalspeed();
            navMeshAgent.SetDestination(target.position);
        }
        else if (distanseToTarget >= chaseRange)
        {
            chaseRange += Time.deltaTime * increaseChaseRange;
        }
        
        PlayProvokeSound();
        if (!isProvoked && !health.IsDead() )
        {
            SetWalkSpeed();
            FaceIdleTarget();
            if (distanseToIdleTarget >= navMeshAgent.stoppingDistance)
            {
                ChaseIdleTarget();
            }
            if (distanseToTarget<= chaseRange)
            {
                isProvoked = true;
            }
        }
    }

    private void SetNormalspeed()
    {
        GetComponent<Animator>().speed = 1f;
        navMeshAgent.speed = 3.5f;
    }

    private void SetWalkSpeed()
    {
        navMeshAgent.speed = 0.6f;
        GetComponent<Animator>().speed = 0.6f;
    }

    private void PlayProvokeSound()
    {  
            if (provokeSoundPlayed == false && isProvoked ==true)
            {
                provokeSound.Play();
                provokeSoundPlayed = true;
            }
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanseToTarget>=navMeshAgent.stoppingDistance )
        {          
            ChaseTarget();
        }
        if  (distanseToTarget <= navMeshAgent.stoppingDistance )                  
        {
            AttackTarget();
        }
    }

    public void OnDamageTaken()
    {
        if (!health.IsDead())
        {
            isProvoked = true;
            gotDamageSound.Play();           
        }
    } 

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);    
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }
    private void ChaseIdleTarget()
    {
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(idleTarget.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    private void FaceIdleTarget()
    {
        Vector3 direction = (idleTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
