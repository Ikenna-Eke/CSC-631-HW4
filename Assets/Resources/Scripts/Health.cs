using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    void Start()
    {
        
    }

    public Text healthText;
    public int health = 100;

    void Update()
    {
        healthText.text = "Health : " + health; 
        if(health <= 0)
        {
            healthText.text = "You lose";
        }
    }

    public void Damage()
    {
        health -= 25;
    }
}
