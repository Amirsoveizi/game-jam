using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class ComradeAI : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private float minEnemyDetectionRange = 4f;
    [SerializeField] private float maxEnemyDetectionRange = 8f;
    [SerializeField] private float minPlayerDetectionRange = 15f;
    [SerializeField] private float maxPlayerDetectionRange = 25f;
    [SerializeField] private float minStoppingDistance = 2f;
    [SerializeField] private float maxStoppingDistance = 4f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float minFireRate = 0.8f;
    [SerializeField] private float maxFireRate = 1.5f;
    [SerializeField] private float avoidPlayerDistance = 1.5f;

    private Rigidbody2D _rb;
    private Transform _player;
    private Transform _targetEnemy;
    private Vector2 _moveDirection;
    private float nextFireTime = 0f;
    
    private float enemyDetectionRange;
    private float playerDetectionRange;
    private float stoppingDistance;
    private float fireRate;

    public GameObject bulletPF;
    public GameObject muzzle;

    private AudioClip pistolShot;

    private void Start()
    {
        pistolShot = Resources.Load<AudioClip>("SFX/PistolShot");

        enemyDetectionRange = Random.Range(minEnemyDetectionRange, maxEnemyDetectionRange);
        playerDetectionRange = Random.Range(minPlayerDetectionRange, maxPlayerDetectionRange);
        stoppingDistance = Random.Range(minStoppingDistance, maxStoppingDistance);
        fireRate = Random.Range(minFireRate, maxFireRate);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        FindClosestEnemy();

        if (_targetEnemy != null)
        {
            FollowAndShootEnemy();
        }
        else if (_player != null)
        {
            FollowOrAvoidPlayer();
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = enemyDetectionRange;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        _targetEnemy = nearestEnemy;
    }

    private void FollowAndShootEnemy()
    {
        float distanceToEnemy = Vector2.Distance(transform.position, _targetEnemy.position);

        if (distanceToEnemy > stoppingDistance)
        {
            _moveDirection = (_targetEnemy.position - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

        RotateTowards(_targetEnemy.position);

        if (Time.time >= nextFireTime && !IsFacingWall())
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FollowOrAvoidPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer < avoidPlayerDistance)
        {
            _moveDirection = (transform.position - _player.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed * 0.8f; // Slightly slower retreat
        }
        else if (distanceToPlayer <= playerDetectionRange && distanceToPlayer > stoppingDistance)
        {
            // Follow player if within range
            _moveDirection = (_player.position - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;
        }
        else
        {
            // Stop moving when close enough
            _rb.velocity = Vector2.zero;
        }

        RotateTowards(_player.position);
    }

    private void Shoot()
    {
        if (animator.GetBool("IsDead")) return;

        SoundManager.Instance?.PlaySound(pistolShot, 1.5f);
        Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
    }

    private void RotateTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = targetAngle;
    }

    private bool IsFacingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(muzzle.transform.position, muzzle.transform.up, 2f);
        return hit.collider != null && hit.collider.CompareTag("Wall");
    }
}
