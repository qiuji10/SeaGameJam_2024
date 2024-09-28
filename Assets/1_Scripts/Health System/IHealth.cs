using UnityEngine;

public interface IHealth
{
    int CurHealth { get; set; }
    int MaxHealth { get; set; }

    void Damage(int damage);

    bool IsDead();
}

namespace Jam.Sample
{
    public class SampleHealth : IHealth
    {
        public int CurHealth { get; set; }
        public int MaxHealth { get; set; }

        private void Start()
        {
            MaxHealth = 100;
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

        public void Heal(int healAmount)
        {
            CurHealth += healAmount;
            CurHealth = Mathf.Clamp(CurHealth, 0, MaxHealth); // Prevent overhealing
        }

        public bool IsDead()
        {
            return CurHealth <= 0;
        }

        private void OnDeath()
        {
            Debug.Log("Player is dead");
        }
    }
}
