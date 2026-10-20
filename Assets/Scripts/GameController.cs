using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject gameOverScreen;

    public static event Action OnReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        SoulFragment.OnSoulCollect += IncreaseProgressAmount;

        PlayerHealth.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100)
        {
            //Level Complete!
            Debug.Log("Level Complete!");
        }
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
