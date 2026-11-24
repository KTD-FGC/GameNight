using System;
using UnityEngine;
using UnityEngine.Events;
public class Minigame : MonoBehaviour
{

    [Tooltip("Event for any direct assignments within the scene for triggering start minigame")]
    public UnityEvent OnStartMinigame;
    public UnityEvent OnAfterIntro;
    public UnityEvent OnPauseActivate;
    public UnityEvent OnPauseDeactivate;
    public UnityEvent OnBeforeScore;

    public bool IsPaused    {get; private set;}

    public void OnEnable()
    {
        MinigameManager.ActiveManager.StartMinigame += StartMinigame;
        MinigameCMD.AfterMinigameIntro += AfterMinigameIntro;
        MinigameCMD.OnPauseMinigame += PauseMinigame;
    }
    public void OnDisable()
    {
        MinigameManager.ActiveManager.StartMinigame -= StartMinigame;
        MinigameCMD.AfterMinigameIntro -= AfterMinigameIntro;
        MinigameCMD.OnPauseMinigame -= PauseMinigame;
    }
    /// <summary>
    /// Override with any feature or visual that needs to be trigger after minigame introduction
    /// </summary>
    public virtual void AfterMinigameIntro()
    {
        OnAfterIntro?.Invoke();

    }



    public virtual void StartMinigame()
    {
        OnStartMinigame?.Invoke();
    }
    private void PauseMinigame(bool shouldPause)
    {
        //If we are already paused and told to unpause
        if(IsPaused && shouldPause == false)
        {
            OnPauseDeactivate?.Invoke();
        }
        else if(!IsPaused && shouldPause == true) //If not paused and told to pause
        {
            OnPauseActivate?.Invoke();
        }
        //If we are already paused and get the call to pause, we ignore it
        //Same for if we are unpaused and get the call to pause
        //Always set paused to shouldPause
        IsPaused = shouldPause;
    }
    public virtual void BeforeScore()
    {
        OnBeforeScore?.Invoke();
        //Slow down the game
        //Run anything that needs to wait lol
    }
}
