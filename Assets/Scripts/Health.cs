using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private int health = 5;
    [SerializeField] private int score = 10;
    [SerializeField] private float hurtForce = 2.5f;
    ScoreKeeper scoreKeeper;
    Player player;
    Animator anim;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("isDead");
            scoreKeeper.ModifyScore(score);
            StartCoroutine(EnemyDead());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EnemyDead()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // Attacking enemy via jumping
    void OnCollisionEnter2D(Collision2D other)
    {
        // If this game object is enemy
        if (!isPlayer)
        {
            InteractWithPlayer(other);
        }
        // If this game object is player
        else
        {
            InteractWithEnemy(other);
        }
    }

    void InteractWithPlayer(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.isFalling)
            {
                TakeDamage();
            }
        }
    }

    void InteractWithEnemy(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (player.isFalling)
            {
                player.rb.velocity += new Vector2(0f, player.jumpSpeed);
            }
            else
            {
                float posX = player.transform.position.x;
                float posY = player.transform.position.y;

                // Player is on the left of the enemy
                if (posX < other.gameObject.transform.position.x)
                {
                    player.transform.position = new Vector2(posX - hurtForce, posY);
                }
                // Player is on the right of the enemy
                else if (posX >= other.gameObject.transform.position.x)
                {
                    player.transform.position = new Vector2(posX + hurtForce, posY);
                }

                // Player is decreased HP
                TakeDamage();
            }
        }
    }
}
