using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Animator animator;
    private int _weaponNumber = 1;
    private int _maxWeapons = 4;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeapon(4);

        // Mouse scroll wheel selection
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

    private void ChangeWeapon(int weaponID)
    {
        if (_weaponNumber != weaponID)
        {
            _weaponNumber = weaponID;
            animator.SetInteger("WeaponNumber", _weaponNumber);
        }
    }
}
