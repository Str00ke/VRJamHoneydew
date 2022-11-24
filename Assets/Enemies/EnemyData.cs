using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct ExplodeChanceCombo
{
    public float comboValue;
    public float chanceValue;
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private Sprite _spr;
    [SerializeField] private int _lifePoints;
    [SerializeField] private float _shootTimeMin;
    [SerializeField] private float _shootTimeMax;
    [SerializeField] private List<ExplodeChanceCombo> _explodeChanceMult;

    public Sprite Sprite => _spr;
    public int LifePoints => _lifePoints;
    public float ShootTimeMin => _shootTimeMin;
    public float ShootTimeMax => _shootTimeMax;
    public List<ExplodeChanceCombo> ExplodeChancePercentage => _explodeChanceMult;
}
