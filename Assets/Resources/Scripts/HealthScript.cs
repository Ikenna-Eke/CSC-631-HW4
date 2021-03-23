using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Text healthText;
    public GameObject playerName;
    int health = 100;
    void Start()
    {
        //playerName = GetComponent<GameObject>();
        healthText = GetComponent<Text>();
    }

    void Update()
    {
        healthText.text =  playerName.name + ": "+ health.ToString();

        if(health <= 0)
        {
            healthText.text = "You lose";
            Time.timeScale = 0.0f;
        }
    }

    public void Damage()
    {
        health -= 25;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player2Capsule")
        {
            Damage();
        }
    }
}
