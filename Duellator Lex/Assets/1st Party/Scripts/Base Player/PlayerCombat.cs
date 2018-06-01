using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour 
{
    Animator anim;      //the animator attached to the playwe

    //hitbox data
    public BoxCollider[] legHitboxes_L;
    public BoxCollider[] legHitboxes_R;
    public BoxCollider[] armHitboxes_L;
    public BoxCollider[] armHitboxes_R;


    void Awake ()
    {
        //setup the references
        anim = GetComponent<Animator>();

        //disable the hitboxes
        DisableLArmBoxes();
        DisableRArmBoxes();
        DisableLLegBoxes();
        DisableRLegBoxes();

	}
	
	void Update () 
    {
        //zero out the local position of the character
        transform.localPosition = Vector3.zero;

        //if we press the primary 1 button and nothing else do the primary 1 attack
        //otherwise do nothing

        if (Input.GetButtonDown("Primary 1")&& Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Primary1", true);
        else
            anim.SetBool("Primary1", false);
        
      
        //if we press the primary 2 button and nothing else do the primary 1 attack
        //otherwise do nothing

        if (Input.GetButtonDown("Primary 2") && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Primary2", true);
        else
            anim.SetBool("Primary2", false);
        

        //if we press the secondary 1 button and nothing else do the primary 1 attack
        //otherwise do nothing

        if (Input.GetButtonDown("Secondary 1")&& Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Secondary1", true);
        else
            anim.SetBool("Secondary1", false);


        //if we press the secondary 2 button and nothing else do the primary 1 attack
        //otherwise do nothing

        if (Input.GetButtonDown("Secondary 2") && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Secondary2", true);
        else
            anim.SetBool("Secondary2", false); 
        
        //if we press the primary 1 or primary 2 button whlie holding the special button do the special 1 attack
        //otherwise do nothing

        if ((Input.GetButtonDown("Primary 1") || Input.GetButtonDown("Primary 2")) && Input.GetButton("Special") &&
            Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Special1", true);
        else
            anim.SetBool("Special1", false);
        //if we press the secondary 1 or secondary 2 button whlie holding the special button do the special 1 attack
        //otherwise do nothing

        if ((Input.GetButtonDown("Secondary 1") || Input.GetButtonDown("Secondary 2")) && Input.GetButton("Special") &&
            Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Special2", true);
        else
            anim.SetBool("Special2", false);


	}

    //functions to enable and disable hitboxes
    public void EnableLArmBoxes()
    {
        foreach(BoxCollider h in armHitboxes_L)
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
