using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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

    // Decrease HP
    void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    // Die when HP reach 0
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
            player.isAlive = false;
            Destroy(gameObject);
        }
    }

    // Wait for a few second before enemy is dead
    IEnumerator EnemyDead()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // Collision with other object
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

    // Enemy interact to player
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

    // Player interact to enemy
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

                // SetAnimation();
                StartCoroutine(SetAnimation());

                // Player is decreased HP
                TakeDamage();
            }
        }
    }

    IEnumerator SetAnimation()
    {
        anim.SetBool("isHurting", true);

        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isHurting", false);
    }
}
