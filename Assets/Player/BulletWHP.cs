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

    [Header("HP")]
    [SerializeField] private float maxHP;
    private float currentHP;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObj = collision.gameObject;

        if (!colObj.CompareTag(m_ownerTag))
        {
            switch (colObj.tag)
            {
                case "Player":
                    colObj.GetComponent<Player>().OnHit();
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
        transform.Translate(movespeed * Time.deltaTime * transform.up);
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
