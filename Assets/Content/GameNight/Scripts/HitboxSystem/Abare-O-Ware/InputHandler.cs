using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static PlayerInput playerInput;
    protected MotionReader _motionReader;

    protected InputAction _moveAction;
    protected InputAction _jumpAction;
    protected InputAction _pauseAction;
    protected InputAction _resumeAction;
    protected InputAction _lPunchAction;
    protected InputAction _mPunchAction;
    protected InputAction _hPunchAction;
    protected InputAction _lKickAction;
    protected InputAction _mKickAction;
    protected InputAction _hKickAction;

    public static Vector2 movement;
    public static bool isCrouching;
    public static bool jumpPressed;
    public static bool pausePressed;
    public static bool resumePressed;
    public static bool lPunchPressed;
    public static bool mPunchPressed;
    public static bool hPunchPressed;
    public static bool lKickPressed;
    public static bool mKickPressed;
    public static bool hKickPressed;


    private void Awake()
    {
        _motionReader = GetComponent<MotionReader>();
        playerInput = GetComponent<PlayerInput>();

        _moveAction = playerInput.actions["Move"];
        _jumpAction = playerInput.actions["Jump"];
        _pauseAction = playerInput.actions["Pause"];
        _resumeAction = playerInput.actions["Resume"];
        _lPunchAction = playerInput.actions["lPunch"];
        _mPunchAction = playerInput.actions["mPunch"];
        _hPunchAction = playerInput.actions["hPunch"];
        _lKickAction = playerInput.actions["lKick"];
        _mKickAction = playerInput.actions["mKick"];
        _hKickAction = playerInput.actions["hKick"];
    }

    // Update is called once per frame
    void Update()
    {
        movement = _moveAction.ReadValue<Vector2>();
        isCrouching = _moveAction.ReadValue<Vector2>().y < -0.4;

        pausePressed = _pauseAction.WasPressedThisFrame();
        resumePressed = _resumeAction.WasPressedThisFrame();

        jumpPressed = _jumpAction.WasPressedThisFrame();
        lPunchPressed = _lPunchAction.WasPressedThisFrame();
        mPunchPressed = _mPunchAction.WasPressedThisFrame();
        hPunchPressed = _hPunchAction.WasPressedThisFrame();
        lKickPressed = _lKickAction.WasPressedThisFrame();
        mKickPressed = _mKickAction.WasPressedThisFrame();
        hKickPressed = _hKickAction.WasPressedThisFrame();
        
        _motionReader.InputTimer();
        _motionReader.ReadInputs();
        _motionReader.ReleaseButton();

        //Makes sure the player can't store inputs by continuously inputing.
        if (_motionReader.inputs.Count > 10)
        {
            _motionReader.inputs.RemoveAt(0);
        }

        if (pausePressed)
        {
            GlobalInstance.Pause();
        }
        if (resumePressed)
        {
            GlobalInstance.Resume();
        }
    }
}
