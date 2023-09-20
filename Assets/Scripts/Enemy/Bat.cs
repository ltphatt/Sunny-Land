using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] GameObject waypointLeft;
    [SerializeField] GameObject waypointRight;
    [SerializeField] GameObject waypointMiddle;
    [SerializeField] float flySpeed = 5f;
    [SerializeField] float downSpeed = 1f;
    [SerializeField] float hangTime = 5f;
    Rigidbody2D rb;
    Animator anim;
    float leftCap = 0f;
    float rightCap = 0f;
    float midCap = 0f;
    bool isFacingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        midCap = waypointMiddle.transform.position.x;
    }

    IEnumerator Move()
    {
        float currentPos = transform.position.x;
        if (isFacingLeft)
        {
            if (currentPos > midCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                anim.SetBool("isHanging", false);
                rb.velocity = new Vector2(-flySpeed, -downSpeed) * Time.deltaTime;
            }
            else if (currentPos > leftCap && currentPos < midCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                anim.SetBool("isHanging", false);
                rb.velocity = new Vector2(-flySpeed, downSpeed) * Time.deltaTime;
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                anim.SetBool("isHanging", true);
                yield return new WaitForSeconds(hangTime);
                isFacingLeft = false;
            }
        }
        else
        {
            if (currentPos < midCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                anim.SetBool("isHanging", false);
                rb.velocity = new Vector2(flySpeed, -downSpeed) * Time.deltaTime;
            }
            else if (currentPos < rightCap && currentPos > midCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                anim.SetBool("isHanging", false);
                rb.velocity = new Vector2(flySpeed, downSpeed) * Time.deltaTime;
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                anim.SetBool("isHanging", true);
                yield return new WaitForSeconds(hangTime);
                isFacingLeft = true;
            }
        }
    }
}
