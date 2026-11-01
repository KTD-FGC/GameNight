using System;
using System.Collections;
using UnityEngine;

public static class GlobalInstance
{
    public static int score;
    public static int lives;
    private static int _maxLives = 5;
    public static bool IsPaused { get; private set; } = false;

    public static Action<bool> RoundFinish;

    public static void StartGame()
    {
        score = 0;
        lives = _maxLives;
    }

    public static void WinGame()
    {
        SceneSelector.ReturnToTitle();

    }
    
    public static void EndGame()
    {

        SceneSelector.ReturnToTitle();
    }

    public static void AddScore()
    {
        score++;
        Debug.Log($"Score = {score}");
        RoundFinish?.Invoke(true);
        if ( score >= 20 )
        {
            WinGame();
        }
    }

    public static void RemoveLives()
    {
        if (lives <= 0)
        {
            RoundFinish?.Invoke(false);
            lives--;
        }
        else
        {
            EndGame();
        }
    }

    public static void Pause()
    {
        if (!IsPaused)
        {
            IsPaused = true;
            Debug.Log("Timer is paused");
        }
    }

    public static void Resume()
    {
        if (IsPaused)
        {
            IsPaused = false;
            Debug.Log("Timer is unpaused");
        }
    }
}
