using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;

    void Awake()
    {
        if (!TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
        {
            SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
            spr.sprite = _data.Sprite;
        }
    }
    void Start()
    {
        
        
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
