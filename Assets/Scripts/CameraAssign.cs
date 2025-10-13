using UnityEngine;

public class CameraAssign : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.tag = "MainCamera";
    }
}
