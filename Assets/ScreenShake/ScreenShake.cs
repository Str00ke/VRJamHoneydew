using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AxisRestriction
{
    X_ONLY,
    Y_ONLY,
    XY
    //Maybe add Z?
}

public class ScreenShake : MonoBehaviour
{
    private Camera cam;

    private Vector3 _basePos;

    private float _currTimer;

    private bool _canShake = true;

    void Awake()
    {
        _basePos = transform.position;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (_currTimer > 0)
        {
            _currTimer -= Time.deltaTime;
            if (_currTimer <= 0) CamReset();
        }
    }

    public void Shake(float intensityMin = 0.1f, float intensityMax = 0.1f, AxisRestriction restriction = AxisRestriction.XY, float duration = 0.01f)
    {
        float intensity = Random.Range(intensityMin, intensityMax);
        //x = r × cos(?) y = r × sin(?);
        float delta = Random.Range(0, 360);
        Vector3 pos = new Vector3(intensity * Mathf.Cos(delta), intensity * Mathf.Sin(delta), _basePos.z);
        transform.position = pos;
        _currTimer = duration;
        _canShake = false;

    }

    void CamReset()
    {
        transform.position = _basePos;
        _canShake = true;
    }
}
