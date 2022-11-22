using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningEffect : MonoBehaviour
{
    LineRenderer _lineRenderer;

    [SerializeField]
    Transform m_leftAnchor;
    [SerializeField]
    Transform m_rightAnchor;

    [SerializeField]
    float m_middlePosBaseHeight;

    /*[Space]

    [SerializeField]
    ParticleSystem _leftFrontSystem;
    [SerializeField]
    ParticleSystem _leftBackSystem;
    [SerializeField]
    ParticleSystem _rightFrontSystem;
    [SerializeField]
    ParticleSystem _rightBackSystem;*/

    private void Start()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();   
    }

    void Update()
    {
        Vector3 middlePos = _lineRenderer.GetPosition(1);
        float anchorsDistance = m_rightAnchor.position.x - m_leftAnchor.position.x;
        middlePos.y = (m_rightAnchor.position.y - m_leftAnchor.position.y);
        Mathf.Lerp(0, m_middlePosBaseHeight, 0);
        middlePos.x = m_leftAnchor.position.x + anchorsDistance / 2;

        _lineRenderer.SetPosition(0, m_leftAnchor.position);
        _lineRenderer.SetPosition(1, middlePos);
        _lineRenderer.SetPosition(2, m_rightAnchor.position);
        
        /*Vector2 rotationVector = middlePos - _leftAnchor.position;
        float rotationValue = Mathf.Atan2(rotationVector.y - Vector2.right.y, rotationVector.x - Vector2.right.x) * 180 / Mathf.PI;

        Vector3 leftSystemScale = new (Vector3.Distance(_leftAnchor.position, middlePos), _leftFrontSystem.shape.scale.y, 0);
        Vector3 leftSystemRot = new Vector3(0, 0, rotationValue);
        Vector3 leftSystemPos = new ((-middlePos.x - _leftAnchor.position.x) / 2, (middlePos.y - _leftAnchor.position.y) / 2, 0);

        ModifyParticleSystemShape(_leftFrontSystem.shape, leftSystemPos, leftSystemRot, leftSystemScale);
        ModifyParticleSystemEmission(_leftFrontSystem.emission, 9f);
        */
    }

    void ModifyParticleSystemShape(ParticleSystem.ShapeModule shape, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        shape.shapeType = ParticleSystemShapeType.Rectangle;
        shape.position = position;
        shape.rotation = rotation;
        shape.scale = scale;
    }

    void ModifyParticleSystemEmission(ParticleSystem.EmissionModule emission, float rateOverTime)
    {
        emission.rateOverTime = rateOverTime;
    }
}
