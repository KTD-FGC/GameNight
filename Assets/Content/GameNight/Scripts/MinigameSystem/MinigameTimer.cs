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


    public static Action TimerExpired;
    public static Action TimerStarted;
    public static Action<float> TimerUpdated;
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

    void Awake()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalInstance.IsPaused && isRunning )
        {
            currentTime -= Time.deltaTime;


            if (currentTime <= 0)
            {
                //When the timer expires, invoke the event located on the minigame manager
                TimerExpired?.Invoke();
                isRunning = false;
            }
            TimerUpdated?.Invoke(currentTime);
        }


    }

    public void StartTimer()
    {
        StartCoroutine(BeginGame());
    }
    
    IEnumerator BeginGame()
    {        
        currentTime = _timerDuration;
        yield return new WaitForSeconds(_timerDelay);
        TimerStarted?.Invoke();
        isRunning = true;

    }
}
