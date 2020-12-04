using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody playerBody;
    float jumpforce = 5f;
    float speed = 5f;
    float moveHorizontal, moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        playerBody.AddForce(movement * speed);
        //if (Input.GetKey(KeyCode.W))
        //{
        //    playerBody.AddForce(Vector3.up * jumpforce, ForceMode.Acceleration);
        //}

        if (Input.GetKey(KeyCode.Space))
        {
            playerBody.AddForce(Vector3.up * jumpforce, ForceMode.Acceleration);
        }

    }
}
