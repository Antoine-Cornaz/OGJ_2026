using System;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{

    private float dieHeight = -10f; 
    private GameManager _gameManager;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnSetLevelGame += OnSetLevel;
    }

    private void OnSetLevel(int level)
    {
        dieHeight = level switch
        {
            1 => -10,
            2 => -50,
            _ => dieHeight
        };
    }

    private void OnEnable()
    {
        _gameManager.OnGameStart += OnStart;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStart -= OnStart;
    }

    private void OnStart()
    {
        isDead = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead)  return;
        if (transform.position.y >= dieHeight) return;
        
        isDead = true;
        Debug.Log("PlayerDie: Call game end");
        _gameManager.CallGameEnd();


    }
}
