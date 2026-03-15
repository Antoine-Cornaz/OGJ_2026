using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameManager _gameManager;
    void Start()
    {
        print("awaking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(){
        print("debug");
        SceneManager.LoadScene("Level1");
    }

    public void stopGame(){
        Application.Quit();
    }
}
