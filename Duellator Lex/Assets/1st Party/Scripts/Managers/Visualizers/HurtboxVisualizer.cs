using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxVisualizer : MonoBehaviour 
{
    BoxCollider[] hurtboxes; //hitboxes attached to this character

	// Use this for initialization
	void Awake ()
    {
        hurtboxes = GetComponentsInChildren <BoxCollider> ();

        HideHurtboxes();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.P))
            ShowHurtboxes();
        else
            HideHurtboxes();

	}
    public void ShowHurtboxes()
    {
        foreach (BoxCollider h in hurtboxes)
        {
            if (h.tag == "Hurtbox")
                h.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void HideHurtboxes()
    {
        foreach (BoxCollider h in hurtboxes)
        {
            if (h.tag == "Hurtbox")
                h.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
