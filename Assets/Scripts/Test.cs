using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    InputManager inputManager;
    UserSettings gameSettings;
    void Start()
    {
        inputManager = InputManager.instance;
        gameSettings = UserSettings.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
