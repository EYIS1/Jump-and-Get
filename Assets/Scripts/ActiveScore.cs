using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveScore : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" && Time.timeScale == 1)
        {
            int aScore = PlayerPrefs.GetInt("AScore", 0);
            GetComponent<Text>().text = "SCORE " + aScore.ToString();
        }
    }
}
