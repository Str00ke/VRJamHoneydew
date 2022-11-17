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
    EnemiesManager m_enemiesManager;

    [Header("Level")]
    [SerializeField]
    LevelData m_levelData;

    int _playerCurLives;

    int _enemiesNb;
    int _enemiesCurNb;
    float _enemiesSpeed;

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
        m_enemiesManager.Init(m_levelData);
        _enemiesNb = m_enemiesManager.EnemiesNb;
    }

    public void EnemyKilled()
    {
        _enemiesCurNb--;

        if(_enemiesCurNb == 0)
        {
            Victory();
        }

        m_enemiesManager.SpeedMultiplyer = m_levelData.SpeedByNumbers.Evaluate(1 -_enemiesCurNb/_enemiesNb);
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
