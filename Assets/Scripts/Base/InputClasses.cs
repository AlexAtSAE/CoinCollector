using System.Linq;
using UnityEngine;

public abstract class Input<t>
{
    public t Value;
    //True when the value is changed
    public bool OnValueChanged;
    public KeyInput[] keyInputs;
    abstract public void Update();
}

/// <summary>
/// Boolean input can be either true or false. If one of the inputs are true then the value of this input is true.
/// No effects are applicable
/// </summary>
public class BooleanInput : Input<bool>
{
    /// <summary> True the frame the input is pressed </summary>
    public bool InputPressed;
    // <summary> True the frame the input is released </summary>
    public bool InputReleased;
    /// <summary> Returns the amount of time this input has been held for</summary>
    public float TimeHeld;
    /// <summary> Returns the amount of time since this input has been released</summary>
    public float TimeSinceRelease;
    public BooleanInput(params KeyInput[] keyInputs)
    {
        this.keyInputs = keyInputs;
    }

    public override void Update()
    {
        bool thisValue = false;
        foreach (KeyInput keyInput in keyInputs)
        {
            if (Input.GetKey(keyInput.key))
            {
                thisValue = true;
                
            }
        }
        //Manage time held and since release
        if (thisValue == false) {TimeHeld = 0; TimeSinceRelease += Time.deltaTime;}
        else {TimeHeld+=Time.deltaTime; TimeSinceRelease = 0;}
        
        //check if value was pressed or released
        if (thisValue != Value)
        {
            if(thisValue == true)
                InputPressed = true;
            if(thisValue == false)
                InputReleased = true;
        }
        else {InputPressed = false; InputReleased = false;}
        Value = thisValue;
    }
    
}


/// <summary>
/// Basically a tri state boolean until easing is figured out
/// </summary>
public class LinearInput : Input<float>
{
    public LinearInput(params KeyInput[] keyInputs)
    {
        this.keyInputs = keyInputs;
    }
    
    public override void Update()
    {
        float thisValue = 0.0f;
        
        foreach (KeyInput keyInput in keyInputs)
        {
            inputEffects[] effects = keyInput.effects;
            
            if (Input.GetKey(keyInput.key))
            {
                if (!effects.Contains(inputEffects.Negate))
                    thisValue = 1.0f;
                else thisValue = -1.0f;
            }
        }
        
        Value = thisValue;
    }
}

public class VectorInput : Input<Vector2>
{
    public VectorInput(params KeyInput[] keyInputs)
    {
        this.keyInputs = keyInputs;
    }
    public override void Update()
    {
        Vector2 thisValue = new Vector2();
        
        foreach (KeyInput keyInput in keyInputs)
        {
            inputEffects[] effects = keyInput.effects;
            
            if (Input.GetKey(keyInput.key))
            {
                float val = 1.0f;
                if(effects.Contains(inputEffects.Negate))
                    val = -1.0f;
                if (effects.Contains(inputEffects.Swizzle))
                    thisValue.y += val;
                else
                    thisValue.x += val;
            }
        }
        
        Value = new Vector2(Mathf.Clamp(thisValue.x,-1f,1f), Mathf.Clamp(thisValue.y,-1f,1f));
    }
}

public class MouseVectorInput : Input<Vector2>
{
    MouseInput[] mouseInputs;
    public MouseVectorInput(params MouseInput[] mouseInputs)
    {
        this.mouseInputs = mouseInputs;
    }

    public override void Update()
    {
        Vector2 thisValue = new Vector2();
        foreach (MouseInput mouseInput in mouseInputs)
        {
            inputEffects[] effects = mouseInput.effects;
            float value = Input.GetAxis(mouseInput.inputName);
            if(effects.Contains(inputEffects.Negate))
                value = -value;
            if (effects.Contains(inputEffects.Swizzle))
            {
                thisValue.y += value;
            }
            else
            {
                thisValue.x += value;
            }
        }
        Value = thisValue;
    }
}


public class MouseInput
{
    public string inputName;
    public inputEffects[] effects {get; private set; }

    public MouseInput(string inputName, params inputEffects[] effects)
    {
        this.inputName = inputName;
        this.effects = effects;
    }

    public MouseInput(string inputName)
    {
        this.inputName = inputName;
        effects = new inputEffects[0];
    }
}
public class KeyInput
{
    public KeyCode key {get; private set; }
    public inputEffects[] effects {get; private set; }
    public KeyInput(KeyCode key, params inputEffects[] effects)
    {
        this.key = key;
        this.effects = effects;
    }
    public KeyInput(KeyCode key)
    {
        this.key = key;
        this.effects = new inputEffects[0];
    }
}
public enum inputEffects
{
    Swizzle,    //Only applicable to Vectors
    Negate,     //Inverted direction
}
