using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Animator animator;
    public int _weaponNumber = 1;
    private int _maxWeapons = 4;

    //private float knifeTime = 0f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(1); //knife
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(2); //pistol
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(3); //rifle
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeapon(4); //shotgun

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


         if (Input.GetKeyDown(KeyCode.Mouse0) && _weaponNumber == 1)
         {
            animator.SetTrigger("KnifeUsed");
            StartCoroutine(ResetKnifeTrigger());
         }
    }

    private IEnumerator ResetKnifeTrigger()
    {
    yield return new WaitForSeconds(1f); // Wait for 1 second
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
    }
}
