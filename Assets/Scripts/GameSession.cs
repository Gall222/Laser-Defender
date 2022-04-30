using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    
    
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int score)
    {
        this.score += score;
    }

    public void ResetGame()
    {
        
        Destroy(gameObject);
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
