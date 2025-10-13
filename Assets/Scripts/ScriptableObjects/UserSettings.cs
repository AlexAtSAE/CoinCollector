using UnityEngine;

[CreateAssetMenu(fileName = "UserSettings", menuName = "Scriptable Objects/UserSettings")]
public class UserSettings : ScriptableObject
{

    public KeyCode InteractKey;
    public KeyCode SlideKey;

    
    
    //All settings
    public bool gamePaused;
    public Vector2 mouseSensitivity;
    public float FOV;
}

