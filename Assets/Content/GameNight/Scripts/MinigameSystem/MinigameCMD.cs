using System;
using UnityEngine;

/// <summary>
/// Static class for basic commands related to minigames. Most are used during minigames to notify when segments are completed and are easy hooks
/// Anything can call these commands if it needs to, but generally keep it to the most important elements like the enemy or similar.
/// Only invoke the events through the method calls.
/// </summary>
public static class MinigameCMD
{
    public static event Action OnStartMinigameIntro;
    public static event Action AfterMinigameIntro;
    /// <summary>
    /// Event hook before a point is awarded
    /// </summary>
    public static event Action BeforeMinigamePointScored;
    /// <summary>
    /// Event hook for after a point is awarded
    /// </summary>
    public static event Action AfterMinigamePointScored;
    /// <summary>
    /// Event hook for after a minigame is completed
    /// </summary>
    public static event Action OnMinigameTaskComplete;
    /// <summary>
    /// Event hook for after a minigame is failed
    /// </summary>
    public static event Action OnMinigameTaskFailed;
    public static void TriggerMinigameIntro() => OnStartMinigameIntro?.Invoke();
    public static void TriggerAfterMinigameIntro() => AfterMinigameIntro?.Invoke();
    public static void TriggerBeforeScore() => BeforeMinigamePointScored?.Invoke();
    public static void TriggerAfterScore() => AfterMinigamePointScored?.Invoke();
    public static void CompleteMinigame() => OnMinigameTaskComplete?.Invoke();
    public static void FailMinigame() => OnMinigameTaskComplete?.Invoke();

}
