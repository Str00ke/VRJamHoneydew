using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int m_playerMaxLives;

    int _playerCurLives;

    int _enemiesNb;

    static GameManager instance;

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
        /*_enemiesNb = FindObjectsOfType<Enemy>().Count();*/
    }

    public void EnemyKilled()
    {
        _enemiesNb--;

        if(_enemiesNb == 0)
        {
            Victory();
        }
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
