using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    private int actualCombo;
    [SerializeField] private Image baseImage;
    [SerializeField] private List<Sprite> allImages = new List<Sprite>();


    public int ActualCombo => actualCombo;

    private void Start()
    {
        baseImage.sprite = allImages[0];
    }

    public void ComboPlus()
    {
        if(actualCombo < 8) actualCombo++;
        ActuCombo();
        
    }

    public void resetCombo()
    {
        actualCombo = 0;
        ActuCombo();
    }

    public void ActuCombo()
    {
        baseImage.sprite = allImages[actualCombo];
    }
}
