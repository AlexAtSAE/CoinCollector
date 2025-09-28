using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Input<t>
{
    public t Value { get; private set; }
    public float TimeHeld { get; private set; }
    public Ease Ease;
    public RawInput[] Inputs;

    public Input(params RawInput[] inputs)
    {
        Inputs = inputs;
    }

    public void Update()
    {
        t Output = default; //autocomplete goat
        foreach (RawInput input in Inputs)
        {
            //Get values for this input key
            t ThisInputResult = default;
            float res = input.Result();
            
            //get result of this input type
            if (typeof(t) == typeof(Vector2))
            {
                ThisInputResult = (t)(object)new Vector2(res,0.0f);
            }
            if (typeof(t) == typeof(float))
            {
                ThisInputResult = (t)(object)res;
            }
            if (typeof(t) == typeof(bool))
            {
                ThisInputResult = (t)(object)(res >= 0.5f);
            }
            //Handle input effects
            InputEffects[] effects = input.Effects;
            foreach (InputEffects effect in effects)
            {
                if (typeof(t) == typeof(bool) || typeof(t) == typeof(float) && effect == InputEffects.Swizzle)
                    continue;
                if (effect == InputEffects.Swizzle)
                {
                    //Only executes if t is NOT bool or float
                    ThisInputResult = (t)(object)new Vector2(0.0f,res);
                }

                if (effect == InputEffects.Negate)
                {
                    if (ThisInputResult is bool b)
                    {
                        ThisInputResult = (t)(object)!b;
                    }
                    if (ThisInputResult is float F)
                    {
                        ThisInputResult = (t)(object)-F;
                    }
                    if (ThisInputResult is Vector2 V)
                    {
                        ThisInputResult = (t)(object)(-V);
                    }
                }
                
            }
            //Add to final output
            if (Output is float outputFloat &&  ThisInputResult is float inputResultFloat)
            {
                Output =  (t)(object)(outputFloat+inputResultFloat);
            }
            if (Output is bool outputBool &&  ThisInputResult is bool inputResultBool)
            {
                Output =  (t)(object)(outputBool||inputResultBool);
            }
            if (Output is Vector2 outputVector &&  ThisInputResult is Vector2 inputResultVector)
            {
                Output =  (t)(object)(outputVector+inputResultVector);
            }
        }
        //Set value of this input
        Value = (t)(object)Output;
    }
}

public abstract class RawInput
{
    public InputEffects[] Effects;
    public abstract float Result();
}

public class MouseInput : RawInput
{
    String axis;
    public MouseInput(String Axis, params InputEffects[] inputEffects)
    {
        axis = Axis;
        Effects = inputEffects;
    }

    public override float Result()
    {
        return Input.GetAxis(axis);
    }
    
    
}
public class KeyInput : RawInput
{
    KeyCode Key;
    public KeyInput(KeyCode key, params InputEffects[] effects)
    {
        Key = key;
        Effects = effects;
    }
    public override float Result()
    {
        if (Input.GetKey(Key))
            return 1f;
        return 0f;
    }
    
}

public static class InputFunctions
{
    
    static float None(float x)
    {
        return x;
    }
}

public enum Ease
{
    None,
    Linear,
    Quadratic,
    Cubic,
}

public enum InputEffects
{
    Swizzle,
    Negate
}