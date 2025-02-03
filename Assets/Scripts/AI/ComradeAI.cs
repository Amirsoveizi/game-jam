using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class ComradeAI : MonoBehaviour
{
    [SerializeField] private float enemyDetectionRange = 5f;
    [SerializeField] private float playerDetectionRange = 20f;
    [SerializeField] private float stoppingDistance = 2.5f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float fireRate = 1f;

    private Rigidbody2D _rb;
    private Transform _player;
    private Transform _targetEnemy;
    private Vector2 _moveDirection;
    private float nextFireTime = 0f;
    
    public GameObject bulletPF;
    public GameObject muzzle;

    private AudioClip pistolShot;

    private void Start() {
            pistolShot = Resources.Load<AudioClip>("SFX/PistolShot");
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
            FollowPlayer();
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

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= playerDetectionRange && distanceToPlayer > stoppingDistance)
        {
            _moveDirection = (_player.position - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

        RotateTowards(_player.position);
    }

    private void Shoot()
    {
        SoundManager.Instance?.PlaySound(pistolShot, 1.5f);
        Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
    }

    private void RotateTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = targetAngle;
    }
}
