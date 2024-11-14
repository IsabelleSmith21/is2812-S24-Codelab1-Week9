using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public int isabelleHealth = 10; // Starting health for Isabelle
    public int masonHealth = 10; // Starting health for Mason

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected!"); // Debug to verify trigger

        // Check if the collision is with a bullet
        if (other.CompareTag("Bullet"))
        {
            // Check if Isabelle is hit
            if (other.GetComponent<Collider2D>().CompareTag("Isabelle"))
            {
                isabelleHealth--;
                Debug.Log("Isabelle was hit! Health: " + isabelleHealth);

                if (isabelleHealth <= 0)
                {
                    Debug.Log("Isabelle has been defeated!");
                }
            }

            // Check if Mason is hit
            else if (other.GetComponent<Collider2D>().CompareTag("Mason"))
            {
                masonHealth--;
                Debug.Log("Mason was hit! Health: " + masonHealth);

                if (masonHealth <= 0)
                {
                    Debug.Log("Mason has been defeated!");
                }
            }

            // Destroy the bullet after it hits a player
            Destroy(other.gameObject);
        }
    }
}
