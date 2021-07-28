using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// score is managed by this class
/// if player collects keys player rewarded with 10 points for each key
/// </summary>
public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        RefreshUI();
    }

    //the score will increase by 10 points after each key collect
    public void IncreaseScore(int increament)
    {
        score += increament;
        RefreshUI();
    }

    //the score ui update with new score
    private void RefreshUI()
    {
        scoreText.text = "Score: " + score;
    }

    //decrese score
    public void DecreaseScore(int decreament)
    {
        score -= decreament;

    }
}

