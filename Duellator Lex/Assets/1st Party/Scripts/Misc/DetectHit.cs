using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public bool hit = false;
    public GameObject character;
    public bool player = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(this.transform.root.tag);
	}


    void OnTriggerEnter(Collider col)
	{
        if(col.transform.root.tag != this.transform.root.tag && col.transform.tag == "Hurtbox" && !hit)
        {
            PlayerHealth playerHealth = col.GetComponentInParent<PlayerHealth>();
            DuoPlayerHealth duoPlayerHealth = col.GetComponentInParent<DuoPlayerHealth>();

            if (duoPlayerHealth != null)
                duoPlayerHealth.TakeDamage(100, 1, 1);
            else if (playerHealth != null)
            {
                Debug.Log(playerHealth.gameObject.name);
                playerHealth.TakeDamage(10, 1, 1);
            }

            //Debug.Log(3);

            if (!player)
            {
                character.GetComponentInChildren<EnemyAI>().DisableLArmBoxes();
                character.GetComponentInChildren<EnemyAI>().DisableRArmBoxes();
                character.GetComponentInChildren<EnemyAI>().DisableLLegBoxes();
                character.GetComponentInChildren<EnemyAI>().DisableRLegBoxes();
            }
            else
            {
                if(GetComponentInParent<EnemyAI>() != null)
                GetComponentInParent<EnemyAI>().attkHit = true;
                
                character.GetComponentInChildren<PlayerCombat>().DisableLArmBoxes();
                character.GetComponentInChildren<PlayerCombat>().DisableRArmBoxes();
                character.GetComponentInChildren<PlayerCombat>().DisableLLegBoxes();
                character.GetComponentInChildren<PlayerCombat>().DisableRLegBoxes();
            }

            hit = true;

            hit = false;



        }

        //Debug.Log(1);
	}
}
