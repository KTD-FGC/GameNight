using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MotionReader : InputHandler
{

   
    [SerializeField] float _maxInputTimer;
    private float _inputTimer;
    public List<int> inputs;

    public bool inputingRight { get; private set; } = false;
    public bool inputingLeft { get; private set; } = false;
    public bool inputingUp { get; private set; } = false;
    public bool inputingDown { get; private set; } = false;

    //This is to make sure the player is not holding a diagonal input to allow for smooth rolling.
    private bool _isCardinal;

    public bool isFacingRight;

    //This script will read the player's inputs and store them in the 'inputs' list, which can be accessed by other scripts.
    //Inputs are stored in numpad notation:
    //
    //     7     8     9
    //
    //     4     5     6
    //
    //     1     2     3
    //
    //These can be compared against arrays of numbers, which can be used to activate special moves.
    //For said special moves, an incorrect input should clear the list.
    public void ReadInputs()
    {
        if (_moveAction.ReadValue<Vector2>().y >= 0.6 && !inputingUp)
        {
            inputingRight = false;
            inputingLeft = false;
            inputingUp = true;
            inputingDown = false;
            _inputTimer = _maxInputTimer;
            if (_moveAction.ReadValue<Vector2>().x >= 0.6 && !inputingRight)
            {
                if (isFacingRight)
                {
                    inputs.Add(9);
                }
                else
                {
                    inputs.Add(7);
                }
                Debug.Log("Up Forward");
                inputingRight = true;
                _isCardinal = false;
            }
            else if (_moveAction.ReadValue<Vector2>().x <= -0.6 && !inputingLeft)
            {
                if (isFacingRight)
                {
                    inputs.Add(7);
                }
                else
                {
                    inputs.Add(9);
                }
                Debug.Log("Up Back");
                inputingLeft = true;
                _isCardinal = false;
            }
            else
            {
                inputs.Add(8);
                Debug.Log("Up");
                _isCardinal = true;
            }
        }
        else if (_moveAction.ReadValue<Vector2>().y <= -0.6 && !inputingDown)
        {
            inputingRight = false;
            inputingLeft = false;
            inputingUp = false;
            inputingDown = true;
            _inputTimer = _maxInputTimer;
            if (_moveAction.ReadValue<Vector2>().x >= 0.6 && !inputingRight)
            {
                if (isFacingRight)
                {
                    inputs.Add(3);
                }
                else
                {
                    inputs.Add(1);
                }
                Debug.Log("Down Forward");
                inputingRight = true;
                _isCardinal = false;
            }
            else if (_moveAction.ReadValue<Vector2>().x <= -0.6 && !inputingLeft)
            {
                if (isFacingRight)
                {
                    inputs.Add(1);
                }
                else
                {
                    inputs.Add(3);
                }
                Debug.Log("Down Back");
                inputingLeft = true;
                _isCardinal = false;
            }
            else
            {
                inputs.Add(2);
                Debug.Log("Down");
                _isCardinal = true;
            }
        }

        if (_moveAction.ReadValue<Vector2>().x >= 0.6 && !inputingRight)
        {
            inputingRight = true;
            inputingLeft = false;
            inputingUp = false;
            inputingDown = false;
            _inputTimer = _maxInputTimer;
            if (_moveAction.ReadValue<Vector2>().y >= 0.6 && !inputingUp)
            {
                if (isFacingRight)
                {
                    inputs.Add(9);
                }
                else
                {
                    inputs.Add(7);
                }
                Debug.Log("Up Forward");
                inputingUp = true;
                _isCardinal = false;
            }
            else if (_moveAction.ReadValue<Vector2>().y <= -0.6 && !inputingDown)
            {
                if (isFacingRight)
                {
                    inputs.Add(3);
                }
                else
                {
                    inputs.Add(1);
                }
                Debug.Log("Down Forward");
                inputingDown = true;
                _isCardinal = false;
            }
            else
            {
                if (isFacingRight)
                {
                    inputs.Add(6);
                }
                else
                {
                    inputs.Add(4);
                }
                Debug.Log("Forward");
                _isCardinal = true;
            }
        }
        else if (_moveAction.ReadValue<Vector2>().x <= -0.6 && !inputingLeft)
        {
            inputingRight = false;
            inputingLeft = true;
            inputingUp = false;
            inputingDown = false;
            _inputTimer = _maxInputTimer;
            if (_moveAction.ReadValue<Vector2>().y >= 0.6 && !inputingUp)
            {
                if (isFacingRight)
                {
                    inputs.Add(7);
                }
                else
                {
                    inputs.Add(9);
                }
                Debug.Log("Up Back");
                inputingUp = true;
                _isCardinal = false;
            }
            else if (_moveAction.ReadValue<Vector2>().y <= -0.6 && !inputingDown)
            {
                if (isFacingRight)
                {
                    inputs.Add(1);
                }
                else
                {
                    inputs.Add(3);
                }
                Debug.Log("Down Back");
                inputingDown = true;
                _isCardinal = false;
            }
            else
            {
                if (isFacingRight)
                {
                    inputs.Add(4);
                }
                else
                {
                    inputs.Add(6);
                }
                Debug.Log("Back");
                _isCardinal = true;
            }
        }
    }

    public void ReleaseButton()
    {
        if (_moveAction.ReadValue<Vector2>().x >= -0.5 && _moveAction.ReadValue<Vector2>().x <= 0.5 && !_isCardinal)
        {
            if (_moveAction.ReadValue<Vector2>().y >= 0.5)
            {
                inputingUp = false;
                _isCardinal = true;
            }
            else if (_moveAction.ReadValue<Vector2>().y <= -0.5)
            {
                inputingDown = false;
                _isCardinal = true;
            }
            inputingRight = false;
            inputingLeft = false;
        }
        if (_moveAction.ReadValue<Vector2>().y >= -0.5 && _moveAction.ReadValue<Vector2>().y <= 0.5 && !_isCardinal)
        {
            if (_moveAction.ReadValue<Vector2>().x >= 0.5)
            {
                inputingRight = false;
                _isCardinal = true;
            }
            else if (_moveAction.ReadValue<Vector2>().x <= -0.5)
            {
                inputingLeft = false;
                _isCardinal = true;
            }
            inputingDown = false;
            inputingUp = false;
        }
        if (_moveAction.ReadValue<Vector2>().x == 0 && _moveAction.ReadValue<Vector2>().y == 0)
        {
            inputingRight = false;
            inputingLeft = false;
            inputingUp = false;
            inputingDown = false;
            _isCardinal = false;
        }
    }

    //This is more or less the buffer for what counts for a string of directional inputs. For instance, a player cannot do an input
    //too slowly or else they will just get nothing. This can be adjusted with the '_maxInputTimer' variable.
    public void InputTimer()
    {
        _inputTimer -= Time.deltaTime;
        if (_inputTimer < 0)
        {
            inputs.Clear();
            inputingRight = false;
            inputingLeft = false;
            inputingUp = false;
            inputingDown = false;
        }
    }
}
