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

    private EnemiesManager eManager;
    private Transform enemiesHolder;

    int _playerCurLives;

    int _enemyNb;
    int _enemyCurNb;
    float _enemySpeed;

    static GameManager instance;

    [Header("HP")] [SerializeField] private List<GameObject> listHeart = new List<GameObject>();

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

        _playerCurLives = m_playerMaxLives;
    }

    private void Start()
    {
        Enemy.onKill += OnEnemyKilled;

        eManager = FindObjectOfType<EnemiesManager>();
        enemiesHolder = eManager.EnemiesHolder;

        _enemyCurNb = enemiesHolder.childCount;
    }

    void OnDisable()
    {

    }

    private void Update()
    {

    }

    public void OnEnemyKilled()
    {
        _enemyCurNb--;
        //Debug.Log(enemiesHolder.childCount);
        if(enemiesHolder.childCount == 1) //Don't know yet why it does not goes to zero.
        {
            Victory();
        }

        //_enemySpeed = m_enemySpeedByEnemyNumbers.Evaluate(1 -_enemyCurNb/_enemyNb);
    }

    public void PlayerHit()
    {
        _playerCurLives--;

        for (int i = 0; i < listHeart.Count; i++)
        {
            if(i + 1 > _playerCurLives) listHeart[i].SetActive(false);
        }

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
