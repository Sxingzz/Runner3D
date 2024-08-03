using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Menu, Game, LevelComplete, GameOver}

    public static Action<GameState> onGameStateChanged;

    private GameState gameState;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);

        Debug.Log("Game State Chang to: " + gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
