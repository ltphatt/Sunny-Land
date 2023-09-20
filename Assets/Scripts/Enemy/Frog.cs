using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] GameObject waypointLeft;
    [SerializeField] GameObject waypointRight;
    [SerializeField] float jumpLength = 10f;
    [SerializeField] float jumpHeight = 15f;
    [SerializeField] LayerMask ground;
    Collider2D coll;
    Rigidbody2D rb;
    bool isFacingLeft = true;
    float leftCap;
    float rightCap;
    Animator anim;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        GetRangeMove();
        TransitionState();
    }

    void GetRangeMove()
    {
        leftCap = waypointLeft.transform.position.x;
        rightCap = waypointRight.transform.position.x;
    }

    void TransitionState()
    {
        // Transition from jump to fall
        if (anim.GetBool("isJumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("isFalling", true);
                anim.SetBool("isJumping", false);
            }
        }
        // Transition from fall to jump
        if (coll.IsTouchingLayers(ground) && anim.GetBool("isFalling"))
        {
            anim.SetBool("isFalling", false);
        }
    }

    // Add to animations event
    void Jump()
    {
        if (isFacingLeft)
        {
            if (transform.position.x > leftCap)
            {
                // Make sure sprite is facing right location, and if it is not, then face the right direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                // Test to see if I am not on the ground, if so jump
                if (coll.IsTouchingLayers(LayerMask.GetMask("Ground")))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("isJumping", true);
                }
            }
            else
            {
                isFacingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("isJumping", true);
                }
            }
            else
            {
                isFacingLeft = true;
            }
        }
    }
}
