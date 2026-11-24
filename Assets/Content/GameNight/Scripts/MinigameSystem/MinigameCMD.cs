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
    public static event Action<bool> OnMinigameTaskComplete;
    /// <summary>
    /// Event hook for after a minigame is failed
    /// </summary>
    public static event Action OnMinigameTaskFailed;

    //Debug/ dev support functions
    /// <summary>
    /// Event hook for pausing a minigame
    /// </summary>
    public static event Action<bool> OnPauseMinigame;
    /// <summary>
    /// Event hook for resetting a minigame
    /// </summary>
    public static event Action OnResetMinigame;
    /// <summary>
    /// Event hook for skipping a minigame
    /// </summary>
    public static event Action OnSkipMinigame;
    public static void TriggerMinigameIntro() => OnStartMinigameIntro?.Invoke();
    public static void TriggerAfterMinigameIntro() => AfterMinigameIntro?.Invoke();
    public static void TriggerBeforeScore() => BeforeMinigamePointScored?.Invoke();
    public static void TriggerAfterScore() => AfterMinigamePointScored?.Invoke();
    public static void CompleteMinigame() => OnMinigameTaskComplete?.Invoke(true);
    public static void FailMinigame() => OnMinigameTaskComplete?.Invoke(false);

    /// <summary>
    /// Pauses the current minigame, passing a bool
    /// If true, minigame will be paused. Will resume or not pause if set to false
    /// </summary>
    /// <param name="shouldPause"></param>
    public static void SetMinigamePaused(bool shouldPause = true)=>OnPauseMinigame?.Invoke(shouldPause);
    public static void ResetMinigame()=>OnResetMinigame?.Invoke();
    public static void SkipMinigame()=> OnSkipMinigame?.Invoke();

}
