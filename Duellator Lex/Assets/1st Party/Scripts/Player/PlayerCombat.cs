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
        if (Input.GetKeyDown(KeyCode.Z) && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Primary1", true);
        else
            anim.SetBool("Primary1", false);
        
      

        if (Input.GetKeyDown(KeyCode.X) && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Primary2", true);
        else
            anim.SetBool("Primary2", false);
        


        if (Input.GetKeyDown(KeyCode.C )&& Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Secondary1", true);
        else
            anim.SetBool("Secondary1", false);



        if (Input.GetKeyDown(KeyCode.V) && Input.GetAxis("Vertical") == 0
            && Input.GetAxis("Horizontal") == 0)
            anim.SetBool("Secondary2", true);
        else
            anim.SetBool("Secondary2", false);


	}
}
