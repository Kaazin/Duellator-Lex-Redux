using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public bool IsGrounded;

    PlayerMovement movement;    //the player movment script attacehd to the paraent gameobject
    Animator anim;  //the animator inside of the movment script
    Vector3 dir;    //the direction vector inside of the movment script

    public float jumpSpeed;   //the speed the player should Jump

    public Vector3 direction;  //the player's direction of movment

    public float speed;        //the speed at which the player should move

    Rigidbody rb;       //the rigidbody attached to the player




    public float gravity = 4.9f;    //the gravity value the player has

    public float fallSpeed;         //how fast the player falls

    float normSpeed;                //the normal speed of the player

    public float airSpeed;          //the air speed of the player

    float baseGravity;              //the starting gravit of the player

    public enum fallSpd { floaty, normal, fastfaller };

    public fallSpd fallRate;    //enum variable of fallspeed

    public float floaty, normal, fast;  //the three possible fall speeds a character can have


    void Awake () 
    {
        movement = GetComponentInParent<PlayerMovement>();
        anim = movement.gameObject.GetComponent<Animator>();

        dir = movement.direction;

        //assign defualt speed to normSpeed
        normSpeed = speed;

        //assign default gravity to baseGravity
        baseGravity = gravity;

        //set up the references
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        //ssign fall rate based on fall speeds
        if (fallRate == fallSpd.floaty)
        {
            gravity *= floaty;
        }
        else if (fallRate == fallSpd.fastfaller)
        {
            gravity *= fast;
        }

        else
            gravity *= normal;


	}
	
	// Update is called once per frame
	void Update () {
        IsGrounded = movement.IsGrounded;

       
        //if he isn't grounded

        if (!IsGrounded)
            dir.y -= gravity;
        else if (dir.y < 0.0f)
            dir.y = 0.0f;

        //if the player is grounded and presses the jump button
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            //make him jump
            Jump();

        //if he isn't grounded

        if (!IsGrounded)
            direction.y -= gravity;
        else if (direction.y < 0.0f)
            direction.y = 0.0f;


	}

    public void Jump()
    {
        //add direction.y to jumpspeed
        dir.y = jumpSpeed;

    }


   


}
