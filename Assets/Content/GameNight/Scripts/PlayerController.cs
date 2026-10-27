using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    /*
     Temp variables for testing
     */
    private Vector2 m_moveAmt;
    public float moveSpeed = 5.0f;
    private CharacterController CharacterController;

    private enum enum_StickDir { UpLeft, Up, UpRight, Right, DownRight, Down, DownLeft, Left, Neutral }
    private enum enum_ButtonName { btn1, btn2, btn3, btn4, btn5, btn6, none }
    public MovelistDictionary moveInputsDictionary; //edit in inspector
    private bool isFacingRight { get; set; } = true; //to mirror inputs


    private struct InputData
    {
        public enum_StickDir stickDirection;
        public enum_ButtonName[] pressedButtons;
        public enum_ButtonName[] heldButtons;
        public enum_ButtonName[] releasedButtons;

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

        public static bool CompareStickDir(enum_StickDir a, Inputs b)
        {
            string tempA = a.ToString();
            string tempB = b.ToString();

            if (tempA == tempB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CompareButtonInput(enum_ButtonName a, Inputs b)
        {
            string tempA = a.ToString();
            string tempB = b.ToString();

            if (tempA == tempB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    private int bufferFrames = 20; //we can have this set on a per minigame basis if needed, but should be large enough to reasonably read the inputs for say a super if needed
    private InputData[] bufferData; //set empty data equal to bufferFrames so it's not putting null into the buffer

    private Gamepad gamepad;

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();

        bufferData = new InputData[bufferFrames];
        for (int i = 0; i < bufferFrames; i++)
        {
            bufferData[i] = new InputData(0, new enum_ButtonName[0], new enum_ButtonName[0], new enum_ButtonName[0]);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamepad = Gamepad.current;
    }
        /*
         test code
         */
    public void OnMove(InputAction.CallbackContext cxt)
    {
        m_moveAmt = cxt.ReadValue<Vector2>();
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        /*
         test code
         */
        Vector3 move = new Vector3(m_moveAmt.x, m_moveAmt.y, 0);
        CharacterController.Move(move * moveSpeed *  Time.deltaTime);
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
        InterpretInput();

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

    void ExecuteMove(MoveData executedMove) //execute the MoveData
    {
        Debug.Log(executedMove.moveName);
        return;
    }

    void InterpretInput() //go from Highest to Lowest Priority inputs
    {
        for (int i = 0; i < moveInputsDictionary.Priority1.Count; i++)
        {
            if (CheckBufferForInput(moveInputsDictionary.Priority1[i])){
                ExecuteMove(moveInputsDictionary.Priority1[i].moveData);
                return;
            }
        }

        for (int i = 0; i < moveInputsDictionary.Priority2.Count; i++)
        {
            if (CheckBufferForInput(moveInputsDictionary.Priority2[i]))
            {
                ExecuteMove(moveInputsDictionary.Priority2[i].moveData);
                return;
            }
        }

        for (int i = 0; i < moveInputsDictionary.Priority3.Count; i++)
        {
            if (CheckBufferForInput(moveInputsDictionary.Priority3[i]))
            {
                ExecuteMove(moveInputsDictionary.Priority3[i].moveData);
                return;
            }
        }

        for (int i = 0; i < moveInputsDictionary.Priority4.Count; i++)
        {
            if (CheckBufferForInput(moveInputsDictionary.Priority4[i]))
            {
                ExecuteMove(moveInputsDictionary.Priority4[i].moveData);
                return;
            }
        }

        for (int i = 0; i < moveInputsDictionary.Priority5.Count; i++)
        {
            if (CheckBufferForInput(moveInputsDictionary.Priority5[i]))
            {
                ExecuteMove(moveInputsDictionary.Priority5[i].moveData);
                return;
            }
        }

    }

    bool CheckBufferForInput(MovelistDictionary.MoveInfo expectedInput) //naive implementation ie: checking full buffer, was the input "successfully" completed? 
    {
        var inputCombo = expectedInput.moveInputs;
        int comboCount = expectedInput.moveInputs.Count - 1; //how many inputs to execute move, hadouken being 236P is 4 Inputs
        for (int i = bufferFrames-1; i >= 0; i--) //this loops through the whole buffer, if we want to define how much of the buffer to loop through on a per move basis, that can be a property of MovelistDictionary
        {
            if (i >= 0) //this is dumb, but you can potentially hit your last input on the move before the StickDir check/viceVersa
            {
                foreach (enum_ButtonName btn in bufferData[i].pressedButtons)
                {
                    if (InputData.CompareButtonInput(btn, inputCombo[comboCount]))
                    {
                        comboCount--;
                    }
                }
            }

            if (i >= 0)
            {
                if (InputData.CompareStickDir(bufferData[i].stickDirection, inputCombo[comboCount]))
                {
                    comboCount--;
                }
            }
        }
        
        if (comboCount == -1){
            return true;
        }
        else{
            return false;
        }
    }
}
