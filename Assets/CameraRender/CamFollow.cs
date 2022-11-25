using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    Transform m_camToFollow;
    [SerializeField]
    float m_multiplier;

    Transform _targetBaseTransform;

    private void Start()
    {
        _targetBaseTransform = m_camToFollow.transform;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(m_camToFollow.rotation, _targetBaseTransform.rotation, m_multiplier);
    }
}
