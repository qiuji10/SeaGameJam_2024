using System.Collections.Generic;
using UnityEngine;

public class ConeDetection : MonoBehaviour
{
    public float detectionAngle = 45f;
    public float detectionDistance = 5f;
    public LayerMask targetMask;
    public Color gizmosColor = Color.red;

    void Update()
    {
        DetectTargets();
    }

    void DetectTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, detectionDistance, targetMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            float angleToTarget = Vector2.Angle(transform.up, directionToTarget);

            if (angleToTarget < detectionAngle)
            {
                Debug.Log("Target detected: " + target.name);
                // Optionally: Perform actions with the detected target
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        Vector3 directionA = Quaternion.Euler(0, 0, -detectionAngle) * transform.up * detectionDistance;
        Vector3 directionB = Quaternion.Euler(0, 0, detectionAngle) * transform.up * detectionDistance;

        Gizmos.DrawLine(transform.position, transform.position + directionA);
        Gizmos.DrawLine(transform.position, transform.position + directionB);
    }
}
