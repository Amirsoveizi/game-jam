using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveScript : MonoBehaviour
{
    public GameObject BulletEffect; 

    public float moveSpeed = 10f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = transform.up * moveSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        GameObject impactEffect = Instantiate(BulletEffect, transform.position, Quaternion.identity);

        // if (BulletEffect != null)
        // {
        //     Debug.Log("Here");
            
        //     Destroy(impactEffect, impactEffect.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);

        // }
    }

}


