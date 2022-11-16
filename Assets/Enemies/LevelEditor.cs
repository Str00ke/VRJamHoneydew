using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MouseEditor))]
[ExecuteInEditMode, UnityEditor.InitializeOnLoad]
public class LevelEditor : MonoBehaviour
{

    private GameObject _enemySelected;

    private GameObject _enemySelectInPool;

    [SerializeField] private string levelName;
    [SerializeField] private List<GameObject> enemiesList;

    private GameObject levelHolder;

    private Vector2 pos;

    void Start()
    {
        //GetComponent<MouseEditor>().onMouseClick += OnClick;
        //GetComponent<MouseEditor>().onMouseUpdate += OnUpdate;
        //GetComponent<MouseEditor>().onRefresh += OnRefresh;

        CreateLevelHolder();

        if (enemiesList.Count > 0)
        {
            if (_enemySelectInPool == null) _enemySelectInPool = enemiesList[0];
            GameObject go = Instantiate(_enemySelectInPool);
            go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            _enemySelected = go;
        }
    }

    void OnDisable()
    {
        //GetComponent<MouseEditor>().onMouseClick -= OnClick;
        //GetComponent<MouseEditor>().onMouseUpdate -= OnUpdate;

    }

    void Update()
    {
        
    }

    void CreateLevelHolder()
    {
        if (levelHolder != null) return;
        levelHolder = new GameObject("LevelHolder");
    }

    public void Clear()
    {
        int max = levelHolder.transform.childCount;
        for (int i = 0; i < max; i++)
        {
            DestroyImmediate(levelHolder.transform.GetChild(0).gameObject);
        }
    }

    public void Restart()
    {
        Vector2 pos = _enemySelected.transform.position;
        DestroyImmediate(levelHolder);
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            DestroyImmediate(enemy.gameObject);
        }
        CreateLevelHolder();

        GameObject go = Instantiate(_enemySelectInPool, pos, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sortingOrder = 1;

        _enemySelected = go;
    }

    public void TryErase()
    {
        if (_enemySelected == null) return;


        Collider2D[] cols = Physics2D.OverlapBoxAll(_enemySelected.transform.position, Vector2.one, 0.0f);
        foreach (Collider2D col in cols)
        {
            Debug.Log(col.gameObject.name);

            if (col.GetComponent<Enemy>() && col.gameObject != _enemySelected) Erase(col.gameObject);
            break;
        }
    }

    public void Erase(GameObject obj)
    {
        DestroyImmediate(obj);
    }

    public void SaveLevel()
    {
        Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Levels/" + levelName + ".prefab");
        EditorUtility.ReplacePrefab(levelHolder, prefab, ReplacePrefabOptions.ConnectToPrefab);

    }

    public void OnClick(Vector2 position)
    {
        CreateLevelHolder();
        if (_enemySelected == null) return;

        Collider2D[] cols = Physics2D.OverlapBoxAll(_enemySelected.transform.position, Vector2.one, 0.0f);
        foreach (Collider2D col in cols)
        {
            if (col.GetComponent<Enemy>()) return;
        }

        if (levelHolder == null) CreateLevelHolder();
        _enemySelected.transform.parent = levelHolder.transform;
        GameObject go = Instantiate(_enemySelected, _enemySelected.transform.position, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sortingOrder = 1;

        _enemySelected = null;

        _enemySelected = go;

    }

    public void ChangeEnemyToPlace(int enemy)
    {
        CreateLevelHolder();
        Vector2 pos = _enemySelected.transform.position;
        DestroyImmediate(_enemySelected);
        _enemySelectInPool = enemiesList[enemy];
        GameObject go = Instantiate(_enemySelectInPool, pos, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sortingOrder = 1;

        _enemySelected = go;
    }

    public void OnUpdate(Vector2 position)
    {
        if (_enemySelected == null) return;
        pos = _enemySelected.transform.position;
        _enemySelected.transform.position = position;
    }

    public void OnRefresh()
    {
        //GetComponent<MouseEditor>().onMouseClick += OnClick;
        //GetComponent<MouseEditor>().onMouseUpdate += OnUpdate;
        //GetComponent<MouseEditor>().onRefresh += OnRefresh;
        DestroyImmediate(_enemySelected);
        if (enemiesList.Count > 0)
        {
            GameObject go = Instantiate(enemiesList[0]);
            go.GetComponent<SpriteRenderer>().sortingOrder = 1;

            _enemySelected = go;
        }
    }


    public GameObject CheckEnemyAlreadyThere()
    {
        if (_enemySelected == null) return null;

        Collider2D[] cols = Physics2D.OverlapBoxAll(_enemySelected.transform.position, Vector2.one, 0.0f);
        foreach (Collider2D col in cols)
        {
            if (col.GetComponent<Enemy>()) return col.gameObject;
        }
        return null;
    }
}
