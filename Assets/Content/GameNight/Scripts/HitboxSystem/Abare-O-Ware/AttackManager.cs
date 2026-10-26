using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackManager : InputHandler
{

    public Action<MoveTypes, int> ExecuteMove;
    MoveTypes _currentMove = MoveTypes.None;
    private int _specialMoveStrength;
    [SerializeField] SpecialMove[] _specialMoves;
    public static int moveStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackReader();
    }

    private void AttackReader()
    {
        if (lPunchPressed || mPunchPressed || hPunchPressed)
        {
            Punch();
        }

        if (lKickPressed ||  mKickPressed || hKickPressed)
        {
            Kick();
        }
        

    }

    //This is mostly the same for Kick so I'll only comment Punch
    //
    //After being called, it'll determine the strength of the button used, which can be passed to affect the properties of the move called.
    //It will then loop through the special moves and see if there's any that start with the same direction.
    //Once it finds a match, it'll compare the rest of the input string to allow for the execution of the move.
    //
    //There is extra code in Dragon Punch (623) to allow for a smoother input experience.
    //
    //One a button is pressed, the input list will always clear.
    private void Punch()
    {
        _currentMove = MoveTypes.Punch;
        if (lPunchPressed)
        {
            moveStrength = 0;
            Debug.Log("jab");

        }
        else if (mPunchPressed)
        {
            moveStrength = 1;
            Debug.Log("Strong");
        }
        else if (hPunchPressed)
        {
            moveStrength = 2;
            Debug.Log("Fierce");
        }

        if (_motionReader.inputs.Count > 0 && _motionReader.inputs != null)
        {
            for (int i = 0; i <= _specialMoves.Length; i++)
            {
                if (_motionReader.inputs.Count > 2)
                {
                    if (_motionReader.inputs[0] == 2)
                    {
                        if (_motionReader.inputs[1] == _specialMoves[0].input[1] &&
                        _motionReader.inputs[2] == _specialMoves[0].input[2])
                        {
                            Debug.Log("Hadoken");
                            _currentMove = MoveTypes.Special1;
                            break;
                        }
                    }
                    else if (_motionReader.inputs[0] == 6)
                    {
                        if (_motionReader.inputs.Count >= 4)
                        {
                            if (_motionReader.inputs[1] == _specialMoves[i].input[1] || _motionReader.inputs[2] == _specialMoves[i].input[1] &&
                            _motionReader.inputs[2] == _specialMoves[i].input[2] || _motionReader.inputs[3] == _specialMoves[i].input[2])
                            {
                                Debug.Log("Shoryuken");
                                _currentMove = MoveTypes.Special3;
                                break;
                            }
                        }
                        else
                        {
                            if (_motionReader.inputs[1] == _specialMoves[i].input[1] &&
                            _motionReader.inputs[2] == _specialMoves[i].input[2])
                            {
                                Debug.Log("Shoryuken");
                                _currentMove = MoveTypes.Special3;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        _motionReader.inputs.Add(5);
        ExecuteMove?.Invoke(_currentMove, moveStrength);
        _motionReader.inputs.Clear();
    }

    private void Kick()
    {
        _currentMove = MoveTypes.Kick;
        if (lKickPressed)
        {
            moveStrength = 0;
            Debug.Log("Short");
        }
        else if (mKickPressed)
        {
            moveStrength = 1;
            Debug.Log("Forward");
        }
        else if (hKickPressed)
        {
            moveStrength = 2;
            Debug.Log("Roundhouse");
        }
        if (_motionReader.inputs.Count > 0 && _motionReader.inputs != null)
        {
            for (int i = 0; i <= _specialMoves.Length; i++)
            {
                if (_motionReader.inputs.Count > 2)
                {
                    if (_motionReader.inputs[0] == 2)
                    {
                        if (_motionReader.inputs[1] == _specialMoves[1].input[1] &&
                        _motionReader.inputs[2] == _specialMoves[1].input[2])
                        {
                            Debug.Log("Tatsumaki");
                            _currentMove = MoveTypes.Special2;
                            break;
                        }
                    }
                }
                break;
            }
        }
        _motionReader.inputs.Add(5);
        ExecuteMove?.Invoke(_currentMove, moveStrength);
        _motionReader.inputs.Clear();
    }
}
