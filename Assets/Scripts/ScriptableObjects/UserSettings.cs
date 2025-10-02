using UnityEngine;

[CreateAssetMenu(fileName = "UserSettings", menuName = "Scriptable Objects/UserSettings")]
public class UserSettings : ScriptableObject
{
    public static UserSettings instance;

    public KeyCode InteractKey;
    void OnValidate()
    {
        instance = this;
    }
    
    
    //All settings
    public bool gamePaused;
    public Vector2 mouseSensitivity;
}

