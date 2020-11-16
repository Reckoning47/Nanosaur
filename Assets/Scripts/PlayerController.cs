using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody playerBody;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetAxis("Vertical"))
        {

        }
        playerBody.AddRelativeForce(Vector3.up);
    }
}
