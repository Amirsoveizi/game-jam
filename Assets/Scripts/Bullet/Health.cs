using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private TextMeshProUGUI healthText;

    void Start()
    {
        if (gameObject.tag == "Player") healthText = GameObject.FindGameObjectWithTag("Health").GetComponent<TextMeshProUGUI>();
        currentHealth = maxHealth;
        if (gameObject.tag == "Player") UpdateHealthUI();
        Debug.Log(gameObject.name + " started with " + currentHealth + " health.");
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.tag + " Health before damage: " + currentHealth);
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        Debug.Log(gameObject.tag + " Health after damage: " + currentHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void TakeDamageP(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void Die()
    {
        
        if (gameObject.tag == "Player") 
        {
            Respawn();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Respawn()
    {
        currentHealth = 100;
        Ammo.currentAmmoPistol = 50;
        Ammo.currentAmmoRifle = 50;
        Ammo.currentAmmoShotgun = 50;
        Ammo.UpdateَAmmoUI(Ammo.currentAmmoPistol);
        Ammo.UpdateَAmmoUI(Ammo.currentAmmoRifle);
        Ammo.UpdateَAmmoUI(Ammo.currentAmmoShotgun);
        UpdateHealthUI();
        gameObject.transform.position = CheckPoint.respawnPoint;
    }

}
