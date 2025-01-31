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
    public float fireRate = 5;
    // Start is called before the first frame update
    void Start()
    {
        weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            findWeapon();
            /*if (timer < fireRate)
            {
                timer += Time.deltaTime;
            }
            else
            {*/
                if (!isKnife)
                {
                    spawnBullet();
                }
                else
                {
                    knife();
                }
                timer = 0;
            //}
        }
    }
    
    void findWeapon()
    {
        int weaponNumber = weaponSwitch.getWeapon();
        switch (weaponNumber)
        {
            case 1:
                isKnife = true;
                fireRate = 1.5F;
                break;
            case 2:
                isKnife = false;
                fireRate = 1.5F;
                bulletPF = pistolBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.02F, 0.572F, 0);
                break;
            case 3:
                isKnife = false;
                fireRate = 1;
                bulletPF = rifleBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.04F, 0.69F, 0);
                break;
            case 4:
                isKnife = false;
                fireRate = 2;
                bulletPF = shotgunBulletPF;
                muzzle.transform.localPosition = new Vector3(-0.02F, 0.66F, 0);
                break;
        }
    }
    [ContextMenu("Fire")]
    void spawnBullet()
    {
        Instantiate(bulletPF, new Vector3(muzzle.transform.position.x, muzzle.transform.position.y,  0), transform.rotation);
    }
    [ContextMenu("Knife")]
    void knife()
    {

    }
}
