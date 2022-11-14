using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private Sprite _spr;
    [SerializeField] private int _lifePoints;

    public Sprite Sprite => _spr;
    public int LifePoints => _lifePoints;
}
