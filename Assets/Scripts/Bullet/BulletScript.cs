using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        if (bulletPF != shotgunBulletPF)
            Instantiate(bulletPF, muzzle.transform.position, transform.rotation);
        else
        {
            Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
            muzzle.transform.Rotate(0, 0, 20);
            Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
            muzzle.transform.Rotate(0, 0, -40);
            Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
            muzzle.transform.Rotate(0, 0, 20);
        }
    }

    void Knife()
    {

    }
}