using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    private int score;
    [SerializeField] private List<Material> spriteScore = new List<Material>();
    [SerializeField] private GameObject spritePart;
    

    public void ChangeScore(int addScore, Transform trans)
    {
        int multiplicator = GameObject.FindObjectOfType<ComboSystem>().ActualCombo;
        if (multiplicator <= 0) multiplicator = 1;
        Instantiate(spritePart, trans.position, trans.rotation);
        spritePart.GetComponent<ParticleSystemRenderer>().material = spriteScore[multiplicator];
        int newScore = addScore * multiplicator;
        score += newScore;
        ActuScore();
    }

    public void ActuScore()
    {
        string finalTest = "";
        for (int i = 0; i < 9 - score.ToString().Length; i++)
        {
            finalTest += "0";
        }

        finalTest += score.ToString();
        finalTest = finalTest.Insert(3, ",");
        finalTest = finalTest.Insert(7, ",");
        
        scoreTxt.text = finalTest;
        
    }

}
