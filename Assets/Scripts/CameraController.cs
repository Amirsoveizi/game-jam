using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float _damping = 0.25f;
    private Vector3 _vel = Vector3.zero;
    [SerializeField]
    private float _mouseInfluence = 0.5f;
    [SerializeField]
    private float _maxMouseMovement = 1f;

    private void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;      
        Vector3 mouseOffset = (mouseWorldPosition - target.position) * _mouseInfluence;
        mouseOffset = Vector3.ClampMagnitude(mouseOffset, _maxMouseMovement);
        Vector3 targetPosition = target.position + _offset + mouseOffset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _vel, _damping);
    }
}
