using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody rb;
    float moveSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = rb.velocity;

        if (Input.GetKey(KeyCode.W))
        {
            v.z = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            v.z = -moveSpeed;
        }
        else
        {
            v.z = 0.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            v.x = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            v.x = moveSpeed;
        }
        else
        {
            v.x = 0.0f;
        }
        rb.velocity = v;
    }
}
