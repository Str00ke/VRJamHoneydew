using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] float m_movementSpeed;
    [SerializeField] private float dividedSpeed;
    [SerializeField] float _shootCooldown;

    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform m_bulletSpawnPoint;

    bool _canShoot = true;

    [Header("ChargeShot")]
    [SerializeField] private float timeToCharge;
    [SerializeField] private float minimTimeToCharge; // temps avant que l'anim se lance
    private float actualTimeToCharge;
    
    [SerializeField] private float distanceBtwShot;
    
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem charged;
    private bool isCharging;
    private bool isCharging2;
    
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
                Charge();
            }
            else
            {
                //Shoot CD feedback;
            }
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_canShoot) Shoot();
        }

        if (actualTimeToCharge > timeToCharge && !isCharging2)
        {
            isCharging2 = true;
            charge.Stop();
            charged.Play();
        }
        else if (actualTimeToCharge > minimTimeToCharge && !isCharging)
        {
            isCharging = true;
            charge.Play();
        }
        
    }

    void Move(Vector2 Dir)
    {
        if(isCharging || isCharging2) gameObject.transform.Translate(Dir * (m_movementSpeed * dividedSpeed) * Time.deltaTime);
        else gameObject.transform.Translate(Dir * m_movementSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        if (actualTimeToCharge < timeToCharge)
        {
            GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().m_ownerTag = gameObject.tag;
            StartCoroutine(IShootCoolDown());
            //shake.Shake(0.1f, 0.5f, AxisRestriction.XY, 0.15f);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(m_bulletSpawnPoint.position.x - distanceBtwShot + (distanceBtwShot * i), m_bulletSpawnPoint.position.y,
                    m_bulletSpawnPoint.position.z);
                GameObject bullet = Instantiate(m_bulletPrefab, pos, m_bulletSpawnPoint.rotation);
                bullet.GetComponent<Bullet>().m_ownerTag = gameObject.tag;
                StartCoroutine(IShootCoolDown());
               //shake.Shake(0.1f, 0.5f, AxisRestriction.XY, 0.15f);
            }
        }

        actualTimeToCharge = 0;
        isCharging = false;
        isCharging2 = false;
        charge.Stop();
        charged.Stop();
        
    }

    void Charge()
    {
        if (_canShoot) actualTimeToCharge += Time.deltaTime;
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
