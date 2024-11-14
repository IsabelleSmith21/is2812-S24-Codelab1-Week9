using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public bool isMason;

    public int maxHealth = 10; // Starting health for each player
    private int health;

    public TextMeshProUGUI healthText;
    private Vector3 initialPosition;

    public GameObject oppWonSprite;

    public PlayerHealth opponent;

    void Start()
    {
        // Store initial position and reset health
        initialPosition = transform.position;
        health = maxHealth;
        healthText.text = (isMason ? "Mason" : "Isabelle") + " health: " + health;

        if (oppWonSprite != null)
        {
            oppWonSprite.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is a bullet
        string bulletTag = isMason ? "isabelleBullet" : "masonBullet";
        if (other.CompareTag(bulletTag))
        {
            if (health > 0) 
            {
                health--; // Reduce health by 1
                healthText.text = (isMason ? "Mason" : "Isabelle") + " health: " + health;
            }
            /*Debug.Log(gameObject.name + " was hit! Health: " + health);*/
            

            // Destroy the bullet on impact
            Destroy(other.gameObject);

            // Check if the player has been defeated
            if (health <= 0)
            {
                DeclareWinner();
            }
        }
    }

    void DeclareWinner()
    {
        /*// Display win message
        oppWonSprite.SetActive(true);

        // Delay before resetting
        Invoke("ResetGame", 3.0f); // Adjust delay time as needed*/

        if (!oppWonSprite.activeSelf && !opponent.oppWonSprite.activeSelf)
        {
            oppWonSprite.SetActive(true);
            Invoke("ResetGame", 3.0f);
        }
    }

    void ResetGame()
    {
        // Reset each player's health and position
        health = maxHealth;
        transform.position = initialPosition;
        healthText.text = (isMason ? "Mason" : "Isabelle") + " health: " + health;

        if (opponent != null)
        {
            opponent.health = opponent.maxHealth;
            opponent.transform.position = opponent.initialPosition;
            opponent.healthText.text = (isMason ? "Isabelle" : "Mason") + " health: " + opponent.health;
        }

        oppWonSprite.SetActive(false);
        //also need to reset position and health of opponent
    }
}
