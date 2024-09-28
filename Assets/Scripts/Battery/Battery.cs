using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public int quantity = 1;
    public int volts = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playermovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (playermovement != null)
            {
                playermovement.AddBattery(quantity, volts);
                Destroy(gameObject);
            }
        }
    }
}
