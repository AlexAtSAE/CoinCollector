using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public UserSettings userSettings;
    [CustomInput] public BooleanInput JumpInput;
    [CustomInput] public BooleanInput SlidingInput;
    [CustomInput] public BooleanInput InteractInput;
    [CustomInput] public BooleanInput RefreshKeybinds;
    [CustomInput] public VectorInput MovementInput;
    [CustomInput] public MouseVectorInput RotateView;
    void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RefreshInputs();
    }

    void Update()
    {
        //Figure out a way to collect these and update them automatically (attributes?) [CustomInput]
        JumpInput.Update();
        InteractInput.Update();
        RefreshKeybinds.Update();
        MovementInput.Update();
        RotateView.Update();
        SlidingInput.Update();
    }
    public void RefreshInputs()
    {
        Debug.Log("Refreshing Inputs");
        JumpInput = new BooleanInput(new KeyInput(KeyCode.Space));
        InteractInput = new BooleanInput(new KeyInput(userSettings.InteractKey));
        RefreshKeybinds = new BooleanInput(new KeyInput(KeyCode.Return));
        SlidingInput = new BooleanInput(new KeyInput(userSettings.SlideKey));

        MovementInput = new VectorInput(
            new KeyInput(KeyCode.W, inputEffects.SwizzleXY),
            new KeyInput(KeyCode.S, inputEffects.SwizzleXY, inputEffects.Negate),
            new KeyInput(KeyCode.D),
            new KeyInput(KeyCode.A, inputEffects.Negate));

        RotateView = new MouseVectorInput(
            new MouseInput("Mouse X"),
            new MouseInput("Mouse Y", inputEffects.SwizzleXY));


    }
}

class CustomInput : Attribute
{
    
}