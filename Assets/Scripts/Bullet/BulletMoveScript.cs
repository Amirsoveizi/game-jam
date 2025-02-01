using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = transform.up * moveSpeed; // Moves forward in the direction the bullet was instantiated
        Destroy(gameObject, 3f); // Destroy bullet after 3 seconds
    }
}
