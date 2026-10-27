using UnityEngine;

[CreateAssetMenu(fileName = "NewModeData", menuName = "Scriptable Objects/MoveData")]
public class MoveData : ScriptableObject
{
    public string moveName;
    public int moveIndex;

    public AnimationClip myClip; //hitboxes will be on the move itself

    public int healthChange;

    public float myXMovement;
    public float myYMovement;
}
