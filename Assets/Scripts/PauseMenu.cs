using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPauseGame = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameplayMenu;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPauseGame)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        gameplayMenu.SetActive(true);
        Time.timeScale = 1f;
        isPauseGame = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        gameplayMenu.SetActive(false);
        Time.timeScale = 0f;
        isPauseGame = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
