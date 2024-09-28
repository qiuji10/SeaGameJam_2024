using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Settings")]
    public float cd;
    public float range;

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
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.maxDistance = range;
    }

    public bool CanAttack()
    {
        return cdTimer <= 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(firePoint.position, range);
    }
}
