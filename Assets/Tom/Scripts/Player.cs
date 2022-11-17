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

    private Camera cam;
    private ScreenShake shake;
    #endregion

    void Start()
    {
        _gameManager = GameManager.Instance;
        cam = Camera.main;
        shake = cam.transform.GetComponent<ScreenShake>();
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
        shake.Shake(0.1f, 0.5f, AxisRestriction.XY, 0.15f);
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
