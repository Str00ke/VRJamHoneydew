using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;

    private int _lifePoints;

    void Start()
    {
        SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
        spr.sprite = _data.Sprite;
        _lifePoints = _data.LifePoints;
    }

    public void OnHit()
    {
        _lifePoints--;

        if (_lifePoints == 0)
        {
            GameManager.Instance.EnemyKilled();
            gameObject.SetActive(false);
        }
    }
}
