using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Player Variables")]
    [SerializeField]
    int m_playerMaxLives;

    [Header("Enemy Variables")]
    [SerializeField]
    GameObject m_enemyPrefab;

    [SerializeField]
    AnimationCurve m_enemySpeedByEnemyNumbers;

    int _playerCurLives;

    int _enemyNb;
    int _enemyCurNb;
    float _enemySpeed;

    static GameManager instance;
    #endregion

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void EnemyKilled()
    {
        _enemyCurNb--;

        if(_enemyCurNb == 0)
        {
            Victory();
        }

        _enemySpeed = m_enemySpeedByEnemyNumbers.Evaluate(1 -_enemyCurNb/_enemyNb);
    }

    public void PlayerHit()
    {
        _playerCurLives--;

        if(_playerCurLives == 0)
        {
            Defeat();
        }
    }

    void Victory()
    {
        Debug.Log("You win !");
    }

    void Defeat()
    {
        Debug.Log("You lose");
    }
}
