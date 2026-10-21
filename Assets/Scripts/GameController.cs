using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    public GameObject gameOverScreen;

    public static event Action OnReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        SoulFragment.OnSoulCollect += IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;

        PlayerHealth.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
        LoadCanvas.SetActive(false);
    }

    void IncreaseProgressAmount(int amount)
    {
        Debug.Log("GameController received gem worth: " + amount);

        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100)
        {
            //Level Complete!
            LoadCanvas.SetActive(true);
            Debug.Log("Level Complete!");
        }
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1;
        LoadCanvas.SetActive(false);

        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[nextLevelIndex].gameObject.SetActive(true);

        player.transform.position = new Vector3(0, 0, 0);

        currentLevelIndex = nextLevelIndex;
        progressAmount = 0;
        progressSlider.value = 0;
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnReset.Invoke();
        Time.timeScale = 1;
    }
}
