using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;
    void Start()
    {
        SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
        spr.sprite = _data.Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
