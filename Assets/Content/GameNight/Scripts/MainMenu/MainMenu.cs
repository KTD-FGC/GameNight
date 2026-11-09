using UnityEngine;

public class MainMenu : MonoBehaviour
{


    public void StartGame()
    {
        MinigameManager.ActiveManager.currentSceneIndex = 0;
        GlobalInstance.StartGame();
        MinigameManager.ActiveManager.StartMinigameShuffle();
    }
}
