using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float m_movementSpeed;

    [SerializeField]
    GameObject m_bulletPrefab;

    [SerializeField]
    float m_bulletSpawnDistance;

    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(new Vector2(-1, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(new Vector2(1, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Move(Vector2 Dir)
    {
        gameObject.transform.Translate(Dir * m_movementSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(m_bulletPrefab, transform.position + transform.forward * m_bulletSpawnDistance, transform.rotation);
        bullet.GetComponent<Bullet>().m_ownerTag = gameObject.tag;
    }

    public void OnHit()
    {
        _gameManager.PlayerHit();
    }
}
