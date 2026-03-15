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

    public event Action<int> OnSetLevelGame;

    private bool isGameOn = false;

    public float removeTuto1Timer = 2f;
    private float removeTuto1TimerTarget = -1f;
    private bool tuto1Timerdone = false;
    private bool tuto1timerstarted = false;

    private bool showTuto2 = false;
    public GameObject tuto2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Awake()
    {
        base.Awake();
        InputManager.Instance.OnRespawnButtonPressed += OnRespawn;
        tuto2.SetActive(false);
    }
    
    private void Start()
    {
        CallGameResetAndStart();
    }

    private void Update()
    {
        Rigidbody2D playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        if (playerRB.linearVelocity.magnitude != 0 && !tuto1timerstarted && !tuto1Timerdone)
        {
            removeTuto1TimerTarget = Time.time + removeTuto1Timer;
            tuto1timerstarted = true;
        }   

        if (removeTuto1TimerTarget > 0f && removeTuto1TimerTarget < Time.time)
        {
            GameObject.FindWithTag("tuto1").SetActive(false);
            tuto1Timerdone = true;
            removeTuto1TimerTarget = -1f;
        }

        if (playerRB.transform.position.x >= 25f && playerRB.transform.position.x <= 37f && !showTuto2)
        {
            showTuto2 = true;
            tuto2.SetActive(true);
        }

        if (playerRB.transform.position.x > 40f && showTuto2)
        {
            showTuto2 = false;
            tuto2.SetActive(false);
        }
    }

    public void CallGameResetAndStart()
    {
        if (isGameOn) return;
        ForceCallGameResetAndStart();
    }

    public void CallSetLevelGame(int level)
    {
        OnSetLevelGame?.Invoke(level);
    }

    public void ForceCallGameResetAndStart()
    {
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
