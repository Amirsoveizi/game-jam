using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using TMPro;
public class WeaponSwitch : MonoBehaviour
{
    public Animator animator;
    public int _weaponNumber = 0;
    private int _maxWeapons = 4;

    private AudioClip pistolEquip;
    private AudioClip rifleEquip;
    private AudioClip shotgunEquip;
    private AudioClip knifeEquip;
    private TextMeshProUGUI KheshabText;

    private void Start()
    {
        pistolEquip = Resources.Load<AudioClip>("SFX/PistolEquip");
        rifleEquip = Resources.Load<AudioClip>("SFX/RifleEquip");
        shotgunEquip = Resources.Load<AudioClip>("SFX/ShotgunEquip");
        knifeEquip = Resources.Load<AudioClip>("SFX/KnifeEquip");
        KheshabText = GameObject.FindGameObjectWithTag("Ammo").GetComponent<TextMeshProUGUI>();
        KheshabText.text = "-";
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(3);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(4);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeapon(1);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            ChangeWeapon(_weaponNumber + 1 > _maxWeapons ? 1 : _weaponNumber + 1);
        }
        else if (scroll < 0f)
        {
            ChangeWeapon(_weaponNumber - 1 < 1 ? _maxWeapons : _weaponNumber - 1);
        }
    }

    private IEnumerator ResetKnifeTrigger()
    {
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("KnifeUsed");
    }
    public int getWeapon()
    {
        return _weaponNumber;
    }
    private void ChangeWeapon(int weaponID)
    {
        if (_weaponNumber != weaponID)
        {
            _weaponNumber = weaponID;
            animator.SetInteger("WeaponNumber", _weaponNumber);
        }


        if (_weaponNumber == 2)
        {
            SoundManager.Instance?.PlaySound(pistolEquip, 4f);
            KheshabText.text = Ammo.currentAmmoPistol.ToString();
        }
        else if (_weaponNumber == 3)
        {
            SoundManager.Instance?.PlaySound(rifleEquip, 3f);
            KheshabText.text = Ammo.currentAmmoRifle.ToString();
        }
        else if (_weaponNumber == 4)
        {
            SoundManager.Instance?.PlaySound(shotgunEquip, 4f);
            KheshabText.text = Ammo.currentAmmoShotgun.ToString();
        }
        else if (_weaponNumber == 1)
        {
            KheshabText.text = "_";
            SoundManager.Instance?.PlaySound(knifeEquip, 4f);
        }
        else
        {
            KheshabText.text = "-";
        }

    }
}
