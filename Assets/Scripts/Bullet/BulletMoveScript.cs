using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveScript : MonoBehaviour
{
    //public GameObject BulletEffect; 

    public float moveSpeed;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = transform.up * moveSpeed;
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "EnemyB PF")
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Damaged Player");
            }
            //shayad badam lazem beshe in if ha. tira be enemy haye dige bokhore destroy beshan ya na?!

            /*if (collision.gameObject.tag != "EnemyB PF"
            && collision.gameObject.tag != "Enemy")*/
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag != "Player"
            && collision.gameObject.tag != "PistolB PF"
            && collision.gameObject.tag != "ShotgunB PF"
            && collision.gameObject.tag != "RifleB PF") 
            Destroy(gameObject);
        }
        //GameObject impactEffect = Instantiate(BulletEffect, transform.position, Quaternion.identity);

        // if (BulletEffect != null)
        // {
        //     Debug.Log("Here");
            
        //     Destroy(impactEffect, impactEffect.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);

        // }
    }

}


