using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour, IHealth
{
    [SerializeField] private bool applyMaxOnAwake = true;
    [field:SerializeField] public int CurHealth { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; } = 1;

    private void Awake()
    {
        if (applyMaxOnAwake)
            CurHealth = MaxHealth;
    }

    public void Damage(int damage)
    {
        CurHealth -= damage;
        CurHealth = Mathf.Clamp(CurHealth, 0, MaxHealth); // Prevent health from going below 0

        if (IsDead())
        {
            //OnDeath();
        }
    }

    public bool IsDead()
    {
        return CurHealth <= 0;
    }
}
