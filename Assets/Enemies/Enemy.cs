using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;

    public static System.Action onKill;

    private int life;
    void Awake()
    {
        if (!TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
        {
            SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
            spr.sprite = _data.Sprite;
        }

        life = _data.LifePoints;
    }
    void Start()
    {
        
        
    }

    /*
    void OnTriggerEnter2D(Collider2D col)
    {
        life--;
        if (life <= 0)
            Destroy(gameObject);
    }
    */

    public void Hit()
    {
        life--;
        if (life <= 0)
        {
            Destroy(gameObject);
            onKill?.Invoke();
        }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
