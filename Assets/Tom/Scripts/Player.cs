using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField]
    float m_movementSpeed;
    [SerializeField]
    float _shootCooldown;

    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_bulletSpawnPoint;

    bool _canShoot = true;

    GameManager _gameManager;
    #endregion

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            Move(-transform.right);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            Move(transform.right);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_canShoot)
            {
                Debug.Log("Ha!");
                Shoot();
            }
            else
            {
                //Shoot CD feedback;
            }
        }
    }

    void Move(Vector2 Dir)
    {
        gameObject.transform.Translate(Dir * m_movementSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
        bullet.GetComponent<Bullet>().m_ownerTag = gameObject.tag;
        StartCoroutine(IShootCoolDown());
    }

    IEnumerator IShootCoolDown()
    {
        _canShoot = false;

        for(float f = 0; f < _shootCooldown; f += Time.deltaTime)
        {
            yield return null;
        }

        _canShoot = true;
    }

    public void OnHit()
    {
        _gameManager.PlayerHit();
    }
}
