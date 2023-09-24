using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int cherry;
    [SerializeField] private int gem;

    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }

    public void ModifyCherry()
    {
        cherry++;
        Mathf.Clamp(cherry, 0, int.MaxValue);
    }

    public void ModifyGem()
    {
        gem++;
        Mathf.Clamp(gem, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCherry()
    {
        return cherry;
    }

    public int GetGem()
    {
        return gem;
    }
}
