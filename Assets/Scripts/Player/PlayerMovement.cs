using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float movementSpeed = 8f;
    private float affectedSpeed;
    private float initialjumpPower = 16f;
    private float jumpPower;
    private float minjumpPower = 1f;
    private float speedReductionFactor = 0.5f;


    public int totalBatteries = 0;
    public int totalVolts = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;


    private void Start()
    {
        affectedSpeed = movementSpeed;
        jumpPower = initialjumpPower;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 2.0f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded && totalBatteries < 10)
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpPower);
            Debug.Log("Jump Button Pressed! Jump Power: " + jumpPower);
        }

        else if (totalBatteries > 10)
        {
            Debug.Log("Player cannot Jump!");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropBattery();
        }

        AdjustJump();

        affectedSpeed = Mathf.Max(3f, movementSpeed - totalBatteries * speedReductionFactor);

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * affectedSpeed, rb.velocity.y);
    }

    public void AddBattery(int quantity, int volts)
    {
        totalBatteries += quantity;
        totalVolts += volts * quantity;
        AdjustJump();
        Debug.Log("Battery Picked Up! Total Batteries: " + totalBatteries + ", Total Volts: " + totalVolts);
    }

    public void DropBattery()
    {
        if (totalBatteries > 0)
        {
            totalBatteries -= 1;
            totalVolts -= 5;
            Vector3 dropPos = transform.position + new Vector3(1f, 0f, 0f);
            Instantiate(batteryPrefab, dropPos, Quaternion.identity);
            Debug.Log("Removed 1 Battery! Total Batteries: " + totalBatteries + ", Total Volts: " + totalVolts);
            AdjustJump();
        }

        else
        {
            Debug.Log("No Batteries to Drop!");
        }
    }

    private void AdjustJump()
    {
        if (totalBatteries == 0)
        {
            jumpPower = initialjumpPower;
        }
        else if (totalBatteries >= 10)
        {
            jumpPower = 0f;
        }
        else
        {
            float batteryRatio = totalBatteries / 10f;
            jumpPower = Mathf.Lerp(initialjumpPower, minjumpPower, batteryRatio);
        }

        Debug.Log("Adjusted Jump Power: " + jumpPower);

    }
}
