using System;
using TMPro;
using UnityEngine;


public class ScoreKeeper : MonoBehaviour
{
    private static TextMeshProUGUI _scoreText;
    private static int currentScore;

    private void Start()
    {
        currentScore = 0;//PlayerPrefs.GetInt("score");
        _scoreText = GetComponent<TextMeshProUGUI>();
        _scoreText.text = PlayerPrefs.GetInt("score", currentScore).ToString();
    }

    

    public static void SetScore(int point)
    {
        PlayerPrefs.SetInt("score", currentScore += point);
        _scoreText.text = PlayerPrefs.GetInt("score", currentScore).ToString();
    }
}
