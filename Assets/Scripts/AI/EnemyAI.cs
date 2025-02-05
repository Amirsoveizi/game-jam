using UnityEngine;
using Audio;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private float minDetectionRange = 5f;
    [SerializeField] private float maxDetectionRange = 15f;
    [SerializeField] private float minShootingRange = 5f;
    [SerializeField] private float maxShootingRange = 10f;
    [SerializeField] private float minStoppingDistance = 2f;
    [SerializeField] private float maxStoppingDistance = 5f; 
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float minFireRate = 0.5f; 
    [SerializeField] private float maxFireRate = 1.5f;
    [SerializeField] private float wallAvoidanceForce = 5f;
    [SerializeField] private float wallAvoidanceRotation = 45f;
    [SerializeField] private float avoidanceRadius = 3f;
    [SerializeField] private float avoidanceStrength = 0.5f;
    [SerializeField] private LayerMask obstacleMask; 

    private Rigidbody2D _rb;
    private Transform _target;
    private Vector2 _moveDirection;
    private float nextFireTime = 0f;
    private bool targetSpotted = false;
    private float stoppingDistance;
    private float fireRate;
    private float detectionRange;
    private float shootingRange;

    public GameObject bulletPF;
    public GameObject muzzle;

    private AudioClip enemyShot;

    private void Start()
    {
        enemyShot = Resources.Load<AudioClip>("SFX/EnemyShot");
        stoppingDistance = Random.Range(minStoppingDistance, maxStoppingDistance);
        detectionRange = Random.Range(minDetectionRange, maxDetectionRange);
        shootingRange = Random.Range(minShootingRange, maxShootingRange);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        fireRate = Random.Range(minFireRate, maxFireRate);
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

        Vector2 targetPosition = _target.position;
        Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius); 
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit.gameObject != gameObject)
            {
                Vector2 avoidanceDirection = ((Vector2)transform.position - (Vector2)hit.transform.position).normalized;
                directionToTarget += avoidanceDirection * avoidanceStrength;
            }
        }

        _moveDirection = directionToTarget.normalized;
        _rb.velocity = _moveDirection * moveSpeed;
    }

    private void StopAndShoot()
    {
        _rb.velocity = Vector2.zero;
        ShootIfReady();
    }

    private void ShootIfReady()
    {
        if (Time.time >= nextFireTime && CanSeeTarget(_target.position))
        {
            Shoot();
            fireRate = Random.Range(minFireRate, maxFireRate);
            nextFireTime = Time.time + fireRate;
        }
    }

    [ContextMenu("Fire")]
    private void Shoot()
    {
        if (animator.GetBool("IsDead")) return;

        SoundManager.Instance?.PlaySound(enemyShot, 1.5f);
        Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
    }

    private void RotateTowardsTarget()
    {
        if (_target != null)
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = targetAngle;
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
    private bool CanSeeTarget(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootingRange, obstacleMask);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            return false;
        }
        return true;
    }
}
