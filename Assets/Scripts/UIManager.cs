using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int score;
    [SerializeField] Text scoreText; 

    public void AddScore (int score)
    {
        this.score += score;
        ShowScore();
    }

    private void ShowScore ()
    {
        scoreText.text = this.score.ToString(); ;
    }
}
