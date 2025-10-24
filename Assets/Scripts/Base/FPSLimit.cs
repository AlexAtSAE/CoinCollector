using UnityEngine;
public class FPSLimit : MonoBehaviour
{
    public int FPS = 120;
    void OnEnable()
    {
        Application.targetFrameRate = FPS;
    }
}
