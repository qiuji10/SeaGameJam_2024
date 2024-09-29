using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStorage : MonoBehaviour
{
    public int minimumRequirement;
    public PlayerMovement player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (player.GetFullBatteryCount() >= minimumRequirement)
            {
                UI_EndGame.instance.ShowWinPanel();
            }
        }
    }
}
