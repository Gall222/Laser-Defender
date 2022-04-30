using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public void loadGameOver()
    {
        
        StartCoroutine(DeathContinuously());
    }

    public void loadGameScene()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void loadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DeathContinuously()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");
    }
}
