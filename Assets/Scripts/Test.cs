using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    InputManager inputManager;
    void Start()
    {
        inputManager = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        /*bool jumpInput = inputManager.Jump.Value;
        Debug.Log($"Jump: {jumpInput}");
        Vector2 move = inputManager.Move.Value;
        Debug.Log($"Movement: {move}");
        bool interact = inputManager.Interact.Value;
        Debug.Log($"Interact: {interact}");*/
        Debug.Log($"X:{inputManager.RotateView.Value.x} Y:{inputManager.RotateView.Value.y}");
    }
}
