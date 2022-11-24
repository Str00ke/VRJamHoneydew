using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnim : MonoBehaviour
{
    [Header("LightShotSetUp")]
    [SerializeField]
    float m_lightShotAnimDuration;
    [SerializeField]
    float m_lightShotShakeStrength;
    [SerializeField]
    AnimationCurve m_lightShotPositionCurve;

    [Space]

    [Header("ChargerdShotBeginSetUp")]
    [SerializeField]
    float m_chargedShotBeginAnimDuration;
    [SerializeField]
    AnimationCurve m_chargedShotBeginPositionCurve;
    [SerializeField]
    AnimationCurve m_chargedShotBeginRotationCurve;

    [Space]

    [Header("ChargerdShotReleaseSetUp")]
    [SerializeField]
    float m_chargedShotReleaseAnimDuration;
    [SerializeField]
    float m_chargedShotReleaseShakeStrength;
    [SerializeField]
    AnimationCurve m_chargedShotReleasePositionCurve;
    [SerializeField]
    AnimationCurve m_chargedShotReleaseRotationCurve;

    [Space]

    [Header("Init")]
    [SerializeField]
    Transform m_leftArm;
    [SerializeField]
    Transform m_rightArm;

    Vector3 _leftArmBasePos;
    Quaternion _leftArmBasRot;

    Vector3 _rightArmBasePos;
    Quaternion _rightArmBaseRot;

    float _leftChargedShotBeginTime;
    float _rightChargedShotBeginTime;
    Coroutine _curLeftChargeBeginCoroutine;
    Coroutine _curRightChargeBeginCoroutine;

    private void Start()
    {
        _leftArmBasePos = m_leftArm.position;
        _leftArmBasRot = m_leftArm.rotation;

        _rightArmBasePos = m_leftArm.position;
        _rightArmBaseRot = m_leftArm.rotation;
    }

    #region Normal Shot
    public void StartLightShotAnim()
    {
        AnimateLightShot(m_leftArm);
        AnimateLightShot(m_rightArm);
    }

    void AnimateLightShot(Transform target)
    {
        target.DOShakePosition(m_lightShotAnimDuration, m_lightShotShakeStrength);
        StartCoroutine(IAnimateOverTime(target, m_lightShotPositionCurve, m_lightShotAnimDuration));
    }
    #endregion

    #region Charged Shot Begin
    public void StartChargedShotBegin()
    {
        _curLeftChargeBeginCoroutine = StartCoroutine(LeftChargedShotBegin(m_leftArm, m_chargedShotBeginPositionCurve, m_chargedShotBeginRotationCurve, m_chargedShotBeginAnimDuration));
        _curRightChargeBeginCoroutine = StartCoroutine(RightChargedShotBegin(m_rightArm, m_chargedShotBeginPositionCurve, m_chargedShotBeginRotationCurve, m_chargedShotBeginAnimDuration));
    }

    public void StartChargedShotBeginCancel()
    {
        StopCoroutine(_curLeftChargeBeginCoroutine);
        StopCoroutine(_curRightChargeBeginCoroutine);

        StartCoroutine(LeftChargedShotBeginCancel(m_leftArm, m_chargedShotBeginPositionCurve, m_chargedShotBeginRotationCurve, m_chargedShotBeginAnimDuration));
        StartCoroutine(RightChargedShotBeginCancel(m_rightArm, m_chargedShotBeginPositionCurve, m_chargedShotBeginRotationCurve, m_chargedShotBeginAnimDuration));
    }

    IEnumerator LeftChargedShotBegin(Transform target, AnimationCurve positionCurve, AnimationCurve rotationCurve, float duration)
    {
        Vector3 basePos = target.position;
        Vector3 baseRot = target.rotation.eulerAngles;
        while (_leftChargedShotBeginTime < duration)
        {
            float progress = _leftChargedShotBeginTime / duration;
            target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(progress));
            target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(progress)));
            _leftChargedShotBeginTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(1));
        target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(1)));
        _leftChargedShotBeginTime = 0;
    }

    IEnumerator RightChargedShotBegin(Transform target, AnimationCurve positionCurve, AnimationCurve rotationCurve, float duration)
    {
        Vector3 basePos = target.position;
        Vector3 baseRot = target.rotation.eulerAngles;
        while (_rightChargedShotBeginTime < duration)
        {
            float progress = _rightChargedShotBeginTime / duration;
            target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(progress));
            target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(progress)));
            _rightChargedShotBeginTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(1));
        target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(1)));
        _rightChargedShotBeginTime = 0;
    }

    IEnumerator LeftChargedShotBeginCancel(Transform target, AnimationCurve positionCurve, AnimationCurve rotationCurve, float duration)
    {
        Vector3 basePos = target.position;
        Vector3 baseRot = target.rotation.eulerAngles;
        while (_leftChargedShotBeginTime > 0)
        {
            float progress = _leftChargedShotBeginTime / duration;
            target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(progress));
            target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(progress)));
            _leftChargedShotBeginTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(0));
        target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(0)));
    }

    IEnumerator RightChargedShotBeginCancel(Transform target, AnimationCurve positionCurve, AnimationCurve rotationCurve, float duration)
    {
        Vector3 basePos = target.position;
        Vector3 baseRot = target.rotation.eulerAngles;
        while (_rightChargedShotBeginTime > 0)
        {
            float progress = _rightChargedShotBeginTime / duration;
            target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(progress));
            target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(progress)));
            _rightChargedShotBeginTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(0));
        target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(0)));
    }
    #endregion

    #region Charged Shot Release
    public void StartChargedShotReleaseAnim()
    {
        AnimateChargedShotRelease(m_leftArm);
        AnimateChargedShotRelease(m_rightArm);
    }

    void AnimateChargedShotRelease(Transform target)
    {
        target.DOShakePosition(m_chargedShotReleaseAnimDuration,m_chargedShotReleaseShakeStrength);
        StartCoroutine(IAnimateOverTime(target, m_chargedShotReleasePositionCurve, m_chargedShotReleaseRotationCurve, m_chargedShotReleaseAnimDuration));
    }

    IEnumerator IAnimateOverTime(Transform target, AnimationCurve positionCurve,float duration)
    {
        float curTime = 0;
        Vector3 basePos = target.position;
        while(curTime < duration)
        {
            float progress = curTime / duration;
            target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(progress));
            curTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator IAnimateOverTime(Transform target, AnimationCurve positionCurve, AnimationCurve rotationCurve, float duration)
    {
        float curTime = 0;
        Vector3 basePos = target.position;
        Vector3 baseRot = target.rotation.eulerAngles;
        while (curTime < duration)
        {
            float progress = curTime / duration;
            target.position = new Vector3(basePos.x, basePos.y + positionCurve.Evaluate(progress));
            target.rotation = Quaternion.Euler(baseRot + new Vector3(0, 0, rotationCurve.Evaluate(progress))); 
            curTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion
}
