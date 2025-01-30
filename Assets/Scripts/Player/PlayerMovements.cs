using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5f;

    private Rigidbody2D _rb;
    private Camera _camera;
    public Animator animator;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsMoving", InputManager.Movement.sqrMagnitude > 0);
        _rb.velocity = InputManager.Movement * _movementSpeed;
        _rb.rotation = LookAtMouse();
    }


    private float LookAtMouse()
    {
        float camDis = _camera.transform.position.y - transform.position.y;
        Vector3 mouse = _camera.ScreenToWorldPoint(new Vector3(InputManager.Direction.x, InputManager.Direction.y, camDis));
        float angleRad = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        return (180 / Mathf.PI) * angleRad - 90; 
    }

}
