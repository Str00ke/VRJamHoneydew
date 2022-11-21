using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    [SerializeField] private float timeBtwMoves;
    [SerializeField] private int maxMoveFromCenter;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float baseSpeed;

    [SerializeField] private AnimationCurve speedByNumbers;

    public float TimeBtwMoves => timeBtwMoves;
    public int MaxMoveFromCenter => maxMoveFromCenter;
    public float XOffset => xOffset;
    public float YOffset => yOffset;
    public float BaseSpeed => baseSpeed;

    public AnimationCurve SpeedByNumbers => speedByNumbers;

}
