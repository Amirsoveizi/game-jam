using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform player;
    public static Vector3 respawnPoint;
    private Vector3 sp;
    private static Score score;
    public GameObject comrade;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        respawnPoint = player.position;
        sp = transform.position;

        if (score == null)
        {
            score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        }
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
    public static void MakeBossFullHp()
    {
        var tanks = GameObject.FindGameObjectsWithTag("Tank");
        var boats = GameObject.FindGameObjectsWithTag("Boat");

        foreach (var item in boats)
        {
            var hp = item.GetComponent<Health>();
            hp.MakeEnemeyFullHp();
            
        }
        foreach (var item in tanks)
        {
            var hp = item.GetComponent<Health>();
            hp.MakeEnemeyFullHp();
        }
    }
    private void SpawnComrad()
    {
        int comrades = score.score / 12;
        if (comrades > 2)
        {
            comrades = 2;
        }

        for (int i = 0; i < comrades; i++)
        {
            Instantiate(comrade, sp, Quaternion.identity);
        }
    }
}
