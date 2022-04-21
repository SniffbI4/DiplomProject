using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int currentScore;

    private void OnEnable()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        TestDelegateToScore.ScoreChanged += ChangeScore;
    }

    private void ChangeScore(TestDelegateToScore obj)
    {
        currentScore += obj.Score;
        scoreText.text = currentScore.ToString();
    }

    private void OnDisable()
    {
        TestDelegateToScore.ScoreChanged -= ChangeScore;
    }
}
