using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float time, multiplier;  //the base time and mulitplier of hitstun
    public bool isDead;
    public Animator anim;

    public FighterSwitch fSwitch;
	// Use this for initialization
	void Awake ()
    {
        //assign current health to maxhealth and assign the references
        currentHealth = maxHealth;
	}

    void Start()
    {
    anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.O))
            TakeDamage(10, time, multiplier);


        //if (currentHealth <= 0 && !isDead)
           // Death();
       

	}

    public void TakeDamage(int amount, float stunTime, float hitstunMultiplier)
    {
        if (!isDead && currentHealth > 0)
        {
            currentHealth -= amount;

                anim.SetTrigger("Hit");
            
            if (currentHealth > 0 && !isDead)
            StartCoroutine(ResetHitStun(time, hitstunMultiplier));

            else if (currentHealth <= 0 && !isDead)
                Death();
        }


    }

    void Death()
    {
        if(!isDead)
        {
            isDead = true;
        
                anim.SetTrigger("Die");
            if(GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            else
                GetComponent<EnemyAI>().enabled = false;

            GetComponent<Rigidbody>().isKinematic = false;


        }
    }

    IEnumerator ResetHitStun(float stunTime, float stunMultiplier)
    {
        yield return new WaitForSeconds(time * stunMultiplier);
            anim.SetTrigger("Reset");
    }
}
