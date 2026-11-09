using UnityEngine;

public class MainMenu : MonoBehaviour
{


    public void StartGame()
    {
        GlobalInstance.StartGame();
        MinigameManager.ActiveManager.StartMinigameShuffle();
    }
}
