using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    bool wasColected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasColected)
        {
            wasColected = true;
            Destroy(gameObject);
        }
    }
}
