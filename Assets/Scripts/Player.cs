using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string moveAxisName="Vertical";
    string turnAxisName="Horizontal";
    float moveAxsis;
    float turnAxis;
    float moveSpeed=5f;
    float turnSpeed=240f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

   
    void Update()
    {  
        moveAxsis = Input.GetAxis(moveAxisName);
        turnAxis = Input.GetAxis(turnAxisName);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<ShootBehavior>().Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * moveAxsis * moveSpeed * Time.deltaTime );
        Quaternion rotation = Quaternion.Euler(0, turnAxis * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(transform.rotation * rotation);
        
    }
}

