using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5;
    public float maxDistance = 5f;

    private Vector2 origin;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        origin = transform.position;
    }

    public void SetDirection(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        _rb.velocity = speed * Time.fixedDeltaTime * transform.right;

        if (Vector2.Distance(origin, transform.position) >= maxDistance)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            if (col.gameObject.TryGetComponent(out IHealth healthComponent))
            {
                healthComponent.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
