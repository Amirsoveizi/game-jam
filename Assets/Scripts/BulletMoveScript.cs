using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //x = rcost
    //y = rsist
    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x + moveSpeed * Time.deltaTime * Mathf.Cos(transform.localRotation.eulerAngles.z + 90);
        float y = transform.position.y + moveSpeed * Time.deltaTime * Mathf.Sin(transform.localRotation.eulerAngles.z + 90);
        Debug.Log("sin: " + Mathf.Sin(transform.localRotation.eulerAngles.z + 90));
        Debug.Log("x: " + x);
        Debug.Log("y: " + y);
        transform.position = new Vector3(x, y, 0);
    }
}
