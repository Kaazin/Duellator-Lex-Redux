using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 direction;  //the player's direction of movment

    public float speed;        //the speed at which the player should move

    public float jumpSpeed;   //the speed the player should Jump

    Rigidbody rb;       //the rigidbody attached to the player

    Animator anim;      //the animator attached to the player

    Vector3 prevPos, fwd,newPos;    //the previous position of the player, forward vector and new position of the player

    //public Transform enemy;

    BoxCollider boxCol;            //the box collider  attached to the player

    public float gravity = 9.8f;    //the gravity value the player has

    public float fallSpeed;         //how fast the player falls
        
    float distToGround;             //the distane of the raycast from the bottom of the player

    float normSpeed;                //the normal speed of the player

    public float airSpeed;          //the air speed of the player

    float baseGravity;              //the starting gravit of the player

    public enum fallSpd {floaty, normal, fastfaller};

    public fallSpd fallRate;    //enum variable of fallspeed

    public float floaty, normal, fast;  //the three possible fall speeds a character can have

    RaycastHit hit;

    public bool IsGrounded;
    void Awake()
    {
        //assign defualt speed to normSpeed
        normSpeed = speed;

        //assign default gravity to baseGravity
        baseGravity = gravity;

        //set up the references
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        boxCol = GetComponent<BoxCollider>();

        distToGround = boxCol.bounds.extents.y;

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

    void Update()
    {
        //the distance between the player and the enemy
        //Vector3 dir = enemy.position - transform.position;
        //zero out the vector's Y value
        //dir.y = 0;

        //assign the player's rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1);

        //assign newPos and movement
        newPos = transform.position;
        Vector3 movement = (newPos - prevPos);

        // get player input on the horizontal axis and set it to a variable
        float moveH = Input.GetAxis("Horizontal") * speed;



        //if the player is moving back wards play the walking backwards animation
        if (Vector3.Dot(transform.forward, direction) > 0)
            anim.SetBool("WalkB", true);
        // otherwise stop the walking backwards animation
        else
            anim.SetBool("WalkB", false);

        //if the player is moving forward play the walking forward animation
        if (Vector3.Dot(transform.forward, direction) < 0)
            anim.SetBool("WalkF", true);
        //otherwise stop the walking backwards animation
        else
            anim.SetBool("WalkF", false);

        //assign a value to the direction vector
        direction = new Vector3(0, direction.y, moveH);

        //make the player move toward the direction vector relative to his rotation
        rb.velocity = transform.rotation * direction;


        if(IsGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            StartCoroutine(Jump());
        }

        if (!IsGrounded)
            direction.y -= gravity;
        else if (direction.y < 0)
            direction.y = 0;
        if (!IsGrounded)
            anim.SetBool("Jump", false);
    }

    void LateUpdate()
    {
        //assign values to prevpos and fwd
        prevPos = transform.position;
        fwd = transform.forward;
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Floor")
        {
            IsGrounded = true;
            anim.SetBool("Jump", false);
            anim.SetBool("Land", true);
            //anim.SetBool("Land", false);

        }
    }

	void OnCollisionStay(Collision col)
	{
        if(col.gameObject.name == "Floor")
        {
        }
                   
	}
	void OnCollisionExit(Collision col)
    {

        if (col.gameObject.name == "Floor")
        {
            IsGrounded = false;
            anim.SetBool("Land", false);

        }

    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(.5f);
        direction.y = jumpSpeed;

    }



}