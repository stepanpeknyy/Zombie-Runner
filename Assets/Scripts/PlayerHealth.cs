using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100;
    [SerializeField] AudioSource playerGotDamageSound;
    [SerializeField] AudioSource pickupFirstAidKitSound;
    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;
    [SerializeField] Canvas hintCanvas;    
    [SerializeField] Canvas shortgunCanvas;
    [SerializeField] Canvas machineGunCanvas;
    [SerializeField] int scoreForShortgun=10;
    [SerializeField] int scoreForMachineGun=30;

    int score=0;
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        playerGotDamageSound.Play();
        DisplayHealth();
        if (hitPoints <= 0)
        {
           GetComponent < DeathHandler>().HandleDeath ();
        }
    }

    private void Start()
    {
        machineGunCanvas.enabled = false;
        shortgunCanvas.enabled = false;
        hintCanvas.enabled = true;
        healthText.text = "Health " + hitPoints;
        StartCoroutine(HideText());
    }

    public float GetHealth( )
    {
       return hitPoints;
    }

    public void IncreaseCurrentHealth(int healhtAmount)
    {
        
        hitPoints += healhtAmount;
        if (hitPoints > 100) hitPoints = 100;
        DisplayHealth();
        pickupFirstAidKitSound.Play();
    }
    public void DisplayHealth()
    {
        healthText.text = "Health " + hitPoints;
    }

    public void EnemyKilled()
    {
        score++;
        if(score ==scoreForShortgun)
        {
            FindObjectOfType<PickupShortgun>().SetActive();
            shortgunCanvas.enabled = true;
            StartCoroutine(HideText());
        }
        if (score == scoreForMachineGun)
        {
            FindObjectOfType<PickupMachineGun>().SetActive();
            machineGunCanvas.enabled = true;
            StartCoroutine(HideText());
        }
    }
    public void DisplayScore()
    {
        scoreText.text = "Zombies killed " + score;
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(15);
        machineGunCanvas .enabled = false;
        shortgunCanvas .enabled = false;
        hintCanvas .enabled = false;
    }
}
