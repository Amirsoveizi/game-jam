using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _rotationSpeed = 10f;

    private Rigidbody2D _rb;
    private Camera _camera;
    public Animator animator;

    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _moveInput = InputManager.Movement;
        animator.SetBool("IsMoving", _moveInput.sqrMagnitude > 0);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveInput * _movementSpeed;
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        float camDis = _camera.transform.position.y - transform.position.y;
        Vector3 mouse = _camera.ScreenToWorldPoint(new Vector3(InputManager.Direction.x, InputManager.Direction.y, camDis));

        Vector2 direction = (mouse - transform.position).normalized;
        if (direction.sqrMagnitude < 0.001f) return;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float smoothedAngle = Mathf.LerpAngle(_rb.rotation, targetAngle, _rotationSpeed * Time.fixedDeltaTime);

        _rb.MoveRotation(smoothedAngle);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger  " + other.gameObject.name + " (Tag: " + other.tag + ")");

        if (other.CompareTag("River1"))
        {
            Debug.Log("Player enter River");
            Destroy(gameObject);
        }
    }
}
