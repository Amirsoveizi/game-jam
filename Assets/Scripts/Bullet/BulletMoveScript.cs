using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveScript : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D _rb;
    public int damageE = 10;
    public int damageP = 1;
    // public int maxHealth=100;
    // private int currentHealth;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.up * moveSpeed;
        Destroy(gameObject, 2f);
        damageE = Random.Range(3, 7);
        if (tag == "ShotgunB PF")
        {
            damageP = 13;
        }
        else if (tag == "PistolB PF")
        {
            damageP = 15;
        }
        else if (tag == "RifleB PF")
        {
            damageP = 6;
        }
        else if (tag == "ComradeB PF")
        {
            damageP = 10;
        }
        else if (tag == "TankB PF")
        {
            damageE = 30;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (tag == "EnemyB PF" || tag == "TankB PF")
        {
            if (collision.gameObject.tag == "Player")
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamageP(damageE);
                }
            }
            else if (collision.gameObject.tag == "Comrade")
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damageE);
                }
            }
            Destroy(gameObject);
        }
        else if (tag == "PistolB PF" || tag == "RifleB PF" || tag == "ComradeB PF")
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Tank")
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damageP);
                }
            }
            if (collision.gameObject.tag != "EnemyB PF") Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag != "ShotgunB PF")
            {
                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Tank")
                {
                    Health health = collision.gameObject.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(damageP);
                    }
                }
                if (collision.gameObject.tag != "EnemyB PF") Destroy(gameObject);
            }

        }
    }
}