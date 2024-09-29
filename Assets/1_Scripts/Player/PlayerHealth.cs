using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int CurHealth { get; set; }
    public int MaxHealth { get; set; } = 1;

    private void Awake()
    {
        CurHealth = MaxHealth;
    }

    public void Damage(int damage)
    {
        CurHealth -= damage;
        CurHealth = Mathf.Clamp(CurHealth, 0, MaxHealth); // Prevent health from going below 0

        if (IsDead())
        {
            OnDeath();
        }
    }

    public bool IsDead()
    {
        return CurHealth <= 0;
    }

    private void OnDeath()
    {
        UI_EndGame.instance.ShowLosePanel();
    }
}
