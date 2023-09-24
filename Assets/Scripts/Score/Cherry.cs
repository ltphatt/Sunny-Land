using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    bool wasColected = false;
    Animator animator;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

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
            scoreKeeper.ModifyScore(scoreValue);

            scoreKeeper.ModifyCherry();
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
