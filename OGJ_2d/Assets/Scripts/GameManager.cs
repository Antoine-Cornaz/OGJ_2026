using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1000)]
public class GameManager : Singleton<GameManager>
{
    
    public event Action OnGameStart;
    public event Action GameEnded;
    public event Action OnGameReset;

    private bool isGameOn = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Awake()
    {
        base.Awake();
        InputManager.Instance.OnRespawnButtonPressed += OnRespawn;
    }
    
    private void Start()
    {
        CallGameResetAndStart();
    }
    
    public void CallGameResetAndStart()
    {
        if (isGameOn) return;
        isGameOn = true;
        Debug.Log("GameManager: Call game reset and start");
        OnGameReset?.Invoke();
        OnGameStart?.Invoke();
        Time.timeScale = 1;
    }
    
    public void CallGameEnd()
    {
        if (!isGameOn) return;
        isGameOn = false;
        Debug.Log("GameManager: Call game end");
        GameEnded?.Invoke();
        Time.timeScale = 0;
    }
    

    private void OnRespawn()
    {
        Debug.Log("GameManager: OnRespawn");
        CallGameResetAndStart();
    }
}
