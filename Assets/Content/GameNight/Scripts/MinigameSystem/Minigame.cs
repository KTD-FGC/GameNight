using System;
using UnityEngine;
using UnityEngine.Events;
public class Minigame : MonoBehaviour
{

    [Tooltip("Event for any direct assignments within the scene for triggering start minigame")]
    public UnityAction OnStartMinigame;
    public UnityAction OnAfterIntro;


    public virtual void OnEnable()
    {
        MinigameManager.ActiveManager.StartMinigame += StartMinigame;
        MinigameCMD.AfterMinigameIntro += AfterMinigameIntro;
    }

    /// <summary>
    /// Override with any feature or visual that needs to be trigger after minigame introduction
    /// </summary>
    public virtual void AfterMinigameIntro()
    {
        OnAfterIntro?.Invoke();

    }

    public virtual void OnDisable()
    {
        MinigameManager.ActiveManager.StartMinigame -= StartMinigame;
    }

    public virtual void StartMinigame()
    {
        OnStartMinigame?.Invoke();
    }
}
