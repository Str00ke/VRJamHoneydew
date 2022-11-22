using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    private int score;
    
    public void ChangeScore(int addScore)
    {
        int multiplicator = GameObject.FindObjectOfType<ComboSystem>().ActualCombo;
        if (multiplicator <= 0) multiplicator = 1;
        int newScore = addScore * multiplicator;
        score += newScore;
        ActuScore();
    }

    public void ActuScore()
    {
        scoreTxt.text = "   Score : " + score.ToString();
    }

}
