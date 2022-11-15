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
    Transform m_enemyHolder;
    [SerializeField]
    int m_enemyRows = 1;
    [SerializeField]
    int m_enemyColumns = 1;

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
        _enemyNb = m_enemyRows * m_enemyColumns;
    }

    private void Update()
    {
        m_enemyHolder.transform.Translate(-m_enemyHolder.transform.up * _enemySpeed);
    }

    void CreateEnemies()
    {
        GameObject[,] enemies = new GameObject[m_enemyColumns, m_enemyRows];
        Vector2 holderPos = m_enemyHolder.position;
        Vector2 enemySize = m_enemyPrefab.GetComponent<Collider2D>().bounds.size;
        Vector2 enemiesSize = new(((enemySize.x + 1) * m_enemyColumns) - 1, ((enemySize.y + 1) * m_enemyRows) - 1);
        for(int ci = 0; ci < m_enemyColumns; ci++)
        {
            for(int ri = 0; ri < m_enemyRows; ri++)
            {
                Vector2 startPos = new ((enemySize.x + 1 * ci) - 1, (enemySize.y + 1 * ri) - 1);
                Vector2 correctStartPos = holderPos + (startPos - enemiesSize/2);
                enemies[ci, ri] = Instantiate(m_enemyPrefab, correctStartPos, m_enemyHolder.rotation, m_enemyHolder);
                //Assign pos to enemy script
            }
        }
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
