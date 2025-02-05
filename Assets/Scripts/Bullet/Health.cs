using UnityEngine;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour
{
    public Animator enemyAnimator;
    public Animator comradeAnimaator;

    public int maxHealth = 100;
    private int currentHealth;

    private TextMeshProUGUI healthText;
    private static Score score;

    void Start()
    {
        if (gameObject.tag == "Player") healthText = GameObject.FindGameObjectWithTag("Health").GetComponent<TextMeshProUGUI>();
        currentHealth = maxHealth;
        if (gameObject.tag == "Player") UpdateHealthUI();
        Debug.Log(gameObject.name + " started with " + currentHealth + " health.");
        if(score == null)
        {
           score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth == 0)
        {
            if (gameObject.tag == "Enemy")
            {
                enemyAnimator.SetBool("IsDead", true);
                StartCoroutine(DelayedDestroyEnemy(gameObject, 1.5f));
            }
            else if (gameObject.tag == "Comrade")
            {
                comradeAnimaator.SetBool("IsDead", true);
                StartCoroutine(DelayedDestroyComrade(gameObject, 1.5f));
            }

            if(gameObject.tag == "Boat")
            {
                Die();
            }
        
        }
        else 
        {
            if (gameObject.tag == "Enemy")
            {
                enemyAnimator.SetTrigger("Damaged");
                DelayDamageEnemy(0.0000000000001f);
            }
            else if (gameObject.tag == "Comrade")
            {
                comradeAnimaator.SetTrigger("Damaged");
                DelayDamageComrade(0.0000000000001f);
            }    


                
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

    public void Die()
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
        Ammo.ResetAmmo();
        UpdateHealthUI();
        gameObject.transform.position = CheckPoint.respawnPoint;
        CheckPoint.MakeBossFullHp();
    }
    public void Resethealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private IEnumerator DelayedDestroyEnemy(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
        score.UpdateStatus(5);
    }

    private IEnumerator DelayedDestroyComrade(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
    }

    private IEnumerator DelayDamageEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);

        enemyAnimator.ResetTrigger("Damaged");
    }

    private IEnumerator DelayDamageComrade(float delay)
    {
        yield return new WaitForSeconds(delay);

        comradeAnimaator.ResetTrigger("Damaged");
    }

    public void MakeEnemeyFullHp()
    {
        currentHealth = maxHealth;
    }
}
