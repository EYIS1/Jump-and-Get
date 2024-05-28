using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Score scoreText;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            int savedScore = PlayerPrefs.GetInt("Score", 0);
            GetComponent<Text>().text = "SCORE " + savedScore.ToString();
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            int bestScore = PlayerPrefs.GetInt("BestScore", 0);
            GetComponent<Text>().text = "BEST " + bestScore.ToString();
        }
    }
}
