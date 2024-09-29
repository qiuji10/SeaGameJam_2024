using System.Collections.Generic;
using UnityEngine;

public class ConeDetection : MonoBehaviour
{
    public bool detected;
    public GameObject target;

    public float detectionAngle = 45f;
    public float detectionDistance = 5f;
    public LayerMask targetMask;
    public LayerMask excludeMask;

#if UNITY_EDITOR
    public Color gizmosColor = Color.red;
#endif
    void Update()
    {
        detected = DetectTargets();
    }

    bool DetectTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, detectionDistance, targetMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            float angleToTarget = Vector2.Angle(transform.up, directionToTarget);

            if (angleToTarget < detectionAngle)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, detectionDistance, ~excludeMask);

                if (hit.collider != null && hit.collider.gameObject == target.gameObject)
                {
                    this.target = target.gameObject;
                    Debug.Log("Target detected: " + target.name);
                    return true;
                }
            }
        }

        //this.target = null;
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        Vector3 directionA = Quaternion.Euler(0, 0, -detectionAngle) * transform.up * detectionDistance;
        Vector3 directionB = Quaternion.Euler(0, 0, detectionAngle) * transform.up * detectionDistance;

        Gizmos.DrawLine(transform.position, transform.position + directionA);
        Gizmos.DrawLine(transform.position, transform.position + directionB);

        if (target != null)
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, detectionDistance, ~excludeMask);
            Gizmos.color = hit.collider != null && hit.collider.gameObject == target ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
#endif
}
