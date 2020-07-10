﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    private void Awake()
    {
        SetupSingleton();

    }

    private void SetupSingleton()
    {
        int numberGamesession = FindObjectsOfType<GameSession>().Length;
        if (numberGamesession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;

    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    
}
