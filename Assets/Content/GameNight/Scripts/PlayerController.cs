using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{

    private enum enum_StickDir { UpLeft, Up, UpRight, Right, DownRight, Down, DownLeft, Left, Neutral}
    private enum enum_ButtonName { btn1, btn2, btn3, btn4, btn5, btn6, none}


    private struct InputData
    {
        private enum_StickDir stickDirection;
        private enum_ButtonName[] pressedButtons;
        private enum_ButtonName[] heldButtons;
        private enum_ButtonName[] releasedButtons;

        public InputData(enum_StickDir stickDir, enum_ButtonName[] pressedBtns, enum_ButtonName[] heldBtns, enum_ButtonName[] releasedBtns)
        {
            this.stickDirection = stickDir;
            this.pressedButtons = pressedBtns;
            this.heldButtons = heldBtns;
            this.releasedButtons = releasedBtns;
        }

        public void Append(enum_ButtonName btn, int type)//0 for pressed, 1 for held, 2 for released
        {
            if (type == 0)
            {
                enum_ButtonName[] tempArray = new enum_ButtonName[this.pressedButtons.Length + 1];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (i != tempArray.Length - 1)
                    {
                        tempArray[i] = this.pressedButtons[i];
                    }
                    if (i == tempArray.Length - 1)
                    {
                        tempArray[i] = btn;
                    }
                }
                pressedButtons = new enum_ButtonName[tempArray.Length];
                pressedButtons = tempArray;
            }

            if (type == 1)
            {
                enum_ButtonName[] tempArray = new enum_ButtonName[this.heldButtons.Length + 1];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (i != tempArray.Length - 1)
                    {
                        tempArray[i] = this.heldButtons[i];
                    }
                    if (i == tempArray.Length - 1)
                    {
                        tempArray[i] = btn;
                    }
                }
                heldButtons = new enum_ButtonName[tempArray.Length];
                heldButtons = tempArray;
            }
            if (type == 2)
            {
                enum_ButtonName[] tempArray = new enum_ButtonName[this.releasedButtons.Length + 1];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (i != tempArray.Length - 1)
                    {
                        tempArray[i] = this.releasedButtons[i];
                    }
                    if (i == tempArray.Length - 1)
                    {
                        tempArray[i] = btn;
                    }
                }
                releasedButtons = new enum_ButtonName[tempArray.Length];
                releasedButtons = tempArray;
            }
        }
    }

    private int bufferFrames = 20; //we can have this set on a per minigame basis if needed, but should be large enough to reasonably read the inputs for say a super if needed
    private InputData[] bufferData;

    private Gamepad gamepad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        Vector2 stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        enum_StickDir inputedDir = DefineStickDir(stickInput);
        enum_ButtonName[] pressedButtons = new enum_ButtonName[6]; //for now assuming no more than 6 buttons for moves
        enum_ButtonName[] heldButtons = new enum_ButtonName[6];
        enum_ButtonName[] releasedButtons = new enum_ButtonName[6];
        InputData thisFramesData = new InputData(inputedDir, pressedButtons, heldButtons, releasedButtons);

        foreach (var control in gamepad.allControls)
        {
            if (control is ButtonControl button)
            {
                if (button.wasPressedThisFrame)
                {
                    thisFramesData.Append(DefineButtonName(button), 0); //append doesn't exist in unity (and arrays are static size :-^) so fix this
                }
                if (button.isPressed && !button.wasPressedThisFrame)
                {
                    thisFramesData.Append(DefineButtonName(button), 1);
                }
                if (button.wasReleasedThisFrame)
                {
                    thisFramesData.Append(DefineButtonName(button), 2);
                }
            }
        }
        AddInputDataToBuffer(thisFramesData); //everything done this frame gets added to a snapshot of that frame in the buffer data

    }

    enum_ButtonName DefineButtonName(ButtonControl button)
    {
        if (button.name == "btn1")
        {
            return enum_ButtonName.btn1;
        }
        
        if (button.name == "btn2")
        {
            return enum_ButtonName.btn2;
        }

        if (button.name == "btn3")
        {
            return enum_ButtonName.btn3;
        }

        if (button.name == "btn4")
        {
            return enum_ButtonName.btn4;
        }

        if (button.name == "btn5")
        {
            return enum_ButtonName.btn5;
        }

        if (button.name == "btn6")
        {
            return enum_ButtonName.btn6;
        }
        return enum_ButtonName.none;//this shouldn't happen
    }

    enum_StickDir DefineStickDir(Vector2 stickInput) //GetAxisRaw only ever returns -1, 0, or 1.
    {
        if (stickInput.x == -1 && stickInput.y == -1)
        {
            return enum_StickDir.DownLeft;
        }
        if (stickInput.x == 0 && stickInput.y == -1)
        {
            return enum_StickDir.Down;
        }
        if (stickInput.x == 1 && stickInput.y == -1)
        {
            return enum_StickDir.DownRight;
        }
        if (stickInput.x == 1 && stickInput.y == 0)
        {
            return enum_StickDir.Right;
        }
        if (stickInput.x == 1 && stickInput.y == 1)
        {
            return enum_StickDir.UpRight;
        }
        if (stickInput.x == 0 && stickInput.y == 1)
        {
            return enum_StickDir.Up;
        }
        if (stickInput.x == -1 && stickInput.y == 1)
        {
            return enum_StickDir.UpLeft;
        }
        if (stickInput.x == 0 && stickInput.y == 0)
        {
            return enum_StickDir.Neutral;
        }
        return enum_StickDir.Neutral;
    }

    void AddInputDataToBuffer(InputData inputs)
    {
        for (int i = 0; i < bufferFrames; i++)
        {
            if (i != bufferFrames-1)//0 indexed
            {
                bufferData[i] = bufferData[i + 1];
            }
            if (i == bufferFrames-1)
            {
                bufferData[i] = inputs;
            }
        }
    }
}
