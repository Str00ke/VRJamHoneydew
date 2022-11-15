using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public string m_ownerTag;

    float movespeed;

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
        transform.Translate(transform.position + movespeed * Time.deltaTime * transform.forward);
    }

    void Explode()
    {
        //Sound + FX
        Destroy(gameObject);
    }
}
