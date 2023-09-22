using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] public float jumpSpeed = 1f;
    Animator animator;
    Vector2 moveInput;
    public Rigidbody2D rb;
    BoxCollider2D feetCollider2D;
    CapsuleCollider2D bodyCollider2D;
    bool isJumping;
    bool isAlive = true;
    public bool isFalling = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        OnLand();
        isFalling = isJumping && rb.velocity.y < 0.1f;
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        // Change speed of player
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // Running state
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    // Flip sprite player when press different key to move
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        bool isStanding = feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isStanding && value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    // Check player is landing
    private void OnLand()
    {
        if (rb.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }

    // Crouch state
    void OnCrouch(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetBool("isCrouching", true);
            moveSpeed = 4f;
            bodyCollider2D.size = new Vector2(0.6f, 0.8f);
            bodyCollider2D.offset = new Vector2(-0.025f, -0.5f);
        }
    }

    // Standing state - Opposite crouch state
    void OnStand(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetBool("isCrouching", false);
            bodyCollider2D.size = new Vector2(0.6f, 1.25f);
            bodyCollider2D.offset = new Vector2(-0.025f, -0.3f);
            moveSpeed = 8f;
        }
    }
}