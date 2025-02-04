using UnityEngine;
using Audio;

public class BoatAI : MonoBehaviour
{
    [SerializeField] private float radius = 7f;
    [SerializeField] private float speed = 1.6f;
    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private int minCyclesBeforeStop = 1;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float minShootingDuration = 2f;
    [SerializeField] private float maxShootingDuration = 5f;

    private Rigidbody2D _rb;
    private Transform _target;
    private float nextFireTime = 0f;
    private float angle = 0f;
    private int cyclesCompleted = 0;
    private bool isShooting = false;
    private bool isInShootingMode = false;
    private bool halfwayShooting = false;

    public GameObject bulletPF;
    public GameObject muzzle1;
    public GameObject muzzle2;
    private AudioClip enemyShot;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        enemyShot = Resources.Load<AudioClip>("SFX/BoatShot");
        transform.position = GetLemniscatePoint(angle);
    }

    private void Update()
    {
        FindClosestTarget();

        if (isShooting)
        {
            RotateTowardsTarget();
            return;
        }

        angle += speed * Time.deltaTime;
        transform.position = GetLemniscatePoint(angle);

        if (angle >= Mathf.PI && !halfwayShooting)
        {
            halfwayShooting = true;
            if (Random.Range(0, 2) == 0)
            {
                StartShooting();
            }
        }

        if (angle >= Mathf.PI * 2)
        {
            cyclesCompleted++;
            angle = 0f;
            halfwayShooting = false;

            if (cyclesCompleted >= minCyclesBeforeStop)
            {
                if (Random.Range(0, 2) == 0)
                {
                    StartShooting();
                }
                cyclesCompleted = 0;
            }
        }
    }

    private void StartShooting()
    {
        isShooting = true;
        isInShootingMode = true;
        float shootingDuration = Random.Range(minShootingDuration, maxShootingDuration);
        Invoke(nameof(EndShootingMode), shootingDuration);
    }

    private Vector2 GetLemniscatePoint(float angle)
    {
        float x = radius * Mathf.Sqrt(2) * Mathf.Cos(angle) / (1 + Mathf.Pow(Mathf.Sin(angle), 2));
        float y = radius * Mathf.Sqrt(2) * Mathf.Sin(angle) * Mathf.Cos(angle) / (1 + Mathf.Pow(Mathf.Sin(angle), 2));
        return new Vector2(x, y);
    }

    private void FindClosestTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            _target = player.transform;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_target == null) return;

        Vector2 direction = (_target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle += 90f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, targetAngle)) < 1f)
        {
            ShootIfReady();
        }
    }

    private void ShootIfReady()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;

            if (!isInShootingMode)
            {
                Invoke(nameof(ResumeMovement), 1f);
            }
        }
    }

    private void ResumeMovement()
    {
        isShooting = false;
    }

    [ContextMenu("Fire")]
    private void Shoot()
    {
        SoundManager.Instance?.PlaySound(enemyShot, 3f);
        Instantiate(bulletPF, muzzle1.transform.position, muzzle1.transform.rotation);
        Instantiate(bulletPF, muzzle2.transform.position, muzzle2.transform.rotation);
    }

    private void EndShootingMode()
    {
        isInShootingMode = false;
        Invoke(nameof(ResumeMovement), 1f);
    }
}
