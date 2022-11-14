using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    [SerializeField] private float timeBtwMoves;
    [SerializeField] private int maxMoveFromCenter;

    public float TimeBtwMoves => timeBtwMoves;
    public int MaxMoveFromCenter => maxMoveFromCenter;
}
