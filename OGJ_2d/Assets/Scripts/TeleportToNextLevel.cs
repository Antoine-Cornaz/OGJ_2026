using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextLevel : MonoBehaviour
{
    [SerializeField] private int Level = 2;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))return;
        
        Debug.Log(other.name);
        teleportToNextLevel();
    }

    // Update is called once per frame
    public void teleportToNextLevel()
    {
        string levelName = "level" + Level;
        SceneManager.LoadScene(levelName);
        GameManager.Instance.ForceCallGameResetAndStart();
        _gameManager.CallSetLevelGame(Level);
    }
}
