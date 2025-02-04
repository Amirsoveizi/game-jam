using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform player;
    public static Vector3 respawnPoint;
    private static Score score;
    public GameObject comrade;

    private void Awake()
    {
        if(score == null)
        {
            score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        respawnPoint = player.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawnPoint = transform.position;

            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.Resethealth();
            }
            SpawnComrad();
            Ammo.ResetAmmo();
            Destroy(this.gameObject);
        }
    }

    private void SpawnComrad()
    {
        int comrades = score.score / 5;
        if (comrades > 3)
        {
            comrades = 3;
        }

        for (int i = 0; i < comrades; i++)
        {
            Instantiate(comrade,player);
        }
    }
}
