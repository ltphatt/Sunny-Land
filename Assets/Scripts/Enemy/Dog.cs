using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] GameObject waypointLeft;
    [SerializeField] GameObject waypointRight;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float fixedlyTime = 3f;
    Collider2D coll;
    Rigidbody2D rb;
    float leftCap = 0f;
    float rightCap = 0f;
    bool isFacingLeft = true;
    Animator animator;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetRangeMove();
        StartCoroutine(Move());
    }

    void GetRangeMove()
    {
        leftCap = waypointLeft.transform.position.x;
        rightCap = waypointRight.transform.position.x;
    }

    IEnumerator Move()
    {
        float currentPos = transform.position.x;
        if (isFacingLeft)
        {
            if (currentPos > leftCap)
            {
                // Make sure sprite is facing right location, and if it is not, then face the right direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(-runSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                animator.SetBool("isRunning", false);
                yield return new WaitForSeconds(fixedlyTime);
                isFacingLeft = false;
            }
        }
        else
        {
            if (currentPos < rightCap)
            {
                // Make sure sprite is facing left location, and if it is not, then face the right direction
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(runSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {

                rb.velocity = new Vector2(0f, rb.velocity.y);
                animator.SetBool("isRunning", false);
                yield return new WaitForSeconds(fixedlyTime);
                isFacingLeft = true;
            }
        }
    }
}
