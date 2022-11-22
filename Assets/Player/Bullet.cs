using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    [SerializeField]
    float movespeed;

    [HideInInspector]
    public string m_ownerTag;

    private Vector2 _direction = Vector2.up;
    public Vector2 Direction { get => _direction; set { _direction = value; } }
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

            Explode();
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

    void Explode()
    {
        //Sound + FX
        Destroy(gameObject);
    }
}
