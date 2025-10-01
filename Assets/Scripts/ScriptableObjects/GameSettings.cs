using UnityEngine;

[CreateAssetMenu(fileName = "UserSettings", menuName = "Scriptable Objects/UserSettings")]
public class UserSettings : ScriptableObject
{
    public static UserSettings instance;

    public KeyCode Interact;
    void OnEnable()
    {
        instance = this;
        
    }
    
    
    //All settings
    public bool gamePaused;
    public Vector2 mouseSensitivity;

    
}

