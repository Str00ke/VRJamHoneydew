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
        score += addScore;
        ActuScore();
    }

    public void ActuScore()
    {
        scoreTxt.text = "   Score : " + score.ToString();
    }

}
