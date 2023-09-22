using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private int health = 5;
    [SerializeField] private int score = 10;
    ScoreKeeper scoreKeeper;
    Player player;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
    }

    public int GetHealth()
    {
        return health;
    }

    void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Attacking enemy via jumping
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isPlayer)
        {
            if ((other.gameObject.tag != "Ground") || (other.gameObject.tag == "Player" && player.isFalling == true))
            {
                TakeDamage();
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (player.isFalling)
                {
                    player.rb.velocity += new Vector2(0f, player.jumpSpeed);
                }
                else
                {
                    TakeDamage();
                }
            }
        }
    }
}
