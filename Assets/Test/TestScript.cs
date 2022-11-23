using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    public InputActionProperty helloInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (helloInput.action.WasPressedThisFrame()) Debug.Log("HelloWorld!");
    }
}
