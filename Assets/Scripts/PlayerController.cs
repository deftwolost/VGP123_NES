using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (groundCheck == null)
            groundCheck = transform;

        if (groundLayer == 0)
            groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Set animator parameters based on movement
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("IsGrounded", isGrounded);

        // Flip the player sprite based on the direction
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        // Attack
        if (Input.GetButtonDown("Fire1"))
        {
            // Trigger the attack animation or perform attack logic
            anim.SetTrigger("Attack");
        }
    }
}
