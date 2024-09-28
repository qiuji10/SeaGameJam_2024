using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce;
    private int batteryVolts = 5;
    private int remainingVolts;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();    
        remainingVolts = batteryVolts;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerMovement.totalVolts > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (remainingVolts > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();

            if (bulletMovement != null)
            {
                float desiredAngle = -90f;
                bulletMovement.AdjustRotation(desiredAngle);
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * shootForce, ForceMode2D.Impulse);

            remainingVolts -= 1;
            playerMovement.totalVolts -= 1;
            Debug.Log("Shots Fired! Volts remaining in current battery: " + remainingVolts);
        }

        else
        {
            if (playerMovement.totalBatteries > 0)
            {
                playerMovement.totalBatteries -= 1;
                playerMovement.totalVolts -= batteryVolts;
                remainingVolts = batteryVolts - 1;

                playerMovement.totalVolts = Mathf.Max(0, playerMovement.totalVolts);
                Debug.Log("Battery Destroyed! Total Batteries: " + playerMovement.totalBatteries + ", Total Volts: " + playerMovement.totalVolts);
            }

            else
            {
                Debug.Log("No Batteries left to Shoot!");
            }
        }
    }

    public void ResetVolts()
    {
        remainingVolts = batteryVolts;
    }
}
