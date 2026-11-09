using UnityEngine;

public class MainMenu : MonoBehaviour
{


    public void StartGame()
    {
        MinigameManager.ActiveManager.StartMinigameShuffle();
    }
}
