using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour 
{
    Animator anim;

   	void Awake ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update () 
    {
        transform.localPosition = Vector3.zero;

        if (Input.GetButtonDown("Primary 1")&& Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Primary1", true);
        else
            anim.SetBool("Primary1", false);
        
      

        if (Input.GetButtonDown("Primary 2") && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Primary2", true);
        else
            anim.SetBool("Primary2", false);
        


        if (Input.GetButtonDown("Secondary 1")&& Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Secondary1", true);
        else
            anim.SetBool("Secondary1", false);



        if (Input.GetButtonDown("Secondary 2") && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0 && !Input.GetButton("Special"))
            anim.SetBool("Secondary2", true);
        else
            anim.SetBool("Secondary2", false); 

        if ((Input.GetButtonDown("Primary 1") || Input.GetButtonDown("Primary 2")) && Input.GetButton("Special") &&
            Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Special1", true);
        else
            anim.SetBool("Special1", false);

        if ((Input.GetButtonDown("Secondary 1") || Input.GetButtonDown("Secondary 2")) && Input.GetButton("Special") &&
            Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Special2", true);
        else
            anim.SetBool("Special2", false);


	}
}
