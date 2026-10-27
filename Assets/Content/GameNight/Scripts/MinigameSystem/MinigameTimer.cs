using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MinigameTimer : MonoBehaviour
{
    [Tooltip("How long they have in the minigame")]

    [SerializeField] float _timerDuration;

    [Tooltip("How long to wait before starting the timer")]
    ///this is for the Unity event for triggering anything that needs to start before the game begins (for specifically Zangief)///
    [SerializeField] float _timerDelay = 2;


    public UnityEvent OnBeginMinigame;
    float currentTime;
    bool isRunning = false;
    private void OnEnable()
    {
        GlobalInstance.RoundFinish += ChangeTimer;
    }
    private void OnDisable()
    {
        GlobalInstance.RoundFinish -= ChangeTimer;
    }

    private void ChangeTimer(bool didWin)
    {
        if (didWin)
        {
            isRunning = false;

        }
        SceneSelector.RandomSceneSelector();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentTime = _timerDuration;
        StartCoroutine(BeginGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalInstance.IsPaused && isRunning )
        {
            currentTime -= Time.deltaTime;


            if (currentTime <= 0)
            {
                GlobalInstance.RemoveLives();
                isRunning = false;
            }
        }


    }
    
    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(_timerDelay);
        OnBeginMinigame?.Invoke();
        isRunning = true;

    }
}
