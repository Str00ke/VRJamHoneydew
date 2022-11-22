using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboSystem : MonoBehaviour
{
    private int actualCombo;
    [SerializeField] private TextMeshProUGUI combo;

    public int ActualCombo => actualCombo;
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
        combo.text = "   Combo : " + actualCombo.ToString();
    }
}
