using System;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{

    [SerializeField] private float dieHeight = 8f; 
    private GameManager _gameManager;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnGameStart += OnStart;
    }

    private void OnStart()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)  return;
        if (transform.position.y >= dieHeight) return;
        
        isDead = true;
        Debug.Log("PlayerDie: Call game end");
        _gameManager.CallGameEnd();


    }
}
