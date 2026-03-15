using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1100)]
public class InputManager : Singleton<InputManager>
{
    public event Action OnRespawnButtonPressed;
    public event Action<InputValue> OnMovePressed;

    public event Action OnZoomPressed;
    public event Action OnZoomReleased;

    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        var zoomAction = playerInput.actions["Zoom"];
        zoomAction.started += HandleZoomStarted;
        zoomAction.canceled += HandleZoomCanceled;
    }

    private void OnDisable()
    {
        var zoomAction = playerInput.actions["Zoom"];
        zoomAction.started -= HandleZoomStarted;
        zoomAction.canceled -= HandleZoomCanceled;
    }

    private void HandleZoomStarted(InputAction.CallbackContext context)
    {
        Debug.Log("InputManager: OnZoom Pressed");
        OnZoomPressed?.Invoke();
    }

    private void HandleZoomCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("InputManager: OnZoom Released");
        OnZoomReleased?.Invoke();
    }

    public void OnMove(InputValue value)
    {
        Debug.Log($"InputManager: OnMove {value}");
        OnMovePressed?.Invoke(value);
    }

    public void OnRespawn()
    {
        Debug.Log("InputManager: OnRespawn");
        OnRespawnButtonPressed?.Invoke();
    }
}