using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;
    [SerializeField] private GameObject bulletPrefab;

    public static System.Action onKill;

    private int life;

    private float _currShootTime;

    [HideInInspector]
    public bool Shooter { get; set; }

    public bool DeathPending { get; private set; }
    
    [SerializeField] Animator anim;

    public Animator Anim => anim;
    
    public GameObject score;

    void Awake()
    {
        if (!TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
        {
            SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
            spr.sprite = _data.Sprite;
        }

        life = _data.LifePoints;

        SetShootRandomTime();
    }
    void Start()
    {
        
        
    }

    void SetShootRandomTime()
    {
        _currShootTime = Random.Range(_data.ShootTimeMin, _data.ShootTimeMax);

    }

    /*
    void OnTriggerEnter2D(Collider2D col)
    {
        life--;
        if (life <= 0)
            Destroy(gameObject);
    }
    */

    void Update()
    {
        if (Shooter)
        {
            _currShootTime -= Time.deltaTime;
            if (_currShootTime <= 0)
            {
                Shoot();
                SetShootRandomTime();
            }
        }
    }
    public void Hit()
    {
        life--;
        if (life <= 0)
        {
            float rnd = Random.Range(0.0f, 100.0f);
            if (rnd <= _data.ExplodeChancePercentage[0].chanceValue) //UPDATEME
            {
                foreach (Enemy e in FindObjectOfType<EnemiesManager>().GetEnemyAdjascent(transform.position))
                {
                    e.InstantKill();
                }
            }
            Destroy(gameObject);
            DeathPending = true;
            //onKill?.Invoke();
            //Debug.Break();
            FindObjectOfType<GameManager>().OnEnemyKilled();
            FindObjectOfType<EnemiesManager>().OnEnemyKilled();
            FindObjectOfType<Score>().ChangeScore(100);
            FindObjectOfType<ComboSystem>().ComboPlus();
            Instantiate(score, transform.position, transform.rotation);
        }

    }

    public void InstantKill()
    {
        Destroy(gameObject);
        DeathPending = true;
        //onKill?.Invoke();
        //Debug.Break();
        FindObjectOfType<GameManager>().OnEnemyKilled();
        FindObjectOfType<EnemiesManager>().OnEnemyKilled();
        FindObjectOfType<Score>().ChangeScore(100);
        FindObjectOfType<ComboSystem>().ComboPlus();
        Instantiate(score, transform.position, transform.rotation);
    }

    public void DisableComponents()
    {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EnableComponents()
    {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = true;
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //go.transform.Rotate(go.transform.position, 180f);
        //go.tag = "Enemy";
        go.GetComponent<BulletWHP>().m_ownerTag = gameObject.tag;

        go.GetComponent<BulletWHP>().Direction = -Vector2.up;
    }
}
