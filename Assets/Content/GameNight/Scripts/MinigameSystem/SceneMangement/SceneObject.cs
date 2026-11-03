using UnityEngine;
using UnityEngine.SceneManagement;


//Code originates from https://www.thebear.dev/managing-scene-references-in-unity-using-scriptable-objects/
[CreateAssetMenu(fileName = "NewSceneObject", menuName = "Scriptable Objects/Create SceneObject")]
public class SceneObject : ScriptableObject
{
    /// <summary>
    /// Implicitly converts a scene object into a Scene reference
    /// </summary>
    /// <param name="self"></param>
    public static implicit operator Scene(SceneObject self)
    {
        return self.Scene;
    }
      // Implicitly converts a scene object to a string containing the scene name.
    public static implicit operator string(SceneObject self)
    { 
        return self.sceneName; 
    }

    public string sceneName;
    public Scene Scene { get=> SceneManager.GetSceneByName(sceneName); }

    #if UNITY_EDITOR

    public UnityEditor.SceneAsset sceneAsset;

    public void OnValidate(){
        sceneName = sceneAsset.name;
    }
    #endif
}
