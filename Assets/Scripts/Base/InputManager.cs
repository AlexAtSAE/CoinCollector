using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    
    //Inputs that will be used
    public Input<Vector2> Move = new Input<Vector2>(
        new KeyInput(KeyCode.W,InputEffects.Swizzle),
        new KeyInput(KeyCode.A,InputEffects.Negate),
        new KeyInput(KeyCode.S,InputEffects.Swizzle,InputEffects.Negate),
        new KeyInput(KeyCode.D))
    {
        Ease = Ease.Quadratic
    };
    
    
    public Input<bool> Jump = new Input<bool>(new KeyInput(KeyCode.Space))
    {
        Ease = Ease.None
    };
    
    public Input<bool> Interact = new Input<bool>(new KeyInput(KeyCode.E))
    {
        Ease = Ease.None
    };

    public Input<Vector2> RotateView = new Input<Vector2>(
        new MouseInput("Mouse X"), 
        new MouseInput("Mouse Y", InputEffects.Swizzle))
    {
        Ease = Ease.None
    };

    void OnEnable()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move.Update();
        Jump.Update();
        Interact.Update();
        RotateView.Update();
    }
}
