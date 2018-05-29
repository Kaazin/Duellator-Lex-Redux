using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 direction;  //the player's direction of movment

    public float speed;        //the speed at which the player should move

    Rigidbody rb;       //the rigidbody attached to the player

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveV = Input.GetAxis("Horizontal") * speed;


        direction = new Vector3(0, direction.y, moveV);

        rb.velocity = transform.rotation * direction;
    }
}
