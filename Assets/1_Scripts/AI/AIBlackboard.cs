using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AIBlackboard : MonoBehaviour
{
    [Header("[Config Variables]")]
    public float baseSpeed = 4;
    public float chaseSpeed = 8;
    public List<Transform> waypoints;

    [Header("[Runtime Variables]")]
    public float speed;
    public int destinationIndex;
    public EDirection direction;

    [Header("[Engine References]")]
    public Animator animator;
    public Rigidbody2D rb;

    [Header("[AI References]")]
    public AIHealth health;
    public AITransform agentTransform;
    public ConeDetection alertDetection;
    public ConeDetection attackDetection;

    [Header("[Weapon References]")]
    public Transform target;
    public GameObject weaponGameObject;

    [Header("[Prefabs]")]
    public GameObject warningPopUp;

    public IWeapon weapon;

    private void Awake()
    {
        weapon = weaponGameObject.GetComponent<IWeapon>();
    }
}
