using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool attkHit;
    public Vector3 direction;  //the player's direction of movment

    public float stopDistance;  //the distance from the player that the AI should stop

    public float speed;        //the speed at which the player should move

    public float jumpSpeed;   //the speed the player should Jump

    public float attkRNG,timeRNG;  //the coice of attack and the time between choosing an attack
    float directionTimeRNG; //the time between switching directions
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

    public float time = 0.5f;
    public bool IsGrounded;
    public bool jump;
    Transform p1;

    //hitbox data//
    public BoxCollider[] legHitboxes_L;
    public BoxCollider[] legHitboxes_R;
    public BoxCollider[] armHitboxes_L;
    public BoxCollider[] armHitboxes_R;
    //hitbox data//

    public bool attk;
    PlayerHealth health;
    public int attackTimes;

    float retreatTimer;
    public float retreatTime;

    public float minMargin, maxMargin;  //the minimum and maximum offset margins for the ai and player to determine if the ai should be playing neutral
   
    void Awake()
      {
        //setup initilization stuff
        GameStart();
     }

    void Update()
    {

        if (p1.GetComponent<isDuo>().Duo)
        {
            if (p1.GetComponent<DuoPlayerHealth>().currentHealth > 0)
                Main();
            else
            {
                anim.SetTrigger("Won");
                anim.SetLayerWeight(1, 1);

                GetComponent<EnemyAI>().enabled = false;
            }
        }
       
        else if (!p1.GetComponent<isDuo>().Duo)
        {
            if (p1.GetComponent<PlayerHealth>().currentHealth > 0)
                Main();
            else
            {
                anim.SetTrigger("Won");
                anim.SetLayerWeight(1, 1);


                GetComponent<EnemyAI>().enabled = false;
            }
                
        }
            
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
        Vector3 dir = p1.position - transform.position;

        //zero out the vector's Y value
        dir.y = 0;

        //assign the character's rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1);

        //assign newPos and movement
        newPos = transform.position;

        Vector3 movement = (newPos - prevPos);



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
        direction = new Vector3(0, direction.y, speed);


        if (IsGrounded && jump)
        {
            anim.SetBool("Jump", true);
            StartCoroutine(Jump(time));
        }

        //if the player isn't grouned add gravity and go away from the jump animation
        if (!IsGrounded)
        {
            direction.y -= gravity;
            anim.SetBool("Jump", false);
        }
        // if the player's vertical direction is less than zero reset him at zero
        else if (direction.y < 0)
            direction.y = 0;

        if (Vector3.Distance(p1.position, transform.position) >= stopDistance)
        {
            Chase();
        }

        else if (Vector3.Distance(p1.position, transform.position) <= stopDistance && health.currentHealth > 0)

        {
            StartCoroutine(Attack(timeRNG));

        }
        if (GetComponent<PlayerHealth>().currentHealth < GetComponent<PlayerHealth>().maxHealth / 2 && retreatTimer < retreatTime)
        {
            Retreat();
            retreatTimer += Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(p1.position, transform.position) >= stopDistance)
            {
                Chase();
            }

            else if (Vector3.Distance(p1.position, transform.position) <= stopDistance && health.currentHealth > 0)

            {
                StartCoroutine(Attack(timeRNG));

            }
        }

       }

   

    void Chase()
    {
        rb.velocity = transform.rotation * direction;
    }

    void Retreat()
    {

        rb.velocity = transform.rotation * -direction;


    }

    void GameStart()
    {
        health = GetComponent<PlayerHealth>();
        //assign defualt speed to normSpeed
        normSpeed = speed;

        //assign default gravity to baseGravity
        baseGravity = gravity;

        //set up the references
        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();

        boxCol = GetComponent<BoxCollider>();

        distToGround = boxCol.bounds.extents.y;

        p1 = GameObject.FindGameObjectWithTag("P1").transform;

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
    IEnumerator Attack(float time)
    {
        //set attack to true and choose an attack retreat if you miss
        attk = true;
        anim.SetBool("WalkF", false);
        anim.SetBool("WalkB", false);

        Debug.Log("Attack");

        attkRNG = Random.Range(0, 50);
        if(timeRNG < 3 || timeRNG > 5)
        timeRNG = Random.Range(3, 5);
        yield return new WaitForSeconds(timeRNG);
        if (attackTimes <= 3)
        {
            attackTimes++;
            if (attkRNG <= 10)
                anim.SetBool("Primary1", true);
            else
                anim.SetBool("Primary1", false);

            if (attkRNG > 10 && attkRNG < 20)
                anim.SetBool("Primary2", true);
            else
                anim.SetBool("Primary2", false);

            if (attkRNG <= 20 && attkRNG < 30)
                anim.SetBool("Primary1", true);
            else
                anim.SetBool("Primary1", false);

            if (attkRNG > 30 && attkRNG < 40)
                anim.SetBool("Primary2", true);
            else
                anim.SetBool("Primary2", false);

        }
        else
        {
            Retreat();
            yield return new WaitForSeconds(10);
            attackTimes = 0;
        }

       
        }

        //attk = false;



    IEnumerator Neutral()
    {
        Chase();
        yield return new WaitForSeconds(Random.Range(1,5));
        Retreat();

    }












    public void EnableLArmBoxes()
    {
        foreach (BoxCollider h in armHitboxes_L)
        {
            h.GetComponent<MeshRenderer>().enabled = true;
            h.enabled = true;
        }
    }
    public void EnableRArmBoxes()
    {
        foreach (BoxCollider h in armHitboxes_R)
        {
            h.GetComponent<MeshRenderer>().enabled = true;

            h.enabled = true;
        }
    }
    public void EnableLLegBoxes()
    {
        foreach (BoxCollider h in legHitboxes_L)
        {
            h.GetComponent<MeshRenderer>().enabled = true;

            h.enabled = true;
        }
    }
    public void EnableRLegBoxes()
    {
        foreach (BoxCollider h in legHitboxes_R)
        {
            h.GetComponent<MeshRenderer>().enabled = true;

            h.enabled = true;
        }
    }

    public void DisableLArmBoxes()
    {
        foreach (BoxCollider h in armHitboxes_L)
        {
            h.enabled = false;
            h.GetComponent<MeshRenderer>().enabled = false;


        }
    }
    public void DisableRArmBoxes()
    {
        foreach (BoxCollider h in armHitboxes_R)
        {
            h.GetComponent<MeshRenderer>().enabled = false;

            h.enabled = false;

        }
    }
    public void DisableLLegBoxes()
    {
        foreach (BoxCollider h in legHitboxes_L)
        {
            h.GetComponent<MeshRenderer>().enabled = false;

            h.enabled = false;

        }
    }

    public void DisableRLegBoxes()
    {
        foreach (BoxCollider h in legHitboxes_R)
        {
            h.GetComponent<MeshRenderer>().enabled = false;

            h.enabled = false;
        }
    }
}
