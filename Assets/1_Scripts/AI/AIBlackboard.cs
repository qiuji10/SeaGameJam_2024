using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlackboard : MonoBehaviour
{
    [Header("Config Variables")]
    public float baseSpeed = 4;
    public float chaseSpeed = 8;

    [Header("Runtime Variables")]
    public float speed;
    public EDirection direction;

    public List<Transform> waypoints;
    public int destinationIndex;

    public Animator animator;
    public Rigidbody2D rb;
    public AIHealth health;
    public AITransform agentTransform;
    public ConeDetection alertDetection;
    public ConeDetection attackDetection;

    public Transform target;
    public GameObject weaponGameObject;
    public IWeapon weapon;

    private void Awake()
    {
        weapon = weaponGameObject.GetComponent<IWeapon>();
    }
}
