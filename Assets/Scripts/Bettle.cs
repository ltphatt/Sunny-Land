using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bettle : MonoBehaviour
{
    [SerializeField] GameObject waypointTop;
    [SerializeField] GameObject waypointBottom;
    [SerializeField] float flySpeed = 10f;
    [SerializeField] float fixedlyTime = 1f;

    Rigidbody2D rb;
    float topCap = 0f;
    float botCap = 0f;
    bool isFacingTop = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetRangeMove();
        StartCoroutine(Move());
    }

    void GetRangeMove()
    {
        topCap = waypointTop.transform.position.y;
        botCap = waypointBottom.transform.position.y;
    }

    IEnumerator Move()
    {
        float currentHeight = transform.position.y;
        if (isFacingTop)
        {
            if (currentHeight < topCap)
            {
                rb.velocity = new Vector2(rb.velocity.x, flySpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                yield return new WaitForSeconds(fixedlyTime);
                isFacingTop = false;
            }
        }
        else
        {
            if (currentHeight > botCap)
            {
                rb.velocity = new Vector2(rb.velocity.x, -flySpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                yield return new WaitForSeconds(fixedlyTime);
                isFacingTop = true;
            }
        }
    }
}
