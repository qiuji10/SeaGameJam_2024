using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
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
    }
}
