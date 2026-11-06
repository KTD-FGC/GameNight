using System;
using UnityEngine;
using UnityEngine.Events;
public class Minigame : MonoBehaviour
{

    [Tooltip("Event for any direct assignments within the scene for triggering start minigame")]
    public UnityAction OnStartMinigame;

    void OnEnable()
    {
        MinigameManager.ActiveManager.StartMinigame += StartMinigame;
    }

    void OnDisable()
    {
        MinigameManager.ActiveManager.StartMinigame -= StartMinigame;
    }

    public virtual void StartMinigame()
    {
        OnStartMinigame?.Invoke();
    }
}
