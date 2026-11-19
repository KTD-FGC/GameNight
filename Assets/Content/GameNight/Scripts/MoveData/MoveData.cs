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

    public int startupFrames = 1;
    public int activeFrames = 1;
    public int recoveryFrames = 1;
    public int cancelFramesStart;
    public int cancelFramesEnd;
}
