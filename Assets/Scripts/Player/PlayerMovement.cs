using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float movementSpeed = 8f;
    private float affectedSpeed;
    private float jumpPower = 16f;
    private float speedReductionFactor = 0.5f;


    public int totalBatteries = 0;
    public int totalVolts = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;


    private void Start()
    {
        affectedSpeed = movementSpeed;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 2.0f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpPower);
            Debug.Log("Jump Button Pressed!");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddBatteries(totalBatteries, totalVolts);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveBatteries();
        }

        affectedSpeed = Mathf.Max(3f, movementSpeed - totalBatteries * speedReductionFactor); 

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * affectedSpeed, rb.velocity.y);
    }

    public void AddBatteries(int quantity, int volts)
    {
        totalBatteries += quantity;
        totalVolts += volts * quantity;
        Debug.Log("Battery Picked Up! Total Batteries: " + totalBatteries + ", Total Volts: " + totalVolts);
    }

    public void RemoveBatteries()
    {
        if (totalBatteries > 0)
        {
            totalBatteries -= 1;
            Debug.Log("Removed 1 Battery! Total Batteries: " + totalBatteries);
        }
    }
}
