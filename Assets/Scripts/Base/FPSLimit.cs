using UnityEngine;
public class FPSLimit : MonoBehaviour
{
    void OnEnable()
    {
        Application.targetFrameRate = 120;
    }
}
