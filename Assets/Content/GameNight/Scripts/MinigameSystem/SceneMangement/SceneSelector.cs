using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSelector
{
    private static int currentScene = 0;
    private static bool isRepeat = true;


    /// <summary>
    /// Preload the minigames into the scene selector
    /// </summary>
    public static void LoadMinigames()
    {
        
    }
    public static void RandomSceneSelector()
    {

        int newScene = Random.Range(1, 4);
        isRepeat = newScene == currentScene;
        while (isRepeat)
        {
            newScene = Random.Range(1, 4);
            isRepeat = newScene == currentScene;
        }
        currentScene = newScene;
        SceneManager.LoadScene(newScene);
    }

    public static void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
