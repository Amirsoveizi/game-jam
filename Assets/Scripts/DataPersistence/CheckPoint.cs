using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform player;
    public static Vector3 respawnPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        respawnPoint = player.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            respawnPoint = transform.position;
            Destroy(this.gameObject);
        }
    }
}
