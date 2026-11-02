using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MinigameManager : MonoBehaviour
{
    private int currentSceneIndex = 0;
    private bool isRepeat = false; //small variable to check for duplicate scenes, to make sure you can't get the same minigame twice
    //TO:DO - Proper sorter and scrambler to avoid this all together and make it IMPOSSIBLE to get the same minigame in a given sequence length
    [SerializeField]
    private UnityEngine.Object[] minigames;

    [SerializeField]
    private TextMeshProUGUI sceneNameText;


    private MinigameObject activeMinigame;
    private MinigameTimer minigameTimer;

    public static MinigameManager ActiveManager;
    public System.Action StartMinigame;
    public System.Action OnMinigameLose;

    private Animator _animator;

    //Difficulty increases based on completed games compared to difficulty scale
    private float _difficulty = 1f;
    //How hard should the difficulty scale off of completed games?
    private float _difficultyScale;
    private float _completedGames = 0;

    public float Difficulty => _difficulty;
    void OnEnable()
    {
        if(ActiveManager == null)
        {
            ActiveManager = this;
        }
        else
        {
            //Immediately get rid of any duped minigame managers, should only be one
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        minigameTimer = GetComponentInChildren<MinigameTimer>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        MinigameTimer.TimerExpired += OnTimerExpired;
        //minigameTimer.StartTimer();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTimerExpired()
    {
        //Send out the event that the game is lost
        GlobalInstance.OnGameLose?.Invoke();
        //When the timer expires, start the sequence to change the minigame
        StartCoroutine(ChangeMinigameSequence());
    }

    //Placing here for later for tracking things on scene load and changes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneNameText.text = scene.name;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadMinigameSet("MinigameSet_test");
        StartCoroutine(ChangeMinigameSequence());
        //RandomSceneSelector();
    }

    /// <summary>
    /// Loads ALL minigames within the Minigames folder, regardless of specific set
    /// Use ONLY if we want bulk testing for minigames of all kinds
    /// </summary>
    public void LoadAllMinigames()
    {
        minigames = Resources.LoadAll("Minigames", typeof(MinigameObject));
    }

    /// <summary>
    /// Load a specific minigame set by folder name. Folder MUST be preset in order to work.
    /// Will load no minigames if none preset
    /// </summary>
    /// <param name="setName"></param>
    public void LoadMinigameSet(string setName)
    {
        string setPath = "Minigames/"+setName;
        minigames = Resources.LoadAll(setPath, typeof(MinigameObject));
    }

    public void RandomSceneSelector()
    {

        int newScene = UnityEngine.Random.Range(0, minigames.Length);
        //If there are more than 2 minigames in the collection, proceed with duplicate protection
        if(minigames.Length > 2)
        {
            if(newScene == currentSceneIndex)
            {
                Debug.LogWarning("Found a repeat, scene. Looping till no repeat");
                isRepeat = true;
                while (isRepeat)
                {
                    newScene = UnityEngine.Random.Range(0, minigames.Length);
                    isRepeat = newScene == currentSceneIndex;
                }
            }

        }
        else if(minigames.Length == 2) //There are 2 or less games, so just follow the randomness and don't account for duplicates
        {
            //Alternate between the 2 possible scenes
            //Current scene is 1, new scene is 0
            if(currentSceneIndex > 0)
            {
                newScene = 0;
            }
            else
            {
                newScene = 1;
            }
        }

        currentSceneIndex = newScene;
        Debug.Log(newScene);
        OnMinigameSelected(minigames[newScene] as MinigameObject);

    }

    private void OnMinigameSelected(MinigameObject minigame)
    {
        string sceneName = minigame.minigameScene.sceneName;
        sceneNameText.text = sceneName;

        if(minigame.OverrideTimer)
        {
            minigameTimer.UpdateTimerValues(minigame.timerDuration, minigame.timerCountdown);
        }
        SceneManager.LoadScene(sceneName);
    }


    IEnumerator ChangeMinigameSequence()
    {
        //Close the curtains
        _animator.Play("EndRound");
        yield return new WaitForSeconds(3);
        //Show if the point was awarded
        //Either lose a life or increase the number of the round
        //Wait a few seconds 
        //Load next minigame
        RandomSceneSelector();
        //Reveal said minigame
        _animator.Play("StartRound");
        yield return new WaitForSeconds(3);
        StartMinigame?.Invoke();
        //Lock out player input until after the countdown

        minigameTimer.StartTimer();
        yield return null;

    }
}
