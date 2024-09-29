using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_jmup : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    public float groundInFrontRange = 2f;  // Range for checking the ground in front
    public float gapAheadRange = 2f;       // Range for checking gaps ahead
    public float platformAboveRange = 3f;  // Range for checking platforms above

    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _shouldJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer); // ground detection
        float direction = Mathf.Sign(player.position.x - transform.position.x); // player direction
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, platformAboveRange, 1 << player.gameObject.layer);

        if (_isGrounded)
        {
            _rb.velocity = new Vector2(direction * chaseSpeed, _rb.velocity.y);

            // Check if there is ground in front of the character
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), groundInFrontRange, groundLayer);

            // Check if there is a gap ahead
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, gapAheadRange, groundLayer);

            // Check if there is a platform above
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, platformAboveRange, groundLayer);

            bool gapInfront = !groundInFront.collider && !gapAhead.collider;
            bool playerAbove = isPlayerAbove && platformAbove.collider;

            if (gapInfront || playerAbove)
            {
                _shouldJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isGrounded && _shouldJump)
        {
            _shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 jumpDirection = direction * jumpForce;
            _rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }

    // This function draws the gizmos to visualize the raycasts
    private void OnDrawGizmos()
    {
        // Get direction based on the player's position relative to the character
        if (player == null) return;
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Ray to check ground directly under the character
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1f); // Ground detection ray

        // Ray to check for ground in front of the character
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction, 0, 0) * groundInFrontRange); // Ground in front detection ray

        // Ray to check for gaps ahead
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(direction, 0, 0), transform.position + new Vector3(direction, -gapAheadRange, 0)); // Gap detection ray

        // Ray to check if there is a platform above
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * platformAboveRange); // Platform above detection ray
    }
}
