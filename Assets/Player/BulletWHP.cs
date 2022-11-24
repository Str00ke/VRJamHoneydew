using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWHP : MonoBehaviour
{
    #region Variables
    [SerializeField]
    float movespeed;

    [HideInInspector]
    public string m_ownerTag;
    
    private Vector2 _direction = Vector2.up;
    public Vector2 Direction { get => _direction; set { _direction = value; } }

    [Header("HP")]
    [SerializeField] private float maxHP;
    private float currentHP;

    public float CurrentHp
    {
        get => currentHP;
        set => currentHP = value;
    }
    public bool isPlayer;
    public float timeBeforeDeletion;
    #endregion

    private void Start()
    {
        currentHP = maxHP;
        StartCoroutine(Deletion());
    }

    IEnumerator Deletion()
    {
        yield return new WaitForSeconds(timeBeforeDeletion);
        if(isPlayer) FindObjectOfType<ComboSystem>().resetCombo();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObj = collision.gameObject;

        if (!colObj.CompareTag(m_ownerTag))
        {
            switch (colObj.tag)
            {
                case "Player":
                    colObj.transform.parent.GetComponent<Player>().OnHit();
                    break;

                case "Enemy":
                    colObj.gameObject.GetComponent<Enemy>().Hit();
                    break;
            }

            HPCheck();
        }
    }

    void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(movespeed * Time.deltaTime * _direction);
    }

    void HPCheck()
    {
        currentHP--;
        if(currentHP <= 0) Explode();
    }
    
    void Explode()
    {
        //Sound + FX
        Destroy(gameObject);
    }
}
