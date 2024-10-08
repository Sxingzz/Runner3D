﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text LevelText;

    private void Start()
    {
        progressBar.value = 0;

        int currentLevel = ChunkManager.instance.GetLevels();
        LevelText.text = "Level " + (currentLevel + 1).ToString();
        Debug.Log("Displayed Level: " + (currentLevel + 1));

        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)
        {
            Show_GameOver_Panel();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            Show_LevelComplete_Panel();
        }
    }

    public void Play_Button_Pressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void PlayAgain_Button_Pressed()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel_Button_Pressed()
    {
        ChunkManager.instance.NextLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Show_LevelComplete_Panel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void Show_GameOver_Panel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState()) return;
        float progress = PlayerController.instance.transform.position.z 
                            / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;

    }

    
}
