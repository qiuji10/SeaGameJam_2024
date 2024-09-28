using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Settings'")]
    public float cd;
    public Transform firePoint;
    public Bullet bulletPrefab;

    private float cdTimer;

    public Dictionary<string, object> attributes { get; set; }

    private void Update()
    {
        if (cdTimer > 0)
        {
            cdTimer -= Time.deltaTime;
        }
    }

    [NaughtyAttributes.Button]
    public void Attack()
    {
        cdTimer = cd;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public bool CanAttack()
    {
        return cdTimer <= 0;
    }
}
