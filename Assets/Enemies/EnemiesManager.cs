using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private EnemiesZone zone;
    [SerializeField] private GameObject[] enemiesPool; //For random gen (for now)

    //TODO: Move this, this has nothing to do here.
    [SerializeField] private LevelData levelData;
    private float _currTime;
    private int _currX;

    private Transform _zone;

    private Transform _enemiesHolder;

    public Transform EnemiesHolder => _enemiesHolder;

    void Awake()
    {
        GameObject hold = new GameObject("EnemiesHolder");
        _enemiesHolder = hold.transform;
    }
    
    void Start()
    {
        Init();

        _currTime = levelData.TimeBtwMoves;
    }

    void Update()
    {
        _currTime -= Time.deltaTime;
        if (_currTime <= 0)
        {
            Move();
            _currTime = levelData.TimeBtwMoves;
        }
    }

    public void Init()
    {
        _zone = zone.Holder.transform;
        for (int i = 0; i < _zone.childCount; i++)
        {
            int rnd = Random.Range(0, enemiesPool.Length);
            GameObject enemy = Instantiate(enemiesPool[rnd]);
            enemy.transform.parent = _enemiesHolder;
            enemy.transform.position = _zone.GetChild(i).position;
        }
    }

    void Move()
    {
        for (int i = 0; i < _enemiesHolder.childCount; i++)
        {
            //Might do move before check, or after...
            //Move
            if (_currX > levelData.MaxMoveFromCenter || _currX < -levelData.MaxMoveFromCenter)
            {
                //descend
            }
        }
    }
}
