using UnityEngine;

public class CameraAssign : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.tag = "MainCamera";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
