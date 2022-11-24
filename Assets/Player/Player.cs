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
    [SerializeField] GameObject m_bulletPrefabWParticules;
    [SerializeField] Transform m_bulletSpawnPoint;

    bool _canShoot = true;

    [Header("ChargeShot")]
    [SerializeField] private float timeToCharge;
    [SerializeField] private float minimTimeToCharge; // temps avant que l'anim se lance
    private float actualTimeToCharge;
    
    [SerializeField] private float distanceBtwShot;
    
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem charged;
    //[SerializeField] private ParticleSystem releaseShot;
    private bool isCharging;
    private bool isCharging2;
    
    GameManager _gameManager;

    [Header("Animation")] 
    [SerializeField] private Animator anim; 

    private Camera cam;
    [SerializeField] private SoundTransmitter st;
    #endregion

    void Start()
    {
        _gameManager = GameManager.Instance;
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            Move(-transform.right);
            anim.SetBool("IsRight", false);
            anim.SetBool("IsLeft", true);
            anim.SetBool("IsIdle", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            Move(transform.right);
            anim.SetBool("IsRight", true);
            anim.SetBool("IsLeft", false);
            anim.SetBool("IsIdle", false);
        }
        if(!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsRight",false);
            anim.SetBool("IsLeft", false);
            anim.SetBool("IsIdle", true);
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
            if (_canShoot) Preshoot();
        }

        if (actualTimeToCharge > timeToCharge && !isCharging2)
        {
            isCharging2 = true;
            charge.Stop();
            if(PlayerPrefs.GetInt("Effect0") == 1) charged.Play();
            st.Play("Charge0");
        }
        else if (actualTimeToCharge > minimTimeToCharge && !isCharging)
        {
            isCharging = true;
            if(PlayerPrefs.GetInt("Effect0") == 1) charge.Play();
            st.Play("Charge1");
        }
        
    }

    void Move(Vector2 Dir)
    {
        if(isCharging || isCharging2) gameObject.transform.Translate(Dir * (m_movementSpeed * dividedSpeed) * Time.deltaTime);
        else gameObject.transform.Translate(Dir * m_movementSpeed * Time.deltaTime);
    }

    void Preshoot()
    {
        if(PlayerPrefs.GetInt("Effect0") == 1) Shoot(m_bulletPrefabWParticules);
        else Shoot(m_bulletPrefab);
    }

    void Shoot(GameObject _bullet)
    {
        if (actualTimeToCharge < timeToCharge)
        {
            GameObject bullet = Instantiate(_bullet, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
            bullet.GetComponent<BulletWHP>().m_ownerTag = gameObject.tag;
            StartCoroutine(IShootCoolDown());
            //shake.Shake(0.1f, 0.5f, AxisRestriction.XY, 0.15f);
        }
        else
        {
            Vector3 pos = new Vector3(m_bulletSpawnPoint.position.x - distanceBtwShot + distanceBtwShot, m_bulletSpawnPoint.position.y,
                    m_bulletSpawnPoint.position.z);
            GameObject bullet = Instantiate(_bullet, pos, m_bulletSpawnPoint.rotation);
            bullet.GetComponent<BulletWHP>().m_ownerTag = gameObject.tag;
            bullet.GetComponent<BulletWHP>().CurrentHp = 2;
            StartCoroutine(IShootCoolDown());
           //shake.Shake(0.1f, 0.5f, AxisRestriction.XY, 0.15f);
           st.Play("Charge2");
        }

        actualTimeToCharge = 0;
        isCharging = false;
        isCharging2 = false;
        charge.Stop();
        charged.Stop();
        //releaseShot.Play();
        
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
