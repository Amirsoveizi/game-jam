using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Animator animator;
    private int _weaponNumber = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(4);
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
