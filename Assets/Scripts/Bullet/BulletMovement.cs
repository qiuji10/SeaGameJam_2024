using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxTravelDistance = 10f; // Maximum distance the bullet can travel
    [SerializeField] private int damageAmount = 1; // Amount of damage the bullet does

    private Rigidbody2D rb;
    private Vector3 startPosition;  // To track the initial position of the bullet

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
        startPosition = transform.position;  // Store the bullet's starting position
    }

    void Update()
    {
        // Calculate the distance traveled from the start position
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        // If the bullet has traveled further than the max distance, destroy it
        if (distanceTraveled >= maxTravelDistance)
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed after traveling max distance.");
        }
    }

    // This method is triggered when the bullet hits another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Enemy" tag
        if (collision.CompareTag("Enemy"))
        {
            // Attempt to get the AIHealth component from the enemy
            AIHealth enemyHealth = collision.GetComponent<AIHealth>();

            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.Damage(damageAmount);

                // Check if the enemy is dead, then destroy the enemy object
                if (enemyHealth.IsDead())
                {
                    Destroy(collision.gameObject);
                    Debug.Log("Enemy destroyed!");
                }

                // Destroy the bullet after it hits an enemy
                Destroy(gameObject);
                Debug.Log("Bullet destroyed after hitting an enemy.");
            }
        }
    }

    public void AdjustRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (col.gameObject.TryGetComponent(out IHealth healthComponent))
            {
                Debug.Log("damage");
                healthComponent.Damage(1);
                
            }
        }

        Destroy(gameObject);
    }
}
