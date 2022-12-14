using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor;
//using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private EnemiesZone zone;
    [SerializeField] private GameObject[] enemiesPool; //For random gen (for now)

    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private float moveCurveSpeed;

    //TODO: Move this, this has nothing to do here.
    [SerializeField] private LevelData levelData;
    private float _currTime;
    private int _currX;
    private int _currDisplace = 1; //Start moving right

    private float offsetX;
    private float offsetY;

    private Transform _zone;

    private Transform _enemiesHolder;
    private List<Enemy> enemyList = new List<Enemy>();

    public Transform EnemiesHolder => _enemiesHolder;


    void Awake()
    {
        GameObject hold = new GameObject("EnemiesHolder");
        _enemiesHolder = hold.transform;
    }
    
    void Start()
    {
        Init();
        SetShooters();

        _currTime = levelData.TimeBtwMoves;
        Enemy.onKill += OnEnemyKilled;

    }

    void Update()
    {
        _currTime -= Time.deltaTime * levelData.BaseSpeed;
        if (_currTime <= 0)
        {
            Move();
            _currTime = levelData.TimeBtwMoves;
        }
    }

    public void Init()
    {
        _zone = zone.Holder.transform;
        _zone.transform.position = transform.position;
        float sizeX = _zone.GetChild(1).localPosition.x - _zone.GetChild(0).localPosition.x;
        float sizeY = _zone.GetChild(10).localPosition.y - _zone.GetChild(0).localPosition.y;
        //Debug.Log(sizeX + "   " + sizeY);
        offsetX = sizeX;
        offsetY = sizeY;
        for (int i = 0; i < _zone.childCount; i++)
        {
            int rnd = Random.Range(0, enemiesPool.Length);
            GameObject enemy = Instantiate(enemiesPool[rnd]);
            enemy.transform.parent = _enemiesHolder;
            enemyList.Add(enemy.GetComponent<Enemy>());
            enemy.transform.position = _zone.GetChild(i).position;
        }
    }

    void Move()
    {
        _currX += _currDisplace;
        bool pass = false; //This is shit
        for (int i = 0; i < _enemiesHolder.childCount; i++)
        {
            Vector3 pos = _enemiesHolder.GetChild(i).transform.position;
            
            if (_currX > levelData.MaxMoveFromCenter || _currX < -levelData.MaxMoveFromCenter)
            {
                //descend
                pass = true;
                pos.y -= levelData.YOffset;

                //_enemiesHolder.GetChild(i).transform.position = pos;
                if (enemyList[i].Anim != null) enemyList[i].Anim.Play("AlienDown");
               

            }
            else
            {
                //Move horizontally
                pos.x += levelData.XOffset * _currDisplace;

                //_enemiesHolder.GetChild(i).transform.position = pos;

                if (enemyList[i].Anim != null)
                {
                    if (_currDisplace < 0) enemyList[i].Anim.Play("AlienLeft");
                    else enemyList[i].Anim.Play("AlienRight");
                }

            }

            StartCoroutine(MoveCor(_enemiesHolder.GetChild(i).transform, _enemiesHolder.GetChild(i).transform.position, pos, moveCurveSpeed));
        }
        if (pass)
            _currDisplace *= -1;
    }

    IEnumerator MoveCor(Transform objT, Vector2 start, Vector2 end, float speed)
    {
        float t = 0;
        float y = 0;
        while (t < 1)
        {
            y = moveCurve.Evaluate(t);
            objT.position = Vector2.Lerp(start, end, t);
            t += Time.deltaTime * speed * y;
            yield return null;
        }
        transform.position = end; //Ensure position get fixed if t < 1 at while loop break
        yield return null;
    }


    public void SetShooters()
    {
        Dictionary<float, Enemy> yLow = new Dictionary<float, Enemy>();
        for (int i = 0; i < _enemiesHolder.childCount; i++)
        {
            if (!_enemiesHolder.GetChild(i).GetComponent<Enemy>().DeathPending)
            {
                _enemiesHolder.GetChild(i).GetComponent<Enemy>().Shooter = false;

                float eX = _enemiesHolder.GetChild(i).transform.position.x;
                float eY = _enemiesHolder.GetChild(i).transform.position.y;

                //If there's no enemy registered for column at pos x, just add one, assuming it's the lowest
                if (!yLow.ContainsKey(eX))
                    yLow.Add(eX, _enemiesHolder.GetChild(i).GetComponent<Enemy>());


                //Or else compare y of both registered and iterator
                else if (eY < yLow[eX].transform.position.y) 
                    yLow[eX] = _enemiesHolder.GetChild(i).GetComponent<Enemy>();
            }
        }


        foreach (Enemy e in yLow.Values)
        {
            e.Shooter = true;
        }
    }

    public List<Enemy> GetEnemyAdjascent(Vector2 pos)
    {
        List<Enemy> rList = new List<Enemy>();
        for (int i = 0; i < _enemiesHolder.childCount; i++)
        {
            float eX = _enemiesHolder.GetChild(i).transform.position.x;
            float eY = _enemiesHolder.GetChild(i).transform.position.y;

            if (Mathf.Approximately(eX, pos.x) && !Mathf.Approximately(eY, pos.y))
            {
                float a = eY - pos.y;
                float b = offsetY;
                if (Mathf.Approximately(a, b))
                {
                    rList.Add(_enemiesHolder.GetChild(i).GetComponent<Enemy>());
                }
            }
            else if (eY == pos.y && eX != pos.x)
            {
                float a = Vector2.Distance(pos, new Vector2(eX, eY));
                float b = offsetX;
                if (Mathf.Approximately(a, b))
                {
                    rList.Add(_enemiesHolder.GetChild(i).GetComponent<Enemy>());
                }
            }
        }

        return rList;
    }

    public void OnEnemyKilled()
    {
        SetShooters();
    }
}
