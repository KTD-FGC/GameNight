using UnityEngine;

[CreateAssetMenu(fileName = "NewMinigameObject", menuName = "Scriptable Objects/Create MinigameObject")]
public class MinigameObject : ScriptableObject
{
    //Stores all details needed for the minigame to run when it is loaded, outside of the code in the scene itself

    //Default timer duration for minigame
    //Countdown timer to start the minigame
    public SceneObject minigameScene;   // The scene to load for the minigame and pass the information into

    public float timerDuration = 10;     //Default value of 10 seconds
    public float timerCountdown = 3;    // How many seconds to count down before starting the timer
    public GameObject playerPrefab;     //Empty space to handle storing the player prefrab to load in for the minigame
}
