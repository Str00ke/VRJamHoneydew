using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    [SerializeField]
    float movespeed;

    [HideInInspector]
    public string m_ownerTag;
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
                    //Enemy Hit Func
                    break;
            }

            Explode();
        }
    }

    void Update()
    {
        transform.Translate(movespeed * Time.deltaTime * transform.up);
    }

    void Explode()
    {
        //Sound + FX
        Destroy(gameObject);
    }
}
