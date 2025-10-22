using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    
    private HashSet<CustomInputType> inputs;
    void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputs = new HashSet<CustomInputType>();
        RefreshInputs();
    }

    void Update()
    {
        foreach (CustomInputType inp in inputs)
            inp.Update();
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


        //Collect inputs for updater
        
        FieldInfo[] fields = typeof(InputManager).GetFields();
        inputs.Clear();
        foreach (FieldInfo field in fields)
        {
            if (field.GetCustomAttributes(typeof(CustomInput)).Any())
                inputs.Add(field.GetValue(this) as CustomInputType);
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
class CustomInput : Attribute{}