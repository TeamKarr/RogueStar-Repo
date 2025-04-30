using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardTrigger : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent onkeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            //Debug.Log("Key Pressed: " + key);
            onkeyPressed?.Invoke();
        }
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }
}
