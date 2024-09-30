using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    // This variable will hold a reference to the PlayerMovement script to access the battery count
    private PlayerMovement playerMovement;

    private void Start()
    {
        // Find the player object and get the PlayerMovement component
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the door has the Player tag
        if (other.CompareTag("Player"))
        {
            // Check if the player has collected enough batteries
            if (playerMovement.totalBatteries >= 10)
            {
                UI_EndGame.instance.ShowWinPanel();
                Debug.Log("You Win!");
            }

            else
            {
                Debug.Log("Not Enough Batteries");
            }
        }
    }
}
