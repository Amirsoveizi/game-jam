using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float stoppingDistance = 2f;
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D _rb;
    private Transform _player;
    private Vector2 _moveDirection;
    public GameObject bulletPF;
    public GameObject muzzle;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        
            if (_player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > stoppingDistance)
        {
            _moveDirection = (_player.position - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

        RotateTowardsPlayer();
    }
    private void Shoot()
    {
        Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
    }
    private void RotateTowardsPlayer()
    {
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = targetAngle;
        }
    }
}
