using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    bool wasColected = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isDestroyed", wasColected);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !wasColected)
        {
            wasColected = true;
            StartCoroutine(Feedback());
        }
    }

    IEnumerator Feedback()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}