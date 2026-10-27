using UnityEngine;
using System.Collections.Generic;

public enum Inputs { UpLeft, Up, UpRight, Right, DownRight, Down, DownLeft, Left, Neutral, btn1, btn2, btn3, btn4, btn5, btn6 } //SF uses P/K, GG uses Slashes, btn is agnostic

[CreateAssetMenu(fileName = "MovelistDictionary", menuName = "Scriptable Objects/MovelistDictionary")]

public class MovelistDictionary : ScriptableObject
{
    

    public List<MoveInfo> Priority1; //eg Ultra moves

    public List<MoveInfo> Priority2; //eg Super moves

    public List<MoveInfo> Priority3; //eg Special moves

    public List<MoveInfo> Priority4; //eg Throws, Parries

    public List<MoveInfo> Priority5; //eg Dashes (Forward Neutral Forward)

    [System.Serializable]
    public struct MoveInfo
    {
        public MoveData moveData; //the move to be executed ie Light Fireball
        public List<Inputs> moveInputs;
    }

}
