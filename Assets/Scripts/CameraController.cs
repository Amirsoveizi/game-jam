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


    private void Update()
    {
        Vector3 targetPosition = target.position + _offset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _vel, _damping);
    }


}
