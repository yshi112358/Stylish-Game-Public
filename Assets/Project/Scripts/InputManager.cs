using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInputActions inputActions;
    public enum InputState
    {
        Player,
        UI
    }
    private static InputState _inputState;
    public static InputState inputState
    {
        set
        {
            if (_inputState != value)
                InputChanger(value);
            _inputState = value;
        }
        get
        {
            return _inputState;
        }
    }
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }
    private static void InputChanger(InputState inputState)
    {
        switch(inputState)
        {
            case InputState.Player:
                inputActions.Player.Enable();
                inputActions.UI.Disable();
                break;
            case InputState.UI:
                inputActions.Player.Disable();
                inputActions.UI.Enable();
                break;
        }    
    }
}
