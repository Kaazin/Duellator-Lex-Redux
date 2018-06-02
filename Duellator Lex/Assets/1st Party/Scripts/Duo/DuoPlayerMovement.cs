using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoPlayerMovement : MonoBehaviour
{
    public Vector3 direction;  //the player's direction of movment

    public float speed;        //the speed at which the player should move

    public float jumpSpeed;   //the speed the player should Jump

    public GameObject[] fighters;

    Rigidbody rb;       //the rigidbody attached to the player

    Animator anim;      //the animator attached to the player

    Vector3 prevPos, fwd, newPos;    //the previous position of the player, forward vector and new position of the player

    //public Transform enemy;

    BoxCollider boxCol;            //the box collider  attached to the player

    public float gravity = 9.8f;    //the gravity value the player has

    public float fallSpeed;         //how fast the player falls

    float distToGround;             //the distane of the raycast from the bottom of the player

    float normSpeed;                //the normal speed of the player

    public float airSpeed;          //the air speed of the player

    float baseGravity;              //the starting gravit of the player

    public enum fallSpd { floaty, normal, fastfaller };

    public fallSpd fallRate;    //enum variable of fallspeed

    public float floaty, normal, fast;  //the three possible fall speeds a character can have

    public bool controlsReversed;

    RaycastHit hit;

    Transform p2;
    public float time = 0.5f;
    public bool IsGrounded;

    FighterSwitch fSwitch;

    void Awake()
    {
        GameStart();
    }

    void Update()
    {
        Main();
    }

    void LateUpdate()
    {
        //assign values to prevpos and fwd
        prevPos = transform.position;
        fwd = transform.forward;
    }

    void OnCollisionEnter(Collision col)
    {
        //if we collide with the floor we should be grounded
        if (col.gameObject.name == "Floor")
        {
            IsGrounded = true;
            anim.SetBool("Jump", false);
            anim.SetBool("Land", true);
            //anim.SetBool("Land", false);

        }
    }

    void OnCollisionExit(Collision col)
    {
        //when we leave the floor wea re no longer grounded
        if (col.gameObject.name == "Floor")
        {
            IsGrounded = false;
            anim.SetBool("Land", false);

        }

    }
    //delay for a time to allow the jump animation to play through untilt hte player leaves the ground
    //once the time has passed jump

    IEnumerator Jump(float time)
    {
        yield return new WaitForSeconds(time);
        direction.y = jumpSpeed;
    }

    void Main()
    {
       //the distance between the player and the enemy
        Vector3 dir = p2.position - transform.position;

        //zero out the vector's Y value
        dir.y = 0;

        //assign the character's rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1);

        //assign newPos and movement
        newPos = transform.position;
        Vector3 movement = (newPos - prevPos);

        // get player input on the horizontal axis and set it to a variable
        float moveH = Input.GetAxis("Horizontal") * speed;



        //if the player is moving back wards play the walking backwards animation
        if (Vector3.Dot(transform.forward, direction) < 0)
        {
            if (fSwitch.fighters[0].activeSelf)
                fSwitch.fighters[0].GetComponentInChildren<Animator>().SetBool("WalkB", true);
            
            if (fSwitch.fighters[1].activeSelf)
                fSwitch.fighters[1].GetComponentInChildren<Animator>().SetBool("WalkB", true);
        }
            
        // otherwise stop the walking backwards animation
        else
        {
            if (fSwitch.fighters[0].activeSelf)
                fSwitch.fighters[0].GetComponentInChildren<Animator>().SetBool("WalkB", false);
            
            if (fSwitch.fighters[1].activeSelf)
                fSwitch.fighters[1].GetComponentInChildren<Animator>().SetBool("WalkB", false);
        }
        //if the player is moving forward play the walking forward animation
        if (Vector3.Dot(transform.forward, direction) > 0)
        {
            if (fSwitch.fighters[0].activeSelf)
                fSwitch.fighters[0].GetComponentInChildren<Animator>().SetBool("WalkF", true);

            if (fSwitch.fighters[1].activeSelf)
                fSwitch.fighters[1].GetComponentInChildren<Animator>().SetBool("WalkF", true);
        }
                else
        {
            if (fSwitch.fighters[0].activeSelf)
                fSwitch.fighters[0].GetComponentInChildren<Animator>().SetBool("WalkF", false);
            
            if (fSwitch.fighters[1].activeSelf)
                fSwitch.fighters[1].GetComponentInChildren<Animator>().SetBool("WalkF", false);
        }

        //assign a value to the direction vector
        direction = new Vector3(moveH, direction.y, 0);

        //make the player move toward the direction vector relative to his rotation
        rb.velocity =  direction;

        if (IsGrounded && Input.GetButtonDown("Jump"))
        {

            if (fSwitch.fighters[0].activeSelf)
                fSwitch.fighters[0].GetComponentInChildren<Animator>().SetBool("Jump", true);

            if (fSwitch.fighters[1].activeSelf)
                fSwitch.fighters[1].GetComponentInChildren<Animator>().SetBool("Jump", true);
                
            StartCoroutine(Jump(time));

        }

        //if the player isn't grouned add gravity and go away from the jump animation
        if (!IsGrounded)
        {
            direction.y -= gravity;
            if (fSwitch.fighters[0].activeSelf)
                fSwitch.fighters[0].GetComponentInChildren<Animator>().SetBool("Jump", false);

            if (fSwitch.fighters[1].activeSelf)
                fSwitch.fighters[1].GetComponentInChildren<Animator>().SetBool("Jump", false);      
        }
        // if the player's vertical direction is less than zero reset him at zero
        else if (direction.y < 0)
            direction.y = 0;
    }

    void GameStart()
    {
        //assign defualt speed to normSpeed
        normSpeed = speed;

        //assign default gravity to baseGravity
        baseGravity = gravity;

        //set up the references
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            rb = GetComponentInParent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();

        boxCol = GetComponent<BoxCollider>();

        distToGround = boxCol.bounds.extents.y;

        p2 = GameObject.FindGameObjectWithTag("P2").transform;

        fSwitch = GetComponent<FighterSwitch>();

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

}