using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProjectOnPlane : MonoBehaviour
{
    [Tooltip("Hand controller to raycast from.")]
    public GameObject handController;
    [Tooltip("Use a transparent or hardly visible cursor here.")]
    public GameObject unboundedCursor;
    [Tooltip("Use a more visible cursor here.")]
    public GameObject boundedCursor;
    [Tooltip("Toggle on to constrain cursor in bounds (oriented on plane local axis).")]
    public bool useBounds = true;
    [Tooltip("Only used if \"useBounds\" is enabled.")]
    public Vector2 bounds = new Vector2(10, 10);

    public bool IsHit { get { return isHit; } }
    public Vector3 HitPosition { get { return hitPosition; } }
    public Vector3 BoundedHitPosition { get { return boundedHitPosition; } }

    private bool isHit = false;
    private Vector3 hitPosition = Vector3.zero;
    private Vector3 boundedHitPosition = Vector3.zero;

    private void Start()
    {
        unboundedCursor?.gameObject.SetActive(false);
        boundedCursor?.gameObject.SetActive(false);
    }

    void Update()
    {
        if(handController != null)
        {
            Plane plane = new Plane(transform.up, transform.position);
            Ray ray = new Ray(handController.transform.position, handController.transform.forward);            
            isHit = plane.Raycast(ray, out float distance);
            unboundedCursor?.gameObject.SetActive(isHit);
            boundedCursor?.gameObject.SetActive(isHit);
            if (isHit) {
                hitPosition = handController.transform.position + handController.transform.forward * distance;
                boundedHitPosition = hitPosition;
                if (useBounds)
                {
                    boundedHitPosition = ClampInBounds(boundedHitPosition);
                }
                if (boundedCursor != null) {
                    boundedCursor.transform.position = boundedHitPosition;
                }
                if (unboundedCursor != null) {
                    unboundedCursor.transform.position = hitPosition;
                }
            }
        } else {
            isHit = false;
            unboundedCursor?.gameObject.SetActive(false);
            boundedCursor?.gameObject.SetActive(false);
        }
    }

    Vector3 ClampInBounds(Vector3 position)
    {
        position = transform.worldToLocalMatrix * position;
        position.x = Mathf.Clamp(position.x, -bounds.x * 0.5f, bounds.x * 0.5f);
        position.z = Mathf.Clamp(position.z, -bounds.y * 0.5f, bounds.y * 0.5f);
        return transform.localToWorldMatrix * position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Vector3 cubeSize = useBounds ? new Vector3(bounds.x, 0, bounds.y) :  new Vector3(5,0,5);
        Handles.DrawWireCube(Vector3.zero, cubeSize);
        Handles.matrix = Matrix4x4.identity;
        if (isHit) {
            Gizmos.DrawSphere(boundedHitPosition, 0.05f);
        }
    }
#endif
}
