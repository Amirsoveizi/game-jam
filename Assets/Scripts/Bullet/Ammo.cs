using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Ammo : MonoBehaviour
{
    private int max = 50;
    public static int currentAmmoPistol;

    public static int currentAmmoRifle;

    public static int currentAmmoShotgun;

    private static TextMeshProUGUI KheshabText;

    void Start()
    {
        KheshabText = GameObject.FindGameObjectWithTag("Ammo").GetComponent<TextMeshProUGUI>();
        currentAmmoPistol = max -5 ;
        currentAmmoRifle = max + 15 ;
        currentAmmoShotgun = max;

        UpdateَAmmoUI(currentAmmoPistol);
    }
    public void ReduceAmmo(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 2: // Pistol
                currentAmmoPistol -= 1;
                if (currentAmmoPistol < 0)
                    currentAmmoPistol = 0;
                UpdateَAmmoUI(currentAmmoPistol);
                break;

            case 3: // Rifle
                currentAmmoRifle -= 1;
                if (currentAmmoRifle < 0)
                    currentAmmoRifle = 0;
                UpdateَAmmoUI(currentAmmoRifle);
                break;

            case 4: // Shotgun
                currentAmmoShotgun -= 3;
                if (currentAmmoShotgun < 0)
                    currentAmmoShotgun = 0;
                UpdateَAmmoUI(currentAmmoShotgun);
                break;
        }
    }
    public static void UpdateَAmmoUI(int ammo)
    {
        if (KheshabText != null)
        {
            KheshabText.text = ammo.ToString();
        }
    }
    public static void ResetAmmo()
    {
        currentAmmoPistol = 45;
        currentAmmoRifle = 65;
        currentAmmoShotgun = 50;
        UpdateَAmmoUI(currentAmmoPistol);
        UpdateَAmmoUI(currentAmmoRifle);
        UpdateَAmmoUI(currentAmmoShotgun);
    }
}
