using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    ScoreKeeper scoreKeeper;

    [Header("Health")]
    [SerializeField] Slider healthBar;
    [SerializeField] Health playerHealth;
    [SerializeField] Image image;

    // [Header("Score")]
    // [SerializeField] TextMeshProUGUI scoreText;

    [Header("Cherry")]
    [SerializeField] TextMeshProUGUI cherryText;

    [Header("Gem")]
    [SerializeField] TextMeshProUGUI gemText;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        healthBar.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        healthBar.value = playerHealth.GetHealth();
        ModifyColorHP();

        // scoreText.text = scoreKeeper.GetScore().ToString();
        cherryText.text = scoreKeeper.GetCherry().ToString();
        gemText.text = scoreKeeper.GetGem().ToString();
    }

    void ModifyColorHP()
    {
        int HP = playerHealth.GetHealth();
        if (HP <= 5 && HP > 3)
        {
            image.GetComponent<Image>().color = new Color(0f, 1f, 0f);
        }
        else if (HP <= 3 && HP > 1)
        {
            image.GetComponent<Image>().color = new Color(1f, 1f, 0f);
        }
        else if (HP <= 1)
        {
            image.GetComponent<Image>().color = new Color(1f, 0f, 0f);
        }
    }
}
