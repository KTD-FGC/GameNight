using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Special Move")]
public class SpecialMove : ScriptableObject
{
    //This creates a scriptable object that contains the motion name and input array. These are currently hard coded in the "Attack Manager"
    //but the system is expandable with relative ease.
    public string motionName;
    public int[] input;
}
