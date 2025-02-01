using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRange = 12f;
    [SerializeField] private float shootingRange = 8f;
    [SerializeField] private float stoppingDistance = 4f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float fireRate = 0.8f;
    [SerializeField] private float wallAvoidanceForce = 5f;
    [SerializeField] private float wallAvoidanceRotation = 45f;

    private Rigidbody2D _rb;
    private Transform _target;
    private Vector2 _moveDirection;
    private float nextFireTime = 0f;
    private bool _isRotating = false;
    private bool targetSpotted = false;

    public GameObject bulletPF;
    public GameObject muzzle;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FindClosestTarget();

        if (_target == null) return;

        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        if (targetSpotted)
        {
            if (distanceToTarget > shootingRange)
            {
                MoveTowardsTarget();
            }
            else if (distanceToTarget > stoppingDistance)
            {
                MoveTowardsTarget();
                ShootIfReady();
            }
            else
            {
                StopAndShoot();
            }

            RotateTowardsTarget();
        }
        else if (distanceToTarget <= detectionRange)
        {
            targetSpotted = true;
            MoveTowardsTarget();
            RotateTowardsTarget();
        }
        else
        {
            _rb.velocity = Vector2.zero;
            _isRotating = false;
        }
    }

    private void FindClosestTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject comrade = GameObject.FindGameObjectWithTag("Comrade");

        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        if (player)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closest = player.transform;
                closestDistance = distanceToPlayer;
            }
        }

        if (comrade)
        {
            float distanceToComrade = Vector2.Distance(transform.position, comrade.transform.position);
            if (distanceToComrade < closestDistance)
            {
                closest = comrade.transform;
                closestDistance = distanceToComrade;
            }
        }

        _target = closest;
    }

    private void MoveTowardsTarget()
    {
        if (_target == null) return;
        
        _moveDirection = (_target.position - transform.position).normalized;
        _rb.velocity = _moveDirection * moveSpeed;
    }

    private void StopAndShoot()
    {
        _rb.velocity = Vector2.zero;
        ShootIfReady();
    }

    private void ShootIfReady()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
    }

    private void RotateTowardsTarget()
    {
        if (_target != null && !_isRotating && _target.position != transform.position)
        {
            _isRotating = true;

            Vector2 direction = (_target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = targetAngle;

            _isRotating = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("OurBullet"))
        {
            targetSpotted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 collisionNormal = collision.contacts[0].normal;
            Vector2 reflection = Vector2.Reflect(_moveDirection, collisionNormal);

            _rb.AddForce(reflection * wallAvoidanceForce, ForceMode2D.Impulse);

            RotateAfterWallCollision();
        }
    }

    private void RotateAfterWallCollision()
    {
        if (_target != null)
        {
            Vector2 directionToTarget = _target.position - transform.position;
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            float rotationDifference = Mathf.DeltaAngle(_rb.rotation + 90f, angleToTarget);

            float rotationDirection = Mathf.Sign(rotationDifference);

            _rb.rotation += rotationDirection * wallAvoidanceRotation;

            _moveDirection = (_target.position - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;
        }
    }
}
