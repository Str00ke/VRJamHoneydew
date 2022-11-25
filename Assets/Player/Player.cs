using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.InputSystem.CommonUsages;

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

    [Header("Inputs")]
    [Header("Keyboard")]
    [SerializeField] private InputActionProperty moveKey;
    [SerializeField] private InputActionProperty shootKey;

    [Header("Joystick")]
    [SerializeField] private InputActionProperty moveStick;
    [SerializeField] private InputActionProperty shootTrigger;

    [Header("HMD")]
    [SerializeField] private InputActionProperty moveOculus;
    [SerializeField] private InputActionProperty shootOculus;
    [SerializeField] private InputAction test;
    #endregion

    void Awake()
    {
        moveKey.action.performed += MoveInput;
        moveStick.action.performed += MoveInput;
        moveOculus.action.performed += MoveInput;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        cam = Camera.main;
    }

    void OnDisable()
    {
        moveKey.action.performed -= MoveInput;
        moveStick.action.performed -= MoveInput;
        moveOculus.action.performed -= MoveInput;
    }

    void Update()
    {

        //UnityEngine.XR.InputDevice vecKey = InputDevices.GetDeviceAtXRNode(test);
        //Vector2 t;
        // c;
        //vecKey.TryGetFeatureValue(CommonUsages.SecondaryTrigger, out c)
        float vecKey = moveOculus.action.ReadValue<float>();
        Debug.Log(vecKey);
        if (shootKey.action.WasPressedThisFrame() | shootTrigger.action.WasPressedThisFrame() | shootOculus.action.WasPressedThisFrame())
        {
            Debug.Log("Press");

            if (_canShoot)
            {
                Charge();
            }
            else
            {
                //Shoot CD feedback;
            }
        }else if (shootKey.action.WasReleasedThisFrame() | shootTrigger.action.WasReleasedThisFrame() | shootOculus.action.WasReleasedThisFrame())
        {
            if (_canShoot) Preshoot();
        }

        if (actualTimeToCharge > timeToCharge && !isCharging2)
        {
            isCharging2 = true;
            charge.Stop();
            if(PlayerPrefs.GetInt("Effect0") == 1) charged.Play();
        }
        else if (actualTimeToCharge > minimTimeToCharge && !isCharging)
        {
            isCharging = true;
            if(PlayerPrefs.GetInt("Effect0") == 1) charge.Play();
        }
        
    }

    void MoveInput(InputAction.CallbackContext ctx)
    {
        float vecKey = moveKey.action.ReadValue<float>();
        float vecJoy = moveStick.action.ReadValue<float>();
        float vecVR = moveOculus.action.ReadValue<float>();

        if (vecKey != 0)
            Move(vecKey);
        else if (vecVR != 0)
            Move(vecJoy);
    }

    void Move(float Dir)
    {
        if (Dir == -1)
        {
            anim.SetBool("IsRight", false);
            anim.SetBool("IsLeft", true);
            anim.SetBool("IsIdle", false);
        }
        else if (Dir == 1)
        {
            anim.SetBool("IsRight", true);
            anim.SetBool("IsLeft", false);
            anim.SetBool("IsIdle", false);
        }
        else if (Dir == 0)
        {
            anim.SetBool("IsRight", false);
            anim.SetBool("IsLeft", false);
            anim.SetBool("IsIdle", true);
        }

        if (isCharging || isCharging2) gameObject.transform.Translate(new Vector2(Dir, 0) * (m_movementSpeed * dividedSpeed) * Time.deltaTime);
        else gameObject.transform.Translate(new Vector2(Dir, 0)* m_movementSpeed * Time.deltaTime);
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
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(m_bulletSpawnPoint.position.x - distanceBtwShot + (distanceBtwShot * i), m_bulletSpawnPoint.position.y,
                    m_bulletSpawnPoint.position.z);
                GameObject bullet = Instantiate(_bullet, pos, m_bulletSpawnPoint.rotation);
                bullet.GetComponent<BulletWHP>().m_ownerTag = gameObject.tag;
                StartCoroutine(IShootCoolDown());
               //shake.Shake(0.1f, 0.5f, AxisRestriction.XY, 0.15f);
            }
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
