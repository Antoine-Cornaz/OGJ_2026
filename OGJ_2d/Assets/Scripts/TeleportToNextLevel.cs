using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextLevel : MonoBehaviour
{
    [SerializeField] private string LevelName = "Level2";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))return;
        
        Debug.Log(other.name);
        teleportToNextLevel();
    }

    // Update is called once per frame
    private void teleportToNextLevel()
    {
        SceneManager.LoadScene(LevelName);
        GameManager.Instance.ForceCallGameResetAndStart();
    }
}
