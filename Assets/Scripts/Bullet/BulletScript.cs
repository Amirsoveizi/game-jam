using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Audio;

public class BulletScript : MonoBehaviour
{

    public Animator animator;
    private GameObject bulletPF;
    public GameObject pistolBulletPF;
    public GameObject rifleBulletPF;
    public GameObject shotgunBulletPF;
    private bool isKnife;
    private WeaponSwitch weaponSwitch;
    public GameObject muzzle;
    private float timer = 0;
    public float fireRate = 0;

    private AudioClip pistolShot;
    private AudioClip rifleShot;
    private AudioClip shotgunShot;
    private AudioClip knifeSlash;

    public int weaponNum;


    [SerializeField] private float knifeFireRate = 0f;
    [SerializeField] private float pistolFireRate = 0.5f;
    [SerializeField] private float rifleFireRate = 0.2f;
    [SerializeField] private float shotgunFireRate = 1f;




    private Dictionary<int, float> weaponFireRates = new Dictionary<int, float>();

    void Start()
    {
        pistolShot = Resources.Load<AudioClip>("SFX/PistolShot");
        rifleShot = Resources.Load<AudioClip>("SFX/RifleShot");
        shotgunShot = Resources.Load<AudioClip>("SFX/ShotgunShot");
        knifeSlash = Resources.Load<AudioClip>("SFX/KnifeUse");

        weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitch>();
        weaponFireRates.Add(1, knifeFireRate);
        weaponFireRates.Add(2, pistolFireRate);
        weaponFireRates.Add(3, rifleFireRate);
        weaponFireRates.Add(4, shotgunFireRate);
    }
    private bool firstShot = true;

    void Update()
    {
        timer += Time.deltaTime;

            if (weaponSwitch.getWeapon() == 3)
            {
                if (Input.GetKey(KeyCode.Mouse0) && timer >= fireRate)
                {
                    FindWeapon();
                    SpawnBullet();
                    timer = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                if (firstShot)
                {
                    FindWeapon(); // Call findWeapon() to set the correct fireRate
                    if (!isKnife)
                    {
                        SpawnBullet();
                    }
                    else
                    {
                        Knife();
                    }
                    firstShot = false; // Set firstShot to false after the initial shot
                    timer = 0;         // Reset timer after the first shot
                }
                else if (timer >= fireRate) // Normal fire rate check after the first shot
                {
                    FindWeapon();
                    if (!isKnife)
                    {
                        SpawnBullet();
                    }
                    else
                    {
                        Knife();
                    }
                    timer = 0;
                
            }
        }
    }

    void FindWeapon()
    {
        int weaponNumber = weaponSwitch.getWeapon();
        fireRate = weaponFireRates[weaponNumber];

        weaponNum = weaponNumber;

        switch (weaponNumber)
        {
            case 1:
                isKnife = true;
                break;
            case 2:
                isKnife = false;
                bulletPF = pistolBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.02F, 0.634F, 0);
                break;
            case 3:
                isKnife = false;
                bulletPF = rifleBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.04F, 0.73F, 0);
                break;
            case 4:
                isKnife = false;
                bulletPF = shotgunBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.02F, 0.72F, 0);
                break;
        }
    }
    [ContextMenu("Fire")]
    void SpawnBullet()
    {
        if (weaponNum == 2 && Ammo.currentAmmoPistol > 0)
        {
            SoundManager.Instance?.PlaySound(pistolShot, 4f);
            Ammo ammo = FindObjectOfType<Ammo>();
            if (ammo != null)
            {
                ammo.ReduceAmmo(2);
            }
            Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
        }
        else if (weaponNum == 3 && Ammo.currentAmmoRifle > 0)
        {
            SoundManager.Instance?.PlaySound(rifleShot, 4f);
            Ammo ammo = FindObjectOfType<Ammo>();
            if (ammo != null)
            {
                ammo.ReduceAmmo(3);
            }
            Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
        }
        else if (weaponNum == 4 && Ammo.currentAmmoShotgun > 0)
        {
            SoundManager.Instance?.PlaySound(shotgunShot, 4f);
            Ammo ammo = FindObjectOfType<Ammo>();
            if (ammo != null)
            {
                ammo.ReduceAmmo(4);
            }
        }
        else
        {
            Debug.Log("No Ammo");
            return;
        }
        if (bulletPF == shotgunBulletPF)
        {
            Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
            muzzle.transform.Rotate(0, 0, 5);
            Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
            muzzle.transform.Rotate(0, 0, -10);
            Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
            muzzle.transform.Rotate(0, 0, 5);
        }
    }


    void Knife()
    {
        animator.SetTrigger("KnifeUsed");
        StartCoroutine(ResetKnifeTrigger());

        SoundManager.Instance?.PlaySound(knifeSlash, 2f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Health health = hit.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamageP(100);
                }
            }
        }
    }

    private IEnumerator ResetKnifeTrigger()
    {
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("KnifeUsed");
    }

}