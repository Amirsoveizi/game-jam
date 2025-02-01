using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject bulletPF;
    public GameObject pistolBulletPF;
    public GameObject rifleBulletPF;
    public GameObject shotgunBulletPF;
    private bool isKnife;
    private WeaponSwitch weaponSwitch;
    public GameObject muzzle;
    private float timer = 0;
    public float fireRate = 0;

    [SerializeField] private float knifeFireRate = 1.5f;
    [SerializeField] private float pistolFireRate = 0.5f;
    [SerializeField] private float rifleFireRate = 0.2f;
    [SerializeField] private float shotgunFireRate = 1f;

    private Dictionary<int, float> weaponFireRates = new Dictionary<int, float>();

    void Start()
    {
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

    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
        if (firstShot)
        {
            findWeapon(); // Call findWeapon() to set the correct fireRate
            if (!isKnife)
            {
                spawnBullet();
            }
            else
            {
                knife();
            }
            firstShot = false; // Set firstShot to false after the initial shot
            timer = 0;         // Reset timer after the first shot
        }
        else if (timer >= fireRate) // Normal fire rate check after the first shot
        {
            findWeapon();
            if (!isKnife)
            {
                spawnBullet();
            }
            else
            {
                knife();
            }
            timer = 0;
        }
    }
}

    void findWeapon()
    {
        int weaponNumber = weaponSwitch.getWeapon();
        fireRate = weaponFireRates[weaponNumber];

        switch (weaponNumber)
        {
            case 1:
                isKnife = true;
                break;
            case 2:
                isKnife = false;
                bulletPF = pistolBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.02F, 0.572F, 0);
                break;
            case 3:
                isKnife = false;
                bulletPF = rifleBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.04F, 0.69F, 0);
                break;
            case 4:
                isKnife = false;
                bulletPF = shotgunBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.02F, 0.66F, 0);
                break;
        }
    }

    void spawnBullet()
    {
        Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
    }

    void knife()
    {

    }
}