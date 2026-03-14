using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1100)]
public class InputManager : Singleton<InputManager>
{
    
    public event Action OnRespawnButtonPressed;
    public event Action<InputValue> OnMovePressed;
    
    
    public void OnMove(InputValue value)
    {
        Debug.Log($"PlayerMovement: OnMove {value}");
        OnMovePressed?.Invoke(value);
    }

    public void OnRespawn()
    {
        Debug.Log("InputManager: OnRespawn");
        OnRespawnButtonPressed?.Invoke();
    }
}
